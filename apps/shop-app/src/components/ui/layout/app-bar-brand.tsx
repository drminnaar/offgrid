'use client';

import { NavbarBrand } from '@heroui/react';
import { MountainSnow } from 'lucide-react';
import { useRouter } from 'next/navigation';

export type AppTopBarBrandProps = {
  className?: string;
};

export const AppBarBrand = ({ className }: AppTopBarBrandProps) => {
  const router = useRouter();
  const classNames = (className ?? '') + ' cursor-pointer';
  return (
    <NavbarBrand className={classNames} onClick={() => router.push('/')}>
      <MountainSnow size={32} color='#338cf1' />
      <p className='font-bold text-inherit' style={{ color: '#338cf1' }}>
        OFFGRID
      </p>
    </NavbarBrand>
  );
};
