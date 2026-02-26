import { Paper, Skeleton, Stack } from '@mui/material';

export const ProductBasicInfoSkeleton = () => {
  return (
    <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
      <Skeleton variant='rectangular' width={120} height={36} sx={{ mb: 2 }} />
      <Stack spacing={2}>
        <Skeleton variant='text' width={200} />
        <Skeleton variant='text' width={100} />
        <Skeleton variant='text' width={400} />
        <Skeleton variant='text' width={120} />
        <Stack direction='row' spacing={2}>
          <Skeleton variant='rectangular' width={80} height={32} />
          <Skeleton variant='rectangular' width={80} height={32} />
        </Stack>
        <Stack direction='row' spacing={2}>
          <Skeleton variant='rectangular' width={80} height={32} />
          <Skeleton variant='rectangular' width={80} height={32} />
        </Stack>
        <Skeleton variant='text' width={180} />
        <Skeleton variant='text' width={180} />
      </Stack>
    </Paper>
  );
};
