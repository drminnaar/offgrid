import { Card, CardMedia, Skeleton, Stack } from '@mui/material';

export const ProductImageGallerySkeleton = () => {
  return (
    <>
      <Card elevation={3}>
        <CardMedia>
          <Skeleton
            variant='rectangular'
            height={400}
            sx={{ bgcolor: '#f0f0f0' }}
          />
        </CardMedia>
      </Card>
      <Stack
        direction='row'
        spacing={1}
        mt={2}
        justifyContent='center'
        sx={{ flexWrap: 'wrap', gap: 1 }}
      >
        {[...Array(4)].map((_, idx) => (
          <Skeleton key={idx} variant='circular' width={80} height={80} />
        ))}
      </Stack>
    </>
  );
};
