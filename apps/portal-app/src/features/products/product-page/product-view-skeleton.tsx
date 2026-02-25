// packages
import { Box, Skeleton } from '@mui/material';

// custom components
import { ProductTableSkeleton } from './product-table-skeleton';
import { ProductFiltersSkeleton } from './product-filters-skeleton';

export const ProductViewSkeleton = () => (
  <Box>
    {/* Filters skeleton */}
    <Box mb={2}>
      <ProductFiltersSkeleton />
    </Box>

    {/* Table skeleton */}
    <Box mb={2}>
      <ProductTableSkeleton rows={10} />
    </Box>

    {/* Pagination skeleton */}
    <Box display='flex' justifyContent='center'>
      <Skeleton variant='rectangular' height={40} width={200} />
    </Box>
  </Box>
);
