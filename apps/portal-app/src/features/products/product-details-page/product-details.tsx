// packages
import { useState } from 'react';
import { format, toDate } from 'date-fns';
import { useNavigate, useParams } from 'react-router';
import {
  Box,
  Typography,
  Chip,
  Divider,
  Grid,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Avatar,
  Badge,
  List,
  ListItem,
  ListItemText,
  Card,
  CardMedia,
  IconButton,
  Tooltip,
  Button,
} from '@mui/material';

// utils
import { toPlaceholderImage } from '../utils/to-placeholder-image';
import { toCurrency } from '../../../lib/utils';

// api
import { useGetProductByIdQuery } from '../../../services/products/product-api';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';
import type { ProductVariant } from '../../../services/products/types';
import { ProductDetailsSkeleton } from './product-details-skeleton';

export const ProductDetails = () => {
  const navigate = useNavigate();
  const { productId } = useParams<{ productId: string }>();
  const [selectedVariant, setSelectedVariant] = useState<ProductVariant | null>(
    null,
  );

  const {
    data: product,
    isLoading,
    isError,
    error,
  } = useGetProductByIdQuery(productId!);

  if (isLoading) return <ProductDetailsSkeleton />;
  if (isError) throw error;

  if (!product) return <AppNoDataAlert message='Product not found' />;

  const updatedAt = toDate(product.updatedAtUnixTimeSeconds);
  const createdAt = toDate(product.createdAtUnixTimeSeconds);

  return (
    <Box sx={{ p: 3, maxWidth: '1400px', mx: 'auto' }}>
      <Typography variant='h4' gutterBottom fontWeight='bold'>
        {product.name}
      </Typography>

      <Grid container spacing={4}>
        {/* Left Column: Images */}
        <Grid size={{ xs: 12, md: 6 }}>
          <Card elevation={3}>
            <CardMedia
              component='img'
              image={toPlaceholderImage(
                selectedVariant?.imageUrl ||
                  product.variants[0]?.imageUrl ||
                  product.primaryImageUrl,
              )}
              alt={product.name}
              sx={{
                height: 400,
                objectFit: 'scale-down',
                bgcolor: selectedVariant
                  ? selectedVariant.attributes.ColorHex
                  : '#f0f0f0',
              }}
            />
          </Card>

          <Stack
            direction='row'
            spacing={1}
            mt={2}
            justifyContent='center'
            sx={{ flexWrap: 'wrap', gap: 1 }}
          >
            {product.variants?.length > 0 &&
              product.variants.map((variant, idx) => (
                <Tooltip key={idx} title={`Gallery Image ${idx + 1}`}>
                  <IconButton onClick={() => setSelectedVariant(variant)}>
                    <Avatar
                      variant='rounded'
                      src={toPlaceholderImage(variant.imageUrl)}
                      sx={{
                        bgcolor: variant.attributes.ColorHex || '#000000',
                        width: 80,
                        height: 80,
                        border:
                          selectedVariant === variant
                            ? '3px solid #1976d2'
                            : 'none',
                      }}
                    />
                  </IconButton>
                </Tooltip>
              ))}
          </Stack>
        </Grid>

        {/* Right Column: Details */}
        <Grid size={{ xs: 12, md: 6 }}>
          {/* Basic Info */}
          <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
            <Button
              variant='contained'
              onClick={() => navigate('/products')}
              color='secondary'
              sx={{ mb: 2 }}
            >
              Back to Products
            </Button>
            <Stack spacing={2}>
              <Box display='flex' alignItems='center' gap={2}>
                <Typography variant='h6'>SKU:</Typography>
                <Typography variant='subtitle1' color='text.secondary'>
                  {product.sku}
                </Typography>
                {product.isOnSale && (
                  <Chip
                    label={`-${product.salePercentage}% SALE`}
                    color='error'
                    size='small'
                  />
                )}
              </Box>

              <Divider />

              <Typography variant='body1' color='text.secondary'>
                {product.description}
              </Typography>

              <Box>
                <Typography variant='h5' fontWeight='bold' color='primary'>
                  {toCurrency(product.currentPrice)}
                </Typography>
                {product.isOnSale && (
                  <Typography
                    variant='body2'
                    sx={{
                      textDecoration: 'line-through',
                      color: 'text.secondary',
                    }}
                  >
                    {toCurrency(product.basePrice)}
                  </Typography>
                )}
              </Box>

              <Stack direction='row' spacing={2}>
                <Chip label={`Type: ${product.type}`} />
                <Chip label={`Brand: ${product.brand}`} />
              </Stack>
              <Stack direction='row' spacing={2}>
                <Chip label={`Category: ${product.category}`} />
                <Chip label={`Subcategory: ${product.subcategory}`} />
              </Stack>

              <Typography variant='caption' color='text.secondary'>
                Create At: {format(createdAt, 'PPPp')}
              </Typography>
              <Typography variant='caption' color='text.secondary'>
                Last updated: {format(updatedAt, 'PPPp')}
              </Typography>
            </Stack>
          </Paper>

          {/* Features */}
          <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
            <Typography variant='h6' gutterBottom>
              Key Features
            </Typography>
            <List dense>
              {product.features.map((feature, idx) => (
                <ListItem key={idx} disablePadding>
                  <ListItemText primary={`• ${feature}`} />
                </ListItem>
              ))}
            </List>
          </Paper>

          {/* Specifications */}
          <Paper sx={{ p: 3 }} elevation={2}>
            <Typography variant='h6' gutterBottom>
              Specifications
            </Typography>
            <TableContainer>
              <Table size='small'>
                <TableBody>
                  {Object.entries(product.specifications).map(
                    ([key, value]) => (
                      <TableRow key={key}>
                        <TableCell
                          component='th'
                          scope='row'
                          sx={{ fontWeight: 'medium' }}
                        >
                          {key}
                        </TableCell>
                        <TableCell align='right'>{value}</TableCell>
                      </TableRow>
                    ),
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
        </Grid>
      </Grid>

      {/* Variants Table */}
      <Box mt={5}>
        <Typography variant='h5' gutterBottom>
          Product Variants ({product.variants.length})
        </Typography>

        <TableContainer component={Paper} elevation={3}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Image</TableCell>
                <TableCell>Variant SKU</TableCell>
                <TableCell>Variant Name</TableCell>
                <TableCell>Color</TableCell>
                <TableCell>Package</TableCell>
                <TableCell>Price Modifier</TableCell>
                <TableCell>Final Price</TableCell>
                <TableCell>Stock</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {product.variants.map((variant) => {
                const modifier = variant.priceModifier;
                const finalPrice = product.currentPrice + modifier;
                const colorHex = variant.attributes.ColorHex || '#000000';

                return (
                  <TableRow key={variant.sku} hover>
                    <TableCell>
                      <Avatar
                        variant='rounded'
                        src={toPlaceholderImage(variant.imageUrl)}
                        sx={{
                          backgroundColor:
                            variant.attributes.ColorHex || '#ffffff',
                          width: 60,
                          height: 60,
                          alignItems: 'center',
                          fontSize: 10,
                        }}
                      >
                        {variant.attributes.Color}
                      </Avatar>
                    </TableCell>
                    <TableCell>{variant.sku}</TableCell>
                    <TableCell>{variant.name}</TableCell>
                    <TableCell>
                      <Box display='flex' alignItems='center' gap={1}>
                        <Box
                          sx={{
                            width: 20,
                            height: 20,
                            bgcolor: colorHex,
                            border: '1px solid #ccc',
                            borderRadius: '4px',
                          }}
                        />
                        {variant.attributes.Color}
                      </Box>
                    </TableCell>
                    <TableCell>{variant.attributes.Package}</TableCell>
                    <TableCell>
                      {variant.priceModifier > 0 ? '+' : ''}
                      {toCurrency(variant.priceModifier)}
                    </TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>
                      {toCurrency(finalPrice)}
                    </TableCell>
                    <TableCell>
                      <Badge
                        color={
                          variant.stockQuantity > 20
                            ? 'success'
                            : variant.stockQuantity > 0
                              ? 'warning'
                              : 'error'
                        }
                        badgeContent={variant.stockQuantity}
                        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                      >
                        <Chip
                          label={
                            variant.stockQuantity > 20
                              ? 'In Stock'
                              : variant.stockQuantity > 0
                                ? 'Low Stock'
                                : 'Out of Stock'
                          }
                          color={
                            variant.stockQuantity > 20
                              ? 'success'
                              : variant.stockQuantity > 0
                                ? 'warning'
                                : 'error'
                          }
                          size='small'
                        />
                      </Badge>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Box>
  );
};
