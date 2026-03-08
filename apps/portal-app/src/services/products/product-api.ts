// api
import { createApi } from '@reduxjs/toolkit/query/react';
import { delayedBaseQuery } from '../base-query';

// types
import type { PagedListResult } from '../types';
import type {
  CurrentProductIndexInfo,
  GetProductsQuery,
  IndexingJobInfo,
  IndexProductResult,
  ProductDetail,
  ProductInfo,
  ProductVariantInfo,
} from './types';

export const productApi = createApi({
  reducerPath: 'productApi',
  baseQuery: delayedBaseQuery,
  endpoints: (builder) => ({

    getProductById: builder.query<ProductDetail, string>({
      query: (productId) => `products/${productId}`,
    }),

    getProductVariants: builder.query<ProductVariantInfo[], string>({
      query: (productId) => `products/${productId}/variants`,
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

    getProductIndex: builder.query<{ jobId: string; }, string>({
      query: (jobId) => `products/indexes/${jobId}`,
    }),

    getRecentProductIndexingJobs: builder.query<IndexingJobInfo[], number>({
      query: (count) => `products/indexes/recent?count=${count}`,
    }),

    getCurrentProductIndex: builder.query<CurrentProductIndexInfo, void>({
      query: () => `products/indexes/current`,
    }),

    indexProducts: builder.mutation<IndexProductResult, void>({
      query: () => ({
        url: 'products/indexes',
        method: 'POST',
      }),
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
   * Fetches products based on current filters. The API expects comma-separated strings for brands,
   * categories, and types. For example, if filters.brand is 'BrandA', we need to pass 'BrandA' to
   * the API. If filters.brand is '', we should pass an empty string to the API to indicate no 
   * filtering by brand. Similarly for category and type.
   * @param filters - Object containing brand, category, type, etc.
   * @returns A PagedListResult object containing a list of ProductInfo objects and pagination information.
   */
  useGetProductsQuery,

  /**
   * Fetches variants of a specific product by its ID.
   * @param productId - The unique identifier of the product whose variants to fetch.
   * @returns An array of ProductVariantInfo objects containing information about each variant.
   */
  useGetProductVariantsQuery,

  /**
   * Fetches the status of a product indexing job by its job ID.
   * @param jobId - The unique identifier of the indexing job to check.
   * @returns An object containing the job ID and its status.
   */
  useGetProductIndexQuery,

  /**
   * Initiates the indexing of products. This mutation does not take any parameters and returns an
   * object containing the job ID and its status.
   * @returns An object containing the job ID and its status after starting the indexing process.
   */
  useIndexProductsMutation,

  /**
   * Fetches the status of the current product indexing job.
   * @returns An object containing the job ID and its status.
   */
  useGetCurrentProductIndexQuery,

  /**
   * Fetches a list of recent product indexing jobs, limited by the specified count.
   * @param count - The maximum number of recent indexing jobs to retrieve.
   * @returns An array of IndexingJobInfo objects containing information about each recent indexing job.
   */
  useGetRecentProductIndexingJobsQuery,
} = productApi;
