/**
 * Represents the query parameters for fetching products from the server.
 * All parameters are optional, allowing for flexible filtering of products.
 * - `page`: The page number for pagination (default is 1).
 * - `limit`: The number of products to return per page (default is 10).
 * - `brands`: A comma-separated list of brand names to filter by.
 * - `categories`: A comma-separated list of category names to filter by.
 * - `types`: A comma-separated list of product types to filter by.
 */
export type GetProductsQuery = {
  /** The page number for pagination (default is 1). */
  page?: number;

  /** The number of products to return per page (default is 10). */
  limit?: number;

  /** A comma-separated list of brand names to filter by. */
  brands?: string;

  /** A comma-separated list of category names to filter by. */
  categories?: string;

  /** A comma-separated list of product types to filter by. */
  types?: string;
};

export type ProductInfo = {
  id: string;
  productId: string;
  sku: string;
  name: string;
  description: string;
  basePrice: number;
  currentPrice: number;
  isOnSale: boolean;
  salePercentage: number;
  totalStockQuantity: number;
  stockLevel: string;
  brand: string;
  category: string;
  subcategory: string;
  type: string;
  primaryImageUrl: string;
  createdAtUnixTimeSeconds: number;
  updatedAtUnixTimeSeconds: number;
};

export type ProductDetail = {
  id: string;
  productId: string;
  sku: string;
  name: string;
  description: string;
  basePrice: number;
  currentPrice: number;
  isOnSale: boolean;
  salePercentage: number;
  totalStockQuantity: number;
  stockLevel: string;
  brand: string;
  category: string;
  subcategory: string;
  type: string;
  createdAtUnixTimeSeconds: number;
  updatedAtUnixTimeSeconds: number;
  features: string[];
  specifications: Record<string, string>;
  variants: ProductVariant[];
  primaryImageUrl: string;
  imageUrls: string[];
};

export type ProductVariant = {
  sku: string;
  name: string;
  priceModifier: number;
  attributes: Record<string, string>;
  stockQuantity: number;
  imageUrl: string;
};

export type ProductCategory = {
  category: string;
  subcategories: string[];
};

export type ProductVariantInfo = {
  sku: string;
  name: string;
  priceModifier: number;
  attributes: Record<string, string>;
  stockQuantity: number;
  imageUrl: string;
};

export type IndexProductStatus = 'Pending' | 'InProgress' | 'Completed' | 'FailedAndRetrying' | 'Deadlettered';

export type IndexProductResult = {
  jobId: string;
  status: IndexProductStatus;
};

export type CurrentProductIndexInfo = {
  jobId: string | null;
  status: IndexProductStatus | '';
};

export type IndexingJobInfo = {
  jobId: string;
  status: IndexProductStatus;
  createdAt: string;
  completedAt: string | null;
};