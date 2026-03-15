export type PageMetadata = {
  currentPageNumber: number;
  itemCount: number;
  pageSize: number;
  pageCount: number;
  lastPageNumber: number;
  nextPageNumber?: number;
  previousPageNumber?: number;
  hasPrevious: boolean;
  hasNext: boolean;
};

export type SearchProductResponse = {
  items: Product[];
  facets: Facets;
  pageMetadata: PageMetadata;
};

export type Product = {
  id: string;
  productId: string;
  productSku: string;
  variantSku: string;
  name: string;
  variantName: string;
  description: string;
  type: string;
  brand: string;
  category: string;
  subcategory: string;
  features: string[];
  isOnSale: boolean;
  salePercentage: number;
  basePrice: number;
  currentPrice: number;
  color: string;
  colorHex: string;
  size?: string;
  package?: string;
  buildKit?: string;
  finSetup?: string;
  totalStock: number;
  hasStock: boolean;
  imageUrl?: string;
};

export type Facets = {
  types: FacetCount[];
  categories: FacetCount[];
  subcategories: FacetCount[];
  brands: FacetCount[];
  colors: FacetCount[];
  sizes: FacetCount[];
  isOnSale: FacetCount[];
};

export type FacetCount = {
  value: string;
  count: number;
};

export type ProductSearchCriteria = {
  query: string;
  page: number;
  pageSize: number;
  sortBy?: string;
  categories: string[];
  subcategories: string[];
  brands: string[];
  types: string[];
  colors: string[];
  sizes: string[];
  minPrice?: number;
  maxPrice?: number;
  inStockOnly?: boolean;
  onSaleOnly?: boolean;
};
