// api
import { createApi } from '@reduxjs/toolkit/query/react';
import { delayedBaseQuery } from '../base-query';

export const productBrandApi = createApi({
  reducerPath: 'productBrandApi',
  baseQuery: delayedBaseQuery,
  endpoints: (builder) => ({
    getProductBrands: builder.query<string[], void>({
      query: () => {
        return 'product-brands';
      }
    }),
  }),
});

export const {
  useGetProductBrandsQuery,
} = productBrandApi;
