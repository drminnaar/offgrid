// packages
import { useState } from 'react';
import { Box } from '@mui/material';

// api
import { useProductFilters } from '../../../services/products';
import { ProductFilters } from './product-filters';
import {
  useGetProductsQuery,
  useGetProductVariantsQuery,
} from '../../../services/products/product-api';

// custom components
import { ProductTable } from './product-table';
import { AppPagination } from '../../../lib/ui';
import { ProductViewSkeleton } from './product-view-skeleton';

// types
type ProductFilters = {
  brand?: string;
  category?: string;
  type?: string;
  page: number;
  limit: number;
};

const initialFilters: ProductFilters = {
  brand: '',
  category: '',
  type: '',
  page: 1,
  limit: 10,
};

export const ProductView = () => {
  const [filters, setFilters] = useState<ProductFilters>(initialFilters);
  const [selectedProductId, setSelectedProductId] = useState<string | null>(
    null,
  );

  const {
    isLoading,
    isError,
    error,
    productTypes,
    productBrands,
    productCategories,
  } = useProductFilters();

  const {
    data: products,
    isLoading: isProductsLoading,
    isError: isProductsError,
    error: productsError,
    refetch,
  } = useGetProductsQuery({
    ...filters,
    brands: filters.brand ?? '',
    categories: filters.category ?? '',
    types: filters.type ?? '',
  });

  const {
    data: productVariants,
    isLoading: isProductVariantsLoading,
    isError: isProductVariantsError,
    error: productVariantsError,
    isFetching: isProductVariantsFetching,
  } = useGetProductVariantsQuery(selectedProductId!, {
    skip: !selectedProductId,
  });

  if (isLoading || isProductsLoading) return <ProductViewSkeleton />;

  const isAnyError = isError || isProductsError || isProductVariantsError;
  const anyError = error || productsError || productVariantsError;
  if (isAnyError) throw anyError;

  return (
    <Box>
      <ProductFilters
        data={{
          brands: productBrands!.map((b) => ({ key: b, value: b, label: b })),
          categories: productCategories!.map((c) => ({
            key: c.category,
            value: c.category,
            label: c.category,
          })),
          types: productTypes!.map((t) => ({ key: t, value: t, label: t })),
        }}
        filters={filters}
        onFilterChange={(key, value) =>
          setFilters((prev) => ({ ...prev, [key]: value }))
        }
        onRefresh={() => refetch()}
      />

      <ProductTable
        products={products?.items ?? []}
        variants={productVariants ?? []}
        onProductRowClick={(productId) => setSelectedProductId(productId)}
        isVariantsLoading={
          isProductVariantsLoading || isProductVariantsFetching
        }
      />

      <AppPagination
        paginationInfo={products}
        onPageChange={(page) => setFilters((prev) => ({ ...prev, page }))}
      />
    </Box>
  );
};
