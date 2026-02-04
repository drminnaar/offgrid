// custom providers
import { KeycloakProvider } from '../lib/auth/keycloak';

export const AppRootProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return <KeycloakProvider>{children}</KeycloakProvider>;
};
