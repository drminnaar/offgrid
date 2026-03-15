import { FacetCount } from '@/services/products';
import { Checkbox } from '@heroui/react';

type FacetSectionProps = {
  title: string;
  items: FacetCount[];
  selectedValues: string[];
  onToggle: (value: string) => void;
};

export const FacetSection = ({
  title,
  items,
  selectedValues,
  onToggle,
}: FacetSectionProps) => {
  if (items.length === 0) {
    return null;
  }

  return (
    <div className='space-y-2'>
      <p className='text-xs font-semibold uppercase tracking-[0.16em] text-default-600'>
        {title}
      </p>
      <div className='space-y-1.5'>
        {items.slice(0, 8).map((item) => (
          <label
            key={`${title}-${item.value}`}
            className='flex cursor-pointer items-center justify-between gap-2 rounded-medium px-2 py-1 transition-colors hover:bg-default-100'
          >
            <Checkbox
              isSelected={selectedValues.includes(item.value)}
              onValueChange={() => onToggle(item.value)}
              size='sm'
            >
              <span className='text-sm text-default-700'>{item.value}</span>
            </Checkbox>
            <span className='text-xs text-default-500'>{item.count}</span>
          </label>
        ))}
      </div>
    </div>
  );
};
