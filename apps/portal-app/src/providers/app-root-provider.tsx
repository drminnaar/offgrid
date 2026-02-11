// packages
import { Provider } from 'react-redux';

// custom providers
import { KeycloakProvider } from '../lib/auth/keycloak';
import { AppThemeProvider } from './app-theme-provider';

// state
import { appStore } from '../store';

export const AppRootProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <Provider store={appStore}>
      <KeycloakProvider>
        <AppThemeProvider>{children}</AppThemeProvider>
      </KeycloakProvider>
    </Provider>
  );
};
