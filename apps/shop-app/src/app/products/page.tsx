import { ProductPageView } from '@/components/products';
import { ProductSearchCriteria, searchProducts } from '@/services/products';

type Props = {
  searchParams: Promise<{ [key: string]: string | string[] | undefined }>;
};

export default async function ProductsPage({ searchParams }: Props) {
  const params = await searchParams;

  const toArray = (value: string | string[] | undefined): string[] => {
    if (!value) {
      return [];
    }

    return Array.isArray(value)
      ? value.filter(Boolean)
      : [value].filter(Boolean);
  };

  const toNumber = (value: string | string[] | undefined, fallback: number) => {
    const singleValue = Array.isArray(value) ? value[0] : value;
    const parsed = Number(singleValue);
    return Number.isFinite(parsed) && parsed > 0 ? parsed : fallback;
  };

  const toBoolean = (value: string | string[] | undefined) => {
    const singleValue = Array.isArray(value) ? value[0] : value;
    return singleValue === 'true';
  };

  const toStringValue = (value: string | string[] | undefined) => {
    if (Array.isArray(value)) {
      return value[0] ?? '';
    }

    return value ?? '';
  };

  const criteria: ProductSearchCriteria = {
    query: toStringValue(params.query),
    page: toNumber(params.page, 1),
    pageSize: toNumber(params.pageSize, 20),
    sortBy: toStringValue(params.sortBy) || undefined,
    categories: toArray(params.categories),
    subcategories: toArray(params.subcategories),
    brands: toArray(params.brands),
    types: toArray(params.types),
    colors: toArray(params.colors),
    sizes: toArray(params.sizes),
    inStockOnly: toBoolean(params.inStockOnly),
    onSaleOnly: toBoolean(params.onSaleOnly),
  };

  const response = await searchProducts(criteria);

  return (
    <div className='mx-auto w-full max-w-7xl px-4 py-8 sm:px-6 lg:px-8'>
      <ProductPageView initialData={response} />
    </div>
  );
}
