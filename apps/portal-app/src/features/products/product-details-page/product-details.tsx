// packages
import { useNavigate, useParams } from 'react-router';
import { Box, Typography, Grid } from '@mui/material';

// api
import { useGetProductByIdQuery } from '../../../services/products/product-api';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';
import { ProductDetailsSkeleton } from './product-details-skeleton';
import { ProductVariantTable } from '../shared';
import { ProductSpecifications } from './product-specifications';
import { ProductFeatures } from './product-features';
import { ProductBasicInfo } from './product-basic-info';
import { ProductImageGallery } from './product-image-gallery';

export const ProductDetails = () => {
  const navigate = useNavigate();
  const { productId } = useParams<{ productId: string }>();

  const {
    data: product,
    isLoading,
    isError,
    error,
  } = useGetProductByIdQuery(productId!);

  if (isLoading) return <ProductDetailsSkeleton />;
  if (isError) throw error;

  if (!product) return <AppNoDataAlert message='Product not found' />;

  return (
    <Box sx={{ p: 3, maxWidth: '1400px', mx: 'auto' }}>
      <Typography variant='h4' gutterBottom fontWeight='bold'>
        {product.name}
      </Typography>

      <Grid container spacing={4}>
        {/* Left Column: Images */}
        <Grid size={{ xs: 12, md: 6 }}>
          <ProductImageGallery
            images={[
              {
                url: product.primaryImageUrl,
                colorHex: '#f0f0f0',
                isPrimary: true,
              },
              ...product.variants.map((v) => ({
                url: v.imageUrl,
                colorHex: v.attributes.colorHex || '#f0f0f0',
                isPrimary: false,
              })),
            ]}
          />
        </Grid>

        {/* Right Column: Details */}
        <Grid size={{ xs: 12, md: 6 }}>
          <ProductBasicInfo
            product={product}
            onBack={() => navigate('/products')}
          />
          <ProductFeatures features={product.features} />
          <ProductSpecifications specifications={product.specifications} />
        </Grid>
      </Grid>

      {/* Variants Table */}
      <Box mt={5}>
        <Typography variant='h5' gutterBottom>
          Product Variants ({product.variants.length})
        </Typography>
        <ProductVariantTable variants={product.variants} product={product} />
      </Box>
    </Box>
  );
};
