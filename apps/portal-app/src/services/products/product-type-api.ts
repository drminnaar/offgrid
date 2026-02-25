// api
import { createApi } from '@reduxjs/toolkit/query/react';
import { delayedBaseQuery } from '../base-query';

export const productTypeApi = createApi({
  reducerPath: 'productTypeApi',
  baseQuery: delayedBaseQuery,
  endpoints: (builder) => ({
    getProductTypes: builder.query<string[], void>({
      query: () => {
        return 'product-types';
      }
    }),
  }),
});

export const {
  useGetProductTypesQuery,
} = productTypeApi;
