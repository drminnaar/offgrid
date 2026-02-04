// packages
import { createContext } from 'react';

// types
import type { KeycloakContextType } from './types';

export const KeycloakContext = createContext<KeycloakContextType | null>(null);
