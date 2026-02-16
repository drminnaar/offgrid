import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { getPortalApiBaseUrl } from '../config';
import type {
  CustomerDetail,
  CustomerInfo,
  GetAllCustomersQuery,
  SuspendCustomerRequest,
  ReinstateCustomerRequest,
  SuspendCustomerResult,
  ReinstateCustomerResult
} from './types';
import type { PagedListResult } from '../types';
import { keycloak } from '../../lib/auth/keycloak/keycloak-client';

const apiBaseUrl = getPortalApiBaseUrl();

const baseQuery = fetchBaseQuery({
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

export const customerApi = createApi({
  reducerPath: 'customerApi',
  baseQuery,
  endpoints: (builder) => ({
    getCustomerById: builder.query<CustomerDetail, string>({
      query: (customerId) => `customers/${customerId}`,
    }),
    getCustomers: builder.query<PagedListResult<CustomerInfo>, GetAllCustomersQuery>({
      query: (params) => {
        const queryParams = new URLSearchParams();
        Object.entries(params).forEach(([key, value]) => {
          if (value !== undefined && value !== '') {
            queryParams.append(key, value.toString());
          }
        });
        return `customers?${queryParams.toString()}`;
      }
    }),
    suspendCustomer: builder.mutation<SuspendCustomerResult, { customerId: string; request: SuspendCustomerRequest; }>({
      query: ({ customerId, request }) => ({
        url: `customers/${customerId}/suspend`,
        method: 'POST',
        body: request,
      }),
    }),
    reinstateCustomer: builder.mutation<ReinstateCustomerResult, { customerId: string; request: ReinstateCustomerRequest; }>({
      query: ({ customerId, request }) => ({
        url: `customers/${customerId}/reinstate`,
        method: 'POST',
        body: request,
      }),
    }),
  }),
});

export const {
  useGetCustomerByIdQuery,
  useGetCustomersQuery,
  useSuspendCustomerMutation,
  useReinstateCustomerMutation,
} = customerApi;
