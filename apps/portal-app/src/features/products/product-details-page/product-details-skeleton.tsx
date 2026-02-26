// packages
import { Box, Grid, Skeleton } from '@mui/material';

// custom components
import { ProductBasicInfoSkeleton } from './product-basic-info-skeleton';
import { ProductFeaturesSkeleton } from './product-features-skeleton';
import { ProductSpecificationsSkeleton } from './product-specifications-skeleton';
import { ProductVariantTableSkeleton } from '../shared';
import { ProductImageGallerySkeleton } from './product-image-gallery-skeleton';

export const ProductDetailsSkeleton = () => (
  <Box sx={{ p: 3, maxWidth: '1400px', mx: 'auto' }}>
    <Skeleton variant='text' width={300} height={40} sx={{ mb: 2 }} />
    <Grid container spacing={4}>
      {/* Left Column: Images */}
      <Grid size={{ xs: 12, md: 6 }}>
        <ProductImageGallerySkeleton />
      </Grid>
      {/* Right Column: Details */}
      <Grid size={{ xs: 12, md: 6 }}>
        {/* Basic Info */}
        <ProductBasicInfoSkeleton />
        {/* Features */}
        <ProductFeaturesSkeleton />
        {/* Specifications */}
        <ProductSpecificationsSkeleton />
      </Grid>
    </Grid>
    {/* Variants Table */}
    <Box mt={5}>
      <Skeleton variant='text' width={220} />
      <ProductVariantTableSkeleton />
    </Box>
  </Box>
);
