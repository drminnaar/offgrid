import { ApiResponse } from '@/lib/shop-api/types';
import { shopApiClient } from '@/lib/shop-api/client';
import { UpsertCustomerRequest, UpsertCustomerResponse } from './types';

export const upsertCustomer = async (
  accessToken: string,
  customer: UpsertCustomerRequest
): Promise<ApiResponse<UpsertCustomerResponse>> => {
  return shopApiClient.request<UpsertCustomerResponse>('/customers', accessToken, {
    method: 'POST',
    body: customer,
    cache: 'no-store',
  });
};
