// packages
import { useContext } from 'react';

// lib
import type { KeycloakContextType } from './types';
import { KeycloakContext } from './keycloak-context';

export const useKeycloak = (): KeycloakContextType => {
  const context = useContext(KeycloakContext);

  if (!context) {
    throw new Error('The \'useKeycloak\' hook must be used within KeycloakProvider');
  }

  return context;
};