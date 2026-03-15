import NextAuth, { DefaultSession } from "next-auth";
import Keycloak from 'next-auth/providers/keycloak';
import { JWT } from 'next-auth/jwt';
import { upsertCustomer } from '@/services/customers';

// Extend the Session interface to include custom properties
// See: https://next-auth.js.org/getting-started/typescript#module-augmentation
declare module "next-auth" {
  interface Session {
    user: {
      id: string;
      active?: string;
      customerNumber?: string;
      provisioningFailed?: boolean;
    } & DefaultSession["user"];
    /** Set when Keycloak token refresh fails. Clients should trigger re-authentication. */
    error?: 'RefreshAccessTokenError';
  }
}

declare module "next-auth/jwt" {
  interface JWT {
    accessToken: string;
    userId: string;
    active?: string;
    customerNumber?: string;
    customerProvisioningFailed?: boolean;
    /** Unix timestamp (seconds) when the access token expires. */
    accessTokenExpiresAt: number;
    /** Keycloak refresh token — kept server-side only, never sent to the client. */
    refreshToken: string;
    /** Unix timestamp (seconds) when the refresh token expires. */
    refreshTokenExpiresAt: number;
    error?: 'RefreshAccessTokenError';
  }
}

// Refresh 30 s before actual expiry to avoid edge-of-window failures.
const EXPIRY_BUFFER_SECONDS = 30;

const isExpired = (expiresAt: number) =>
  Math.floor(Date.now() / 1000) >= expiresAt - EXPIRY_BUFFER_SECONDS;

const refreshAccessToken = async (token: JWT): Promise<JWT> => {
  try {
    const issuer = process.env.AUTH_KEYCLOAK_ISSUER;
    const clientId = process.env.AUTH_KEYCLOAK_ID;
    const clientSecret = process.env.AUTH_KEYCLOAK_SECRET;

    if (!issuer || !clientId) {
      throw new Error('Missing Keycloak environment configuration for token refresh');
    }

    const params: Record<string, string> = {
      grant_type: 'refresh_token',
      client_id: clientId,
      refresh_token: token.refreshToken,
    };
    if (clientSecret) params.client_secret = clientSecret;

    const response = await fetch(
      `${issuer}/protocol/openid-connect/token`,
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: new URLSearchParams(params),
        cache: 'no-store',
      }
    );

    const refreshed = await response.json() as Record<string, unknown>;

    if (!response.ok) {
      throw new Error((refreshed.error_description as string | undefined) ?? `Token refresh failed (${response.status})`);
    }

    const now = Math.floor(Date.now() / 1000);

    return {
      ...token,
      accessToken: refreshed.access_token as string,
      accessTokenExpiresAt: now + (refreshed.expires_in as number),
      // Keycloak rotates the refresh token on each use when rotation is enabled.
      refreshToken: (refreshed.refresh_token as string | undefined) ?? token.refreshToken,
      refreshTokenExpiresAt: typeof refreshed.refresh_expires_in === 'number'
        ? now + refreshed.refresh_expires_in
        : token.refreshTokenExpiresAt,
      error: undefined,
    };
  } catch (error) {
    console.error('[auth] refresh_access_token_failed', { error: String(error) });
    return { ...token, error: 'RefreshAccessTokenError' as const };
  }
};

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [Keycloak],
  callbacks: {
    async jwt({ token, account, profile }) {
      const nextToken = { ...token };

      // ── Initial sign-in: populate all token fields from Keycloak ──────────
      if (account?.access_token) {
        const keycloakAccount = account as Record<string, unknown>;
        const now = Math.floor(Date.now() / 1000);

        nextToken.accessToken = account.access_token;
        nextToken.userId = profile?.sub ?? nextToken.userId ?? '';
        nextToken.accessTokenExpiresAt = (account.expires_at as number | undefined) ?? 0;
        nextToken.refreshToken = (account.refresh_token as string | undefined) ?? '';
        nextToken.refreshTokenExpiresAt = typeof keycloakAccount.refresh_expires_in === 'number'
          ? now + keycloakAccount.refresh_expires_in
          : 0;

        if (profile?.email) {
          try {
            const response = await upsertCustomer(account.access_token, {
              keycloakUserId: profile.sub ?? '',
              email: profile.email,
              fullName: profile.name || '',
            });

            if (response.success) {
              nextToken.active = response.data.status.toLowerCase();
              nextToken.customerNumber = response.data.customerNumber;
              nextToken.customerProvisioningFailed = false;
            } else {
              // Do not block sign-in if customer API is temporarily unavailable.
              nextToken.customerProvisioningFailed = true;
            }
          } catch (error) {
            console.error('[auth] customer_upsert_failed', { error: String(error) });
            nextToken.customerProvisioningFailed = true;
          }
        }

        return nextToken;
      }

      // ── Subsequent calls: return early if access token is still valid ──────
      if (!isExpired(nextToken.accessTokenExpiresAt ?? 0)) {
        return nextToken;
      }

      // ── Refresh token is missing or has itself expired — force re-auth ─────
      if (!nextToken.refreshToken || isExpired(nextToken.refreshTokenExpiresAt ?? 0)) {
        return { ...nextToken, error: 'RefreshAccessTokenError' as const };
      }

      // ── Attempt silent token refresh ──────────────────────────────────────
      return refreshAccessToken(nextToken);
    },

    async session({ session, token }) {
      // Safe fields only; never expose accessToken or refreshToken to the client.
      session.user.id = token.userId ?? '';
      session.user.active = token.active;
      session.user.customerNumber = token.customerNumber;
      session.user.provisioningFailed = token.customerProvisioningFailed;
      // Propagate refresh error so the client can trigger re-authentication.
      session.error = token.error;
      return session;
    },
  },
});