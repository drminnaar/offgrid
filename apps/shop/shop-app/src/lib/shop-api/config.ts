import { isValidUrl } from '../utils/url-validation';

export const environmentSettingName = {
  shopApiUrl: 'SHOP_API_BASE_URL',
} as const;

export const getShopApiBaseUrl = (): string => {
  const shopApiBaseUrl = process.env.SHOP_API_BASE_URL;

  if (!shopApiBaseUrl) {
    throw new Error(`Environment variable ${environmentSettingName.shopApiUrl} is missing`);
  }

  if (!isValidUrl(shopApiBaseUrl)) {
    throw new Error(`Environment variable ${environmentSettingName.shopApiUrl} is not a valid URL`);
  }

  return shopApiBaseUrl;
};
