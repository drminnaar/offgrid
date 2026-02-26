import React from 'react';
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Avatar,
  Box,
  Badge,
  Chip,
} from '@mui/material';

// utils
import { toCurrency } from '../../../lib/utils';
import { toPlaceholderImage } from '../utils/to-placeholder-image';

export type ProductVariantRowItem = {
  sku: string;
  name: string;
  priceModifier: number;
  attributes: Record<string, string>;
  stockQuantity: number;
  imageUrl: string;
};

type ProductVariantTableProps = {
  variants: ProductVariantRowItem[];
  product: {
    currentPrice: number;
  };
};

export const ProductVariantTable: React.FC<ProductVariantTableProps> = ({
  variants,
  product,
}) => {
  return (
    <TableContainer component={Paper} elevation={3}>
      <Table size='small'>
        <TableHead sx={{ bgcolor: '#64B5F6' }}>
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
          {variants?.map((variant) => {
            const modifier = variant.priceModifier;
            const finalPrice = product.currentPrice + modifier;
            const colorHex = variant.attributes.ColorHex || '#000000';

            return (
              <TableRow key={variant.sku} hover>
                <TableCell>
                  <Avatar
                    variant='circular'
                    src={toPlaceholderImage(variant.imageUrl)}
                    sx={{
                      backgroundColor: variant.attributes.ColorHex || '#ffffff',
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
                    anchorOrigin={{
                      vertical: 'top',
                      horizontal: 'right',
                    }}
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
  );
};
