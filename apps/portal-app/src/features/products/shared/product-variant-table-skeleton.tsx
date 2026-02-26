// packages
import React from 'react';
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  Skeleton,
  TableBody,
} from '@mui/material';

export const ProductVariantTableSkeleton: React.FC = () => {
  return (
    <TableContainer component={Paper} elevation={3}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>
              <Skeleton variant='text' width={60} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={100} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={100} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={80} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={80} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={100} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={100} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={80} />
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {[...Array(3)].map((_, idx) => (
            <TableRow key={idx}>
              {[...Array(8)].map((_, colIdx) => (
                <TableCell key={colIdx}>
                  <Skeleton variant='text' width={80} />
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
