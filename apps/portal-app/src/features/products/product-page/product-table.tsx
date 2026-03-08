// packages
import React, { useState } from 'react';
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
  Collapse,
  Box,
  IconButton,
} from '@mui/material';
import {
  KeyboardArrowDown,
  KeyboardArrowUp,
  Visibility,
} from '@mui/icons-material';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';
import {
  ProductVariantTable,
  ProductVariantTableSkeleton,
  type ProductVariantRowItem,
} from '../shared';

// utils
import { toPlaceholderImage } from '../utils/to-placeholder-image';
import { toEmoji } from '../utils/to-emoji';

type ProductRowItem = {
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

type ProductTableProps = {
  products: ProductRowItem[];
  variants: ProductVariantRowItem[];
  isVariantsLoading: boolean;
  onProductRowClick: (productId: string) => void;
};

export const ProductTable: React.FC<ProductTableProps> = ({
  products,
  variants,
  isVariantsLoading,
  onProductRowClick,
}) => {
  const [expanded, setExpanded] = useState<string | null>(null);

  const handleExpandClick = (productId: string) => {
    setExpanded(expanded === productId ? null : productId);
    onProductRowClick(productId);
  };

  if (!products || products.length === 0) {
    return <AppNoDataAlert message='No products found' />;
  }

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell></TableCell>
            <TableCell></TableCell>
            <TableCell>Type</TableCell>
            <TableCell>Category</TableCell>
            <TableCell>Subcategory</TableCell>
            <TableCell>Brand</TableCell>
            <TableCell>SKU</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Base Price</TableCell>
            <TableCell align='right'>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products.map((product) => (
            <React.Fragment key={product.productId}>
              <TableRow key={product.productId} hover>
                <TableCell>
                  <IconButton
                    size='small'
                    onClick={() => handleExpandClick(product.productId)}
                  >
                    {expanded === product.productId ? (
                      <KeyboardArrowUp />
                    ) : (
                      <KeyboardArrowDown />
                    )}
                  </IconButton>
                </TableCell>
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
                    startIcon={<Visibility />}
                  >
                    View
                  </Button>
                </TableCell>
              </TableRow>
              <TableRow>
                <TableCell
                  style={{ paddingBottom: 0, paddingTop: 0 }}
                  colSpan={10}
                >
                  <Collapse
                    in={expanded === product.productId}
                    timeout='auto'
                    unmountOnExit
                  >
                    <Box margin={1}>
                      {isVariantsLoading ? (
                        <ProductVariantTableSkeleton />
                      ) : (
                        <ProductVariantTable
                          variants={variants}
                          product={product}
                        />
                      )}
                    </Box>
                  </Collapse>
                </TableCell>
              </TableRow>
            </React.Fragment>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
