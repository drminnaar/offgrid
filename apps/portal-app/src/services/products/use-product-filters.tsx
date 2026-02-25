import { useGetProductBrandsQuery } from './product-brand-api';
import { useGetProductCategoriesQuery } from './product-category-api';
import { useGetProductTypesQuery } from './product-type-api';

export const useProductFilters = () => {
  const types = useGetProductTypesQuery();
  const brands = useGetProductBrandsQuery();
  const categories = useGetProductCategoriesQuery();

  return {
    isLoading: types.isLoading || brands.isLoading || categories.isLoading,
    isError: types.isError || brands.isError || categories.isError,
    error: types.error || brands.error || categories.error,
    productTypes: types.data,
    productBrands: brands.data,
    productCategories: categories.data,
  };
};
