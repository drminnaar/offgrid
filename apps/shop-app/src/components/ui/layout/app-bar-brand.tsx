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
      {/* <MountainSnow size={32} color='#338cf1' /> */}
      <div
        style={{
          width: 32,
          height: 32,
          borderRadius: 8,
          background: 'linear-gradient(135deg, #00C2A8, #FF6B35)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          fontSize: 16,
        }}
      >
        <MountainSnow color='#fff' />
      </div>
      {/* <p className='font-bold text-inherit mx-2' style={{ color: '#338cf1' }}>
        
      </p> */}
      <div
        style={{
          borderRadius: 8,
          background: 'linear-gradient(135deg, #00C2A8, #FF6B35)',
          backgroundClip: 'text',
          WebkitBackgroundClip: 'text',
          color: 'transparent',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          fontSize: 30,
          fontWeight: 'bold',
          marginLeft: 4,
        }}
      >
        OFFGRID
      </div>
    </NavbarBrand>
  );
};
