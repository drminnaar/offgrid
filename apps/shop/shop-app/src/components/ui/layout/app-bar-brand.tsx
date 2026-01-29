'use client';

import { NavbarBrand } from '@heroui/react';
import { MountainSnow } from 'lucide-react';

export type AppTopBarBrandProps = {
  className?: string;
};

export const AppBarBrand = ({ className }: AppTopBarBrandProps) => {
  return (
    <NavbarBrand className={className}>
      <MountainSnow size={32} color='#338cf1' />
      <p className='font-bold text-inherit' style={{ color: '#338cf1' }}>
        OFFGRID
      </p>
    </NavbarBrand>
  );
};
