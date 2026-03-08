// mui components
import { Box, Divider, Skeleton } from '@mui/material';

// custom components
import { ProductIndexingJobTableSkeleton } from '../product-indexing-job-table/product-indexing-job-table-skeleton';

export const ProductIndexingJobSummarySkeleton = () => {
  return (
    <>
      <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
        <Skeleton variant='rectangular' width={180} height={40} />
      </Box>
      <Divider sx={{ my: 2 }} />
      <ProductIndexingJobTableSkeleton />
    </>
  );
};
