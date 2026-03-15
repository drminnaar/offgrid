'use client';

import {
  ProductSearchCriteria,
  SearchProductResponse,
} from '@/services/products';
import {
  Button,
  Card,
  CardBody,
  Chip,
  Select,
  SelectItem,
  Spinner,
  Switch,
} from '@heroui/react';
import {
  ListFilter as ListFilterIcon,
  SlidersHorizontal as SlidersHorizontalIcon,
} from 'lucide-react';
import {
  ReadonlyURLSearchParams,
  usePathname,
  useRouter,
  useSearchParams,
} from 'next/navigation';
import {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
  useTransition,
} from 'react';
import { SearchBar } from '../search';
import { ProductGrid } from '../grid';
import { PaginationBar } from '../pagination';
import { FacetSection } from '../facets';

type Props = {
  initialData: SearchProductResponse;
};

type FacetKey =
  | 'categories'
  | 'subcategories'
  | 'brands'
  | 'types'
  | 'colors'
  | 'sizes';

const DEFAULT_PAGE_SIZE = 20;

const SORT_OPTIONS = [
  { key: 'relevance', label: 'Most Relevant' },
  { key: 'currentPrice:asc', label: 'Price: Low to High' },
  { key: 'currentPrice:desc', label: 'Price: High to Low' },
] as const;

// ── URL ↔ Criteria helpers ──────────────────────────────────────────────────

const getAll = (p: ReadonlyURLSearchParams, key: string): string[] =>
  p
    .getAll(key)
    .map((v) => v.trim())
    .filter(Boolean);

const parseCriteria = (p: ReadonlyURLSearchParams): ProductSearchCriteria => {
  const pageRaw = Number(p.get('page'));
  const pageSizeRaw = Number(p.get('pageSize'));
  return {
    query: p.get('query') ?? '',
    page: Number.isFinite(pageRaw) && pageRaw > 0 ? pageRaw : 1,
    pageSize:
      Number.isFinite(pageSizeRaw) && pageSizeRaw > 0
        ? pageSizeRaw
        : DEFAULT_PAGE_SIZE,
    sortBy: p.get('sortBy') ?? undefined,
    categories: getAll(p, 'categories'),
    subcategories: getAll(p, 'subcategories'),
    brands: getAll(p, 'brands'),
    types: getAll(p, 'types'),
    colors: getAll(p, 'colors'),
    sizes: getAll(p, 'sizes'),
    inStockOnly: p.get('inStockOnly') === 'true',
    onSaleOnly: p.get('onSaleOnly') === 'true',
  };
};

const buildSearch = (criteria: ProductSearchCriteria): string => {
  const p = new URLSearchParams();

  p.set('page', String(criteria.page));
  p.set('pageSize', String(criteria.pageSize));

  if (criteria.query.trim()) p.set('query', criteria.query.trim());
  if (criteria.sortBy?.trim()) p.set('sortBy', criteria.sortBy);

  const facetKeys: FacetKey[] = [
    'categories',
    'subcategories',
    'brands',
    'types',
    'colors',
    'sizes',
  ];
  facetKeys.forEach((key) =>
    criteria[key]
      .map((v) => v.trim())
      .filter(Boolean)
      .forEach((v) => p.append(key, v)),
  );

  if (criteria.inStockOnly) p.set('inStockOnly', 'true');
  if (criteria.onSaleOnly) p.set('onSaleOnly', 'true');

  return p.toString();
};

