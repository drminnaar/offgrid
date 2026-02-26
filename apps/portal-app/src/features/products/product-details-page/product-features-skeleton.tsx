import { Paper, Skeleton } from '@mui/material';

export const ProductFeaturesSkeleton = () => {
  return (
    <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
      <Skeleton variant='text' width={120} />
      {[...Array(4)].map((_, idx) => (
        <Skeleton key={idx} variant='text' width={300} />
      ))}
    </Paper>
  );
};
