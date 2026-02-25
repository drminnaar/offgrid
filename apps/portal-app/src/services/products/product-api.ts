// api
import { createApi } from '@reduxjs/toolkit/query/react';
import { delayedBaseQuery } from '../base-query';

// types
import type { PagedListResult } from '../types';
import type { GetProductsQuery, ProductDetail, ProductInfo } from './types';

export const productApi = createApi({
  reducerPath: 'productApi',
  baseQuery: delayedBaseQuery,
  endpoints: (builder) => ({

    getProductById: builder.query<ProductDetail, string>({
      query: (productId) => `products/${productId}`,
    }),

    getProducts: builder.query<PagedListResult<ProductInfo>, GetProductsQuery>({
      query: (params) => {
        const queryParams = new URLSearchParams();
        Object.entries(params).forEach(([key, value]) => {
          if (value !== undefined && value !== '') {
            queryParams.append(key, value.toString());
          }
        });
        return `products?${queryParams.toString()}`;
      }
    }),
  }),
});

export const {
  /**
   * Fetches detailed information about a single product by its ID.
   * @param productId - The unique identifier of the product to fetch.
   * @returns A ProductDetail object containing comprehensive information about the product.
   */
  useGetProductByIdQuery,

  /**
   * Fetches products based on current filters. The API expects comma-separated strings for brands, categories, and types.
   * For example, if filters.brand is 'BrandA', we need to pass 'BrandA' to the API. If filters.brand is '', we should pass an empty string to the API to indicate no filtering by brand. Similarly for category and type.
   * @param filters - Object containing brand, category, type, etc.
   * @returns A PagedListResult object containing a list of ProductInfo objects and pagination information.
   */
  useGetProductsQuery,
} = productApi;