export const ProductPageView = ({ initialData }: Props) => {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();
  const [isPending, startTransition] = useTransition();

  // ── URL is the single source of truth for all filter/search/sort/page state ─
  const criteria = useMemo(() => parseCriteria(searchParams), [searchParams]);

  // ── navigate: the only way to mutate state ────────────────────────────────
  const navigate = useCallback(
    (next: ProductSearchCriteria) => {
      const qs = buildSearch(next);
      startTransition(() => {
        router.replace(qs ? `${pathname}?${qs}` : pathname, { scroll: false });
      });
    },
    [pathname, router],
  );

  // ── searchText: local buffer for debounced typing ────────────────────────
  // Local search text buffers keystrokes and debounces to the URL.
  // prevQuery tracks the last URL query so we can detect external URL changes
  // (back/forward) and reset the input — without using an effect for setState.
  // See: https://react.dev/learn/you-might-not-need-an-effect#adjusting-some-state-when-a-prop-changes
  const [searchText, setSearchText] = useState(criteria.query);
  const [prevQuery, setPrevQuery] = useState(criteria.query);
  if (prevQuery !== criteria.query) {
    setPrevQuery(criteria.query);
    setSearchText(criteria.query);
  }

  // Keep a ref so the debounce callback always sees the latest criteria without
  // needing it as an effect dep (which would reset the timer on every URL change).
  // useEffect is safe here: 300 ms always elapses well after this effect runs.
  const criteriaRef = useRef(criteria);
  useEffect(() => {
    criteriaRef.current = criteria;
  }, [criteria]);

  // Debounce typing → URL push..
  useEffect(() => {
    if (searchText === criteriaRef.current.query) return;
    const timer = window.setTimeout(
      () => navigate({ ...criteriaRef.current, query: searchText, page: 1 }),
      300,
    );
    return () => window.clearTimeout(timer);
  }, [searchText, navigate]);

  // ── derived counts ────────────────────────────────────────────────────────
  const activeFilterCount = useMemo(
    () =>
      criteria.categories.length +
      criteria.subcategories.length +
      criteria.brands.length +
      criteria.types.length +
      criteria.colors.length +
      criteria.sizes.length +
      (criteria.inStockOnly ? 1 : 0) +
      (criteria.onSaleOnly ? 1 : 0),
    [criteria],
  );

  // ── action handlers ───────────────────────────────────────────────────────
  const toggleFacet = (key: FacetKey, value: string) => {
    const prev = criteriaRef.current;
    navigate({
      ...prev,
      page: 1,
      [key]: prev[key].includes(value)
        ? prev[key].filter((e) => e !== value)
        : [...prev[key], value],
    });
  };

  const setSort = (value: string) => {
    navigate({
      ...criteriaRef.current,
      page: 1,
      sortBy: value === 'relevance' ? undefined : value,
    });
  };

  const clearAll = () => {
    setSearchText('');
    navigate({
      ...criteriaRef.current,
      page: 1,
      query: '',
      sortBy: undefined,
      categories: [],
      subcategories: [],
      brands: [],
      types: [],
      colors: [],
      sizes: [],
      inStockOnly: false,
      onSaleOnly: false,
    });
  };

  return (
    <section className='flex flex-col gap-5'>
      <header className='space-y-1'>
        <h1 className='text-2xl font-bold tracking-tight text-foreground'>
          Browse Products
        </h1>
        <p className='text-sm text-default-600'>
          Filter by facets, refine your search, and sort results by price.
        </p>
      </header>

      <SearchBar value={searchText} onSearchTextChange={setSearchText} />

      <div className='flex items-start gap-6'>
        <aside className='hidden w-72 shrink-0 lg:flex lg:flex-col lg:gap-4'>
          <Card className='border border-divider bg-content1'>
            <CardBody className='space-y-5 p-4'>
              <div className='flex items-center justify-between'>
                <div className='flex items-center gap-2 text-sm font-semibold text-foreground'>
                  <ListFilterIcon className='h-4 w-4 text-primary' />
                  Filters
                </div>
                {activeFilterCount > 0 && (
                  <Button
                    size='sm'
                    variant='light'
                    color='primary'
                    onPress={clearAll}
                  >
                    Clear all
                  </Button>
                )}
              </div>

              <FacetSection
                title='Type'
                items={initialData.facets.types}
                selectedValues={criteria.types}
                onToggle={(value) => toggleFacet('types', value)}
              />

              <FacetSection
                title='Category'
                items={initialData.facets.categories}
                selectedValues={criteria.categories}
                onToggle={(value) => toggleFacet('categories', value)}
              />

              <FacetSection
                title='Subcategory'
                items={initialData.facets.subcategories}
                selectedValues={criteria.subcategories}
                onToggle={(value) => toggleFacet('subcategories', value)}
              />

              <FacetSection
                title='Brand'
                items={initialData.facets.brands}
                selectedValues={criteria.brands}
                onToggle={(value) => toggleFacet('brands', value)}
              />

              <FacetSection
                title='Color'
                items={initialData.facets.colors}
                selectedValues={criteria.colors}
                onToggle={(value) => toggleFacet('colors', value)}
              />

              <FacetSection
                title='Size'
                items={initialData.facets.sizes}
                selectedValues={criteria.sizes}
                onToggle={(value) => toggleFacet('sizes', value)}
              />

              <div className='space-y-2 border-t border-divider pt-4'>
                <Switch
                  isSelected={Boolean(criteria.inStockOnly)}
                  onValueChange={(isSelected) =>
                    setCriteria((prev) => ({
                      ...prev,
                      page: 1,
                      inStockOnly: isSelected,
                    }))
                  }
                >
                  In stock only
                </Switch>
                <Switch
                  isSelected={Boolean(criteria.onSaleOnly)}
                  onValueChange={(isSelected) =>
                    setCriteria((prev) => ({
                      ...prev,
                      page: 1,
                      onSaleOnly: isSelected,
                    }))
                  }
                >
                  On sale only
                </Switch>
              </div>
            </CardBody>
          </Card>
        </aside>

        <div className='flex min-w-0 flex-1 flex-col gap-4'>
          <Card className='border border-divider bg-content1'>
            <CardBody className='flex flex-col gap-3 p-4'>
              <div className='flex flex-wrap items-center justify-between gap-3'>
                <div className='flex items-center gap-3'>
                  <span className='text-sm text-default-600'>
                    {initialData.pageMetadata.itemCount.toLocaleString()}{' '}
                    results
                  </span>
                  {activeFilterCount > 0 && (
                    <Chip color='primary' variant='flat' size='sm'>
                      {activeFilterCount} active filters
                    </Chip>
                  )}
                </div>
                <div className='flex items-center gap-2'>
                  <SlidersHorizontalIcon className='h-4 w-4 text-default-500' />
                  <Select
                    aria-label='Sort products'
                    placeholder='Sort by'
                    selectedKeys={[criteria.sortBy ?? 'relevance']}
                    onSelectionChange={(keys) => {
                      const [selected] = Array.from(keys).map(String);
                      if (selected) {
                        setSort(selected);
                      }
                    }}
                    size='sm'
                    className='w-56'
                  >
                    {SORT_OPTIONS.map((option) => (
                      <SelectItem key={option.key}>{option.label}</SelectItem>
                    ))}
                  </Select>
                </div>
              </div>

              {activeFilterCount > 0 && (
                <div className='flex flex-wrap items-center gap-2'>
                  {criteria.types.map((value) => (
                    <ActiveFilterChip
                      key={`type-${value}`}
                      label={`Type: ${value}`}
                      onClose={() => toggleFacet('types', value)}
                    />
                  ))}
                  {criteria.categories.map((value) => (
                    <ActiveFilterChip
                      key={`category-${value}`}
                      label={`Category: ${value}`}
                      onClose={() => toggleFacet('categories', value)}
                    />
                  ))}
                  {criteria.subcategories.map((value) => (
                    <ActiveFilterChip
                      key={`subcategory-${value}`}
                      label={`Subcategory: ${value}`}
                      onClose={() => toggleFacet('subcategories', value)}
                    />
                  ))}
                  {criteria.brands.map((value) => (
                    <ActiveFilterChip
                      key={`brand-${value}`}
                      label={`Brand: ${value}`}
                      onClose={() => toggleFacet('brands', value)}
                    />
                  ))}
                  {criteria.colors.map((value) => (
                    <ActiveFilterChip
                      key={`color-${value}`}
                      label={`Color: ${value}`}
                      onClose={() => toggleFacet('colors', value)}
                    />
                  ))}
                  {criteria.sizes.map((value) => (
                    <ActiveFilterChip
                      key={`size-${value}`}
                      label={`Size: ${value}`}
                      onClose={() => toggleFacet('sizes', value)}
                    />
                  ))}
                  {criteria.inStockOnly && (
                    <ActiveFilterChip
                      key='in-stock'
                      label='In stock'
                      onClose={() =>
                        setCriteria((prev) => ({
                          ...prev,
                          page: 1,
                          inStockOnly: false,
                        }))
                      }
                    />
                  )}
                  {criteria.onSaleOnly && (
                    <ActiveFilterChip
                      key='on-sale'
                      label='On sale'
                      onClose={() =>
                        setCriteria((prev) => ({
                          ...prev,
                          page: 1,
                          onSaleOnly: false,
                        }))
                      }
                    />
                  )}
                </div>
              )}
            </CardBody>
          </Card>

          <div className='relative min-h-100'>
            {isPending && (
              <div className='absolute inset-0 z-10 flex items-center justify-center rounded-2xl bg-background/75 backdrop-blur-[2px]'>
                <Spinner size='lg' color='primary' />
              </div>
            )}
            <ProductGrid
              products={initialData.items ?? []}
              loading={isPending && initialData.items.length === 0}
            />
          </div>

          {initialData.pageMetadata.pageCount > 1 && (
            <PaginationBar
              page={initialData.pageMetadata.currentPageNumber}
              totalPages={initialData.pageMetadata.pageCount}
              onChange={(newPage) =>
                setCriteria((prev) => ({ ...prev, page: newPage }))
              }
            />
          )}
        </div>
      </div>
    </section>
  );
};

type ActiveFilterChipProps = {
  label: string;
  onClose: () => void;
};

export const ActiveFilterChip = ({ label, onClose }: ActiveFilterChipProps) => {
  return (
    <Chip size='sm' variant='flat' onClose={onClose} className='text-xs'>
      {label}
    </Chip>
  );
};
