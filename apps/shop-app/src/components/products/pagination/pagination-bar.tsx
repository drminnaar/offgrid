import { Pagination } from '@heroui/react';
import React from 'react';

type Props = {
  page: number;
  totalPages: number;
  onChange: (page: number) => void;
};

export const PaginationBar: React.FC<Props> = ({
  page,
  totalPages,
  onChange,
}) => {
  return (
    <div className='flex justify-center pt-2'>
      <Pagination
        total={totalPages}
        page={page}
        onChange={onChange}
        showControls
        color='primary'
        variant='flat'
        classNames={{
          wrapper: 'gap-1.5',
          item: 'border border-divider bg-content1 text-default-700 data-[hover=true]:bg-default-100',
          cursor: 'border border-primary bg-primary text-primary-foreground',
          prev: 'border border-divider bg-content1 text-default-700 data-[hover=true]:bg-default-100',
          next: 'border border-divider bg-content1 text-default-700 data-[hover=true]:bg-default-100',
        }}
      />
    </div>
  );
};
