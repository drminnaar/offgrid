import NextAuth, { DefaultSession } from "next-auth";
import Keycloak from 'next-auth/providers/keycloak';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
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
    // accessToken: string;
  }
}

declare module "next-auth/jwt" {
  interface JWT {
    accessToken: string;
    userId: string;
    active?: string;
    customerNumber?: string;
    customerProvisioningFailed?: boolean;
  }
}

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [Keycloak],
  callbacks: {
    async jwt({ token, account, profile }) {
      // Keep prior values by default
      const nextToken = { ...token };

      // Initial sign-in with Keycloak
      if (account?.access_token) {
        nextToken.accessToken = account.access_token;
        nextToken.userId = profile?.sub ?? nextToken.userId ?? '';

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
              // Do not block sign-in if customer API is temporarily failing
              nextToken.customerProvisioningFailed = true;
            }
          } catch (error) {
            // TODO: replace with structured logging
            console.error('Customer upsert failed during sign-in', error);
            nextToken.customerProvisioningFailed = true;
          }
        }
      }

      return nextToken;
    },

    async session({ session, token }) {
      // Safe fields only; never expose accessToken to client
      session.user.id = token.userId ?? '';
      session.user.active = token.active;
      session.user.customerNumber = token.customerNumber;
      session.user.provisioningFailed = token.customerProvisioningFailed;
      return session;
    },
  },
});