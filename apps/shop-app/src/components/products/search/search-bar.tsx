'use client';

import React from 'react';
import { Input } from '@heroui/react';
import { Search as SearchIcon } from 'lucide-react';

type Props = {
  value: string;
  onSearchTextChange: (text: string) => void;
};

export const SearchBar: React.FC<Props> = ({ value, onSearchTextChange }) => {
  const handleSearchTextChange = (text: string) => {
    onSearchTextChange(text);
  };

  return (
    <Input
      type='search'
      value={value}
      onValueChange={handleSearchTextChange}
      placeholder='Search products, brands, categories...'
      size='lg'
      radius='lg'
      startContent={<SearchIcon className='h-4 w-4 text-default-500' />}
      endContent={
        value ? (
          <button
            type='button'
            onClick={() => handleSearchTextChange('')}
            className='text-default-500 transition-colors hover:text-default-700'
            aria-label='Clear search'
          ></button>
        ) : null
      }
      classNames={{
        base: 'w-full',
        inputWrapper:
          'border border-divider bg-content1 shadow-sm data-[hover=true]:border-primary/50 group-data-[focus=true]:border-primary',
      }}
    />
  );
};
