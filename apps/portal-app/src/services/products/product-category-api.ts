// api
import { createApi } from '@reduxjs/toolkit/query/react';
import { delayedBaseQuery } from '../base-query';

// types
import type { ProductCategory } from './types';

export const productCategoryApi = createApi({
  reducerPath: 'productCategoryApi',
  baseQuery: delayedBaseQuery,
  endpoints: (builder) => ({
    getProductCategories: builder.query<ProductCategory[], void>({
      query: () => {
        return 'product-categories';
      }
    }),
  }),
});

export const {
  useGetProductCategoriesQuery,
} = productCategoryApi;
