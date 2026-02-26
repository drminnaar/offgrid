import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { getPortalApiBaseUrl } from './config';
import { keycloak } from '../lib/auth/keycloak/keycloak-client';

const apiBaseUrl = getPortalApiBaseUrl();

export const baseQuery = fetchBaseQuery({
  baseUrl: apiBaseUrl,
  prepareHeaders: async (headers) => {
    headers.set('Accept', 'application/json');

    // Ensure token is valid and refresh if needed
    if (keycloak.authenticated) {
      try {
        await keycloak.updateToken(30); // Refresh if token expires in less than 30 seconds
      } catch (error) {
        console.error('Failed to refresh token:', error);
        // Token refresh failed, user might need to re-authenticate
      }
    }

    // Add authorization header if token is available
    if (keycloak.token) {
      headers.set('Authorization', `Bearer ${keycloak.token}`);
    }

    return headers;
  },
});

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export const delayedBaseQuery: typeof baseQuery = async (args, api, extraOptions) => {
  await delay(500); // 1 second delay for testing
  return baseQuery(args, api, extraOptions);
};