import Keycloak, { type KeycloakConfig } from 'keycloak-js';

const ENV_VARS = {
  url: 'VITE_KEYCLOAK_URL',
  realm: 'VITE_KEYCLOAK_REALM',
  clientId: 'VITE_KEYCLOAK_CLIENT_ID',
} as const;

const missingEnvVars = Object.values(ENV_VARS).filter(
  envVar => !import.meta.env[envVar]
);

if (missingEnvVars.length > 0) {
  throw new Error(`Missing required environment variables: ${missingEnvVars.join(', ')}`);
}

const keycloakConfig: KeycloakConfig = {
  url: import.meta.env[ENV_VARS.url],
  realm: import.meta.env[ENV_VARS.realm],
  clientId: import.meta.env[ENV_VARS.clientId],
};

const keycloak = new Keycloak(keycloakConfig);

export { keycloak };