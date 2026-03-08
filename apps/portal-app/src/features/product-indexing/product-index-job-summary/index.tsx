// mui components
import { Box, Divider } from '@mui/material';

// api services
import { useGetRecentProductIndexingJobsQuery } from '../../../services/products/product-api';

// custom components
import { ProductIndexingJobTable } from '../product-indexing-job-table';
import { IndexProductsButton } from '../index-products-button';
import { ProductIndexingJobSummarySkeleton } from './product-index-job-summary-skeleton';

export const ProductIndexJobSummary = () => {
  const {
    data: recentJobs,
    isLoading: isGetRecentProductIndexingJobsLoading,
    isError,
    error,
    refetch,
  } = useGetRecentProductIndexingJobsQuery(10);

  if (isGetRecentProductIndexingJobsLoading)
    return <ProductIndexingJobSummarySkeleton />;
  if (isError) throw error;

  return (
    <>
      <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
        <IndexProductsButton
          onConfirmIndexStarted={() => {
            refetch();
          }}
        />
      </Box>
      <Divider sx={{ my: 2 }} />
      <ProductIndexingJobTable jobs={recentJobs || []} />
    </>
  );
};
