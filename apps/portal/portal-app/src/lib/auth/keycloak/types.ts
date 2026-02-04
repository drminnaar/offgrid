import Keycloak from 'keycloak-js';

export interface KeycloakUser {
  id?: string;
  username?: string;
  email?: string;
  firstName?: string;
  lastName?: string;
  emailVerified?: boolean;
  [key: string]: unknown;
}

export interface KeycloakContextType {
  keycloak: Keycloak | null;
  authenticated: boolean;
  user: KeycloakUser | null;
  loading: boolean;
  login: () => void;
  logout: () => void;
  getRoles: () => string[];
  getToken: () => string | undefined;
  hasRealmRole: (role: string) => boolean;
  hasResourceRole: (role: string, resource?: string) => boolean;
  updateToken: (minValidity?: number) => Promise<boolean>;
}