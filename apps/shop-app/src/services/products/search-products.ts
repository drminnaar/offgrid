import { ApiResponse } from '@/lib/shop-api/types';
import { shopApiClient } from '@/lib/shop-api/client';
import { ProductSearchCriteria, SearchProductResponse } from './types';

export const searchProducts = async (
  criteria: ProductSearchCriteria
): Promise<SearchProductResponse> => {
  const response = await searchProductsAsync(criteria);
  if (response.success) {
    return response.data;
  } else {
    const errorMessage = typeof response.error === 'string'
      ? response.error
      : response.error?.title || 'Failed to search products';
    throw new Error(errorMessage);
  }
};

const searchProductsAsync = async (
  criteria: ProductSearchCriteria
): Promise<ApiResponse<SearchProductResponse>> => {
  const queryParams = new URLSearchParams();
  Object.entries(criteria).forEach(([key, value]) => {
    if (value === undefined || value === null || value === '') {
      return;
    }

    if (Array.isArray(value)) {
      value
        .map((entry) => entry?.toString().trim())
        .filter((entry) => Boolean(entry))
        .forEach((entry) => queryParams.append(key, entry!));
      return;
    }

    if (typeof value === 'boolean') {
      if (value) {
        queryParams.append(key, 'true');
      }
      return;
    }

    queryParams.append(key, value.toString());
  });

  return shopApiClient.request<SearchProductResponse>(`/products?${queryParams}`, '', {
    method: 'GET',
    cache: 'no-store',
  });
};
