// packages
import React from 'react';
import { Link } from 'react-router';
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
  Avatar,
} from '@mui/material';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';

// utils
import { toPlaceholderImage } from '../utils/to-placeholder-image';
import { toEmoji } from '../utils/to-emoji';

export type ProductRowItem = {
  productId: string;
  sku: string;
  name: string;
  description: string;
  basePrice: number;
  currentPrice: number;
  isOnSale: boolean;
  salePercentage: number;
  totalStockQuantity: number;
  stockLevel: string;
  brand: string;
  category: string;
  subcategory: string;
  type: string;
  primaryImageUrl: string;
  createdAtUnixTimeSeconds: number;
  updatedAtUnixTimeSeconds: number;
};

export type ProductTableProps = {
  products: ProductRowItem[];
};

export const ProductTable: React.FC<ProductTableProps> = ({ products }) => {
  if (!products || products.length === 0) {
    return <AppNoDataAlert message='No products found' />;
  }

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell></TableCell>
            <TableCell>Type</TableCell>
            <TableCell>Category</TableCell>
            <TableCell>Subcategory</TableCell>
            <TableCell>Brand</TableCell>
            <TableCell>Stock Level</TableCell>
            <TableCell>SKU</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Base Price</TableCell>
            <TableCell align='right'>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products.map((product) => (
            <TableRow key={product.productId} hover>
              <TableCell>
                <Avatar
                  variant='circular'
                  src={toPlaceholderImage(product.primaryImageUrl)}
                  sx={{
                    backgroundColor: '#BDBDBD',
                    width: 40,
                    height: 40,
                    alignItems: 'center',
                    fontSize: 1,
                  }}
                ></Avatar>
              </TableCell>
              <TableCell>{`${toEmoji(product.type)} ${
                product.type
              }`}</TableCell>
              <TableCell>{product.category}</TableCell>
              <TableCell>{product.subcategory}</TableCell>
              <TableCell>{product.brand}</TableCell>
              <TableCell
                sx={{
                  color:
                    product.stockLevel === 'In Stock'
                      ? '#4CAF50'
                      : product.stockLevel === 'Limited Stock'
                        ? '#FF9800'
                        : '#F44336',
                }}
              >
                {`${product.stockLevel} (${product.totalStockQuantity})`}
              </TableCell>
              <TableCell>{product.sku}</TableCell>
              <TableCell>{product.name}</TableCell>
              <TableCell>{product.basePrice}</TableCell>
              <TableCell align='right'>
                <Button
                  size='small'
                  variant='outlined'
                  color='primary'
                  component={Link}
                  to={`/products/${product.productId}`}
                >
                  View
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
