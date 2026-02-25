import { Box, Skeleton } from '@mui/material';

export const ProductFiltersSkeleton = () => (
  <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
    <Skeleton variant='rectangular' width={200} height={40} />
    <Skeleton variant='rectangular' width={200} height={40} sx={{ ml: 2 }} />
    <Skeleton variant='rectangular' width={200} height={40} sx={{ ml: 2 }} />
    <Skeleton variant='rectangular' width={100} height={40} sx={{ ml: 2 }} />
  </Box>
);
