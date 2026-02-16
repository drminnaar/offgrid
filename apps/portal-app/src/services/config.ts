import { isValidUrl } from '../lib/utils/url-utils';

export const environmentSettingName = {
  portalApiUrl: 'VITE_PORTAL_API_BASE_URL',
} as const;

export const getPortalApiBaseUrl = (): string => {
  const portalApiBaseUrl = import.meta.env.VITE_PORTAL_API_BASE_URL;

  if (!portalApiBaseUrl) {
    throw new Error(`Environment variable ${environmentSettingName.portalApiUrl} is missing`);
  }

  if (!isValidUrl(portalApiBaseUrl)) {
    throw new Error(`Environment variable ${environmentSettingName.portalApiUrl} is not a valid URL`);
  }

  return portalApiBaseUrl;
};