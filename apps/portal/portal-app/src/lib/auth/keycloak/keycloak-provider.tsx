// packages
import { useState, useEffect, useCallback, useRef } from 'react';

// lib
import { keycloak } from './keycloak-client';
import type { KeycloakContextType, KeycloakUser } from './types';
import { KeycloakContext } from './keycloak-context';

interface KeycloakProviderProps {
  children: React.ReactNode;
}

export const KeycloakProvider: React.FC<KeycloakProviderProps> = ({
  children,
}) => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [user, setUser] = useState<KeycloakUser | null>(null);
  const initialized = useRef(false);

  useEffect(() => {
    if (initialized.current) return;

    initialized.current = true;

    keycloak
      .init({
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: `${window.location.origin}/silent-check-sso.html`,
        checkLoginIframe: false,
        pkceMethod: 'S256',
        enableLogging: false,
      })
      .then((isAuthenticated) => {
        setAuthenticated(isAuthenticated);
        setLoading(false);

        if (isAuthenticated) {
          if (keycloak.tokenParsed) {
            setUser({
              id: keycloak.tokenParsed.sub,
              username: keycloak.tokenParsed.preferred_username,
              firstName: keycloak.tokenParsed.given_name,
              lastName: keycloak.tokenParsed.family_name,
              email: keycloak.tokenParsed.email,
            } as KeycloakUser);
          }
        }

        // Token refresh
        setInterval(() => {
          keycloak.updateToken(70).catch(() => {
            console.error('Failed to refresh token');
          });
        }, 60000);
      })
      .catch((error) => {
        console.error('Failed to initialize Keycloak', error);
        setAuthenticated(false);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  const login = useCallback(() => {
    keycloak.login();
  }, []);

  const logout = useCallback(() => {
    keycloak.logout();
  }, []);

  const getRoles = useCallback((): string[] => {
    return keycloak.realmAccess?.roles ?? [];
  }, []);

  const getToken = useCallback((): string | undefined => {
    return keycloak.token;
  }, []);

  const hasRealmRole = useCallback((role: string): boolean => {
    return keycloak.hasRealmRole(role);
  }, []);

  const hasResourceRole = useCallback(
    (role: string, resource?: string): boolean => {
      return keycloak.hasResourceRole(role, resource);
    },
    [],
  );

  const updateToken = useCallback(
    async (minValidity: number = 30): Promise<boolean> => {
      try {
        const refreshed = await keycloak.updateToken(minValidity);
        return refreshed;
      } catch (error) {
        console.error('Failed to refresh token', error);
        return false;
      }
    },
    [],
  );

  const value: KeycloakContextType = {
    keycloak,
    authenticated,
    user,
    loading,
    login,
    logout,
    getRoles,
    getToken,
    hasRealmRole,
    hasResourceRole,
    updateToken,
  };

  if (loading) {
    return (
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <div>Loading authentication...</div>
      </div>
    );
  }

  return (
    <KeycloakContext.Provider value={value}>
      {children}
    </KeycloakContext.Provider>
  );
};
