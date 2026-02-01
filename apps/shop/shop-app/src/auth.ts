import NextAuth, { DefaultSession } from "next-auth";
import Keycloak from 'next-auth/providers/keycloak';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { JWT } from 'next-auth/jwt';
import { upsertCustomer } from './app/actions/customers';

// Extend the Session interface to include custom properties
// See: https://next-auth.js.org/getting-started/typescript#module-augmentation
declare module "next-auth" {
  interface Session {
    user: {
      id: string;
      active: string;
      customerNumber: string;
    } & DefaultSession["user"];
    accessToken: string;
  }
}

declare module "next-auth/jwt" {
  interface JWT {
    accessToken: string;
    userId: string;
  }
}

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [Keycloak],
  callbacks: {
    async jwt({ token, account, profile }) {
      if (account && account.access_token) {
        token.accessToken = account.access_token;
        token.userId = profile?.sub ?? '';
      }
      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken;
      session.user.id = token.userId;
      if (session.user.id && session.user.email) {
        const response = await upsertCustomer(session.accessToken, {
          keycloakUserId: session.user.id,
          email: session.user.email,
          fullName: session.user.name || '',
        });

        if (response.success) {
          // Update session with customer details
          session.user.active = response.data.status.toLowerCase();
          session.user.customerNumber = response.data.customerNumber;
        }
      }
      return session;
    }
  }
});