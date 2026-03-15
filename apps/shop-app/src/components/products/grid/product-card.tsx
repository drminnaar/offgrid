'use client';

import Image from 'next/image';
import { Button, Card, CardBody, CardFooter, Chip } from '@heroui/react';

export type ProductCardData = {
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
  // pricing
  isOnSale: boolean;
  salePercentage: number;
  basePrice: number;
  currentPrice: number;
  // variant
  color: string;
  colorHex: string;
  size?: string;
  package?: string;
  buildKit?: string;
  finSetup?: string;
  // stock
  totalStock: number;
  hasStock: boolean;
  // meta
  imageUrl?: string;
};

type Props = {
  product: ProductCardData;
  priority?: boolean;
};

const CATEGORY_ICON: Record<string, string> = {
  Kayak: '🛶',
  Bike: '🚵',
  Surfboard: '🏄',
};

export const toPlaceholderImage = (url: string) => {
  if (
    !url ||
    url.trim().length === 0 ||
    url.trim().toLowerCase().includes('example')
  ) {
    return '/placeholder.png';
  }
  return url;
};

export const ProductCard = ({ product: p, priority = false }: Props) => {
  const product = { ...p, imageUrl: toPlaceholderImage(p.imageUrl ?? '') };
  const icon = CATEGORY_ICON[product.type] ?? '🏔️';
  const hasImg = Boolean(product.imageUrl);

  return (
    <Card
      shadow='sm'
      radius='lg'
      className='group border border-divider bg-content1 transition-all duration-300 hover:-translate-y-0.5 hover:border-primary/40 hover:shadow-md'
    >
      <div className='relative h-52 overflow-hidden bg-linear-to-br from-default-100 to-content1'>
        {hasImg ? (
          <Image
            src={product.imageUrl!}
            alt={product.variantName}
            width={400}
            height={208}
            className='h-full w-full object-contain p-3 transition-transform duration-500 group-hover:scale-105'
            priority={priority}
          />
        ) : (
          <div className='flex h-full w-full items-center justify-center'>
            <span className='select-none text-8xl opacity-70 transition-opacity group-hover:opacity-45'>
              {icon}
            </span>
          </div>
        )}

        <div className='absolute left-3 top-3 flex flex-col gap-1.5'>
          {product.isOnSale && (
            <Chip
              color='danger'
              variant='solid'
              size='sm'
              className='text-[10px]'
            >
              -{product.salePercentage}%
            </Chip>
          )}
          {!product.hasStock && (
            <Chip
              variant='flat'
              color='default'
              size='sm'
              className='text-[10px]'
            >
              SOLD OUT
            </Chip>
          )}
        </div>

        <div className='absolute right-3 top-3'>
          <div
            className='h-4 w-4 rounded-full border-2 border-white/80 shadow-sm ring-1 ring-black/10'
            style={{ backgroundColor: product.colorHex }}
            title={product.color}
          />
        </div>
      </div>

      <CardBody className='flex flex-1 flex-col gap-2 p-4'>
        <div className='flex items-center justify-between gap-2'>
          <span className='truncate text-[10px] font-semibold uppercase tracking-[0.16em] text-default-600'>
            {product.brand}
          </span>
          <Chip variant='flat' size='sm' className='max-w-[48%] text-[10px]'>
            {product.subcategory}
          </Chip>
        </div>

        <h3 className='line-clamp-2 text-sm font-semibold leading-snug text-foreground'>
          {product.variantName}
        </h3>

        <div className='flex flex-wrap gap-1.5'>
          {product.size && <SpecChip label={`Size ${product.size}`} />}
          {product.finSetup && <SpecChip label={product.finSetup} />}
          {product.buildKit && <SpecChip label={product.buildKit} />}
          {product.package && <SpecChip label={product.package} />}
        </div>

        <div className='mt-auto flex items-baseline gap-2 pt-2'>
          <span className='text-xl font-bold text-foreground'>
            ${product.currentPrice.toFixed(2)}
          </span>
          {product.isOnSale && (
            <span className='text-xs text-default-500 line-through'>
              ${product.basePrice.toFixed(2)}
            </span>
          )}
        </div>
      </CardBody>

      <CardFooter className='p-4 pt-0'>
        <Button
          disabled={!product.hasStock}
          color={product.hasStock ? 'primary' : 'default'}
          variant={product.hasStock ? 'solid' : 'flat'}
          className='w-full font-semibold tracking-wide'
        >
          {product.hasStock ? 'ADD TO CART' : 'NOTIFY ME'}
        </Button>
      </CardFooter>
    </Card>
  );
};

const SpecChip = ({ label }: { label: string }) => {
  return (
    <Chip variant='flat' size='sm' className='text-[10px]'>
      {label}
    </Chip>
  );
};
