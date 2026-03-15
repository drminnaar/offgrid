'use client';

import { ProductCard, type ProductCardData } from './product-card';

type Props = {
  products: ProductCardData[];
  loading?: boolean;
};

export const ProductGrid = ({ products, loading }: Props) => {
  if (loading) {
    return (
      <div className='grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-4'>
        {Array.from({ length: 12 }).map((_, i) => (
          <SkeletonCard key={i} />
        ))}
      </div>
    );
  }

  if (products.length === 0) {
    return (
      <div className='flex flex-col items-center justify-center gap-4 rounded-2xl border border-dashed border-divider bg-content2/40 px-6 py-20 text-center'>
        <div className='text-default-300'>
          <svg
            className='h-16 w-16'
            fill='none'
            viewBox='0 0 24 24'
            stroke='currentColor'
          >
            <circle cx='11' cy='11' r='7' strokeWidth={1.2} />
            <path
              strokeLinecap='round'
              strokeWidth={1.2}
              d='M21 21l-4.35-4.35'
            />
            <path strokeLinecap='round' strokeWidth={1.2} d='M8 11h6M11 8v6' />
          </svg>
        </div>
        <p className='text-xl font-semibold tracking-tight text-foreground'>
          No products found
        </p>
        <p className='text-sm text-default-600'>
          Try a different search or clear your filters
        </p>
      </div>
    );
  }

  return (
    <div className='grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-4'>
      {products.map((p, i) => (
        <ProductCard key={p.id} product={p} priority={i < 4} />
      ))}
    </div>
  );
};

function SkeletonCard() {
  return (
    <div className='animate-pulse overflow-hidden rounded-2xl border border-divider bg-content1'>
      <div className='h-52 bg-default-100' />
      <div className='p-4 space-y-3'>
        <div className='h-2.5 w-1/3 rounded bg-default-100' />
        <div className='h-4 w-3/4 rounded bg-default-100' />
        <div className='h-3 w-1/2 rounded bg-default-100' />
        <div className='mt-2 h-6 w-1/4 rounded bg-default-100' />
      </div>
    </div>
  );
}
