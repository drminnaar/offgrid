import {
  Paper,
  Button,
  Stack,
  Box,
  Typography,
  Chip,
  Divider,
} from '@mui/material';
import { format, toDate } from 'date-fns';

import { toCurrency } from '../../../lib/utils';

type ProductBasicInfoProps = {
  product: {
    sku: string;
    isOnSale: boolean;
    salePercentage: number;
    description: string;
    currentPrice: number;
    basePrice: number;
    type: string;
    brand: string;
    category: string;
    subcategory: string;
    createdAtUnixTimeSeconds: number;
    updatedAtUnixTimeSeconds: number;
  };
  onBack: () => void;
};

export const ProductBasicInfo = ({
  product,
  onBack,
}: ProductBasicInfoProps) => {
  const updatedAt = toDate(product.updatedAtUnixTimeSeconds);
  const createdAt = toDate(product.createdAtUnixTimeSeconds);
  return (
    <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
      <Button
        variant='contained'
        onClick={onBack}
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
          Created At: {format(createdAt, 'PPPp')}
        </Typography>

        <Typography variant='caption' color='text.secondary'>
          Last updated: {format(updatedAt, 'PPPp')}
        </Typography>
      </Stack>
    </Paper>
  );
};
