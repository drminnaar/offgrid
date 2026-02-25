// packages
import { useState } from 'react';
import { Box } from '@mui/material';

// api
import { useProductFilters } from '../../../services/products';
import { ProductFilters } from './product-filters';
import { useGetProductsQuery } from '../../../services/products/product-api';

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

  if (isLoading || isProductsLoading) return <ProductViewSkeleton />;

  if (isError) throw error;
  if (isProductsError) throw productsError;

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

      <ProductTable products={products?.items ?? []} />

      <AppPagination
        paginationInfo={products}
        onPageChange={(page) => setFilters((prev) => ({ ...prev, page }))}
      />
    </Box>
  );
};
