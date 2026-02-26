import React from 'react';
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Skeleton,
} from '@mui/material';

export const ProductTableSkeleton: React.FC<{ rows?: number }> = ({
  rows = 10,
}) => (
  <TableContainer component={Paper}>
    <Table>
      <TableHead>
        <TableRow>
          <TableCell></TableCell>
          <TableCell></TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell>
            <Skeleton variant='text' />
          </TableCell>
          <TableCell align='right'>
            <Skeleton variant='text' />
          </TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {Array.from({ length: rows }).map((_, idx) => (
          <TableRow key={idx}>
            <TableCell></TableCell>
            <TableCell>
              <Skeleton variant='rectangular' width={40} height={40} />
            </TableCell>
            <TableCell>
              <Skeleton width={60} />
            </TableCell>
            <TableCell>
              <Skeleton width={80} />
            </TableCell>
            <TableCell>
              <Skeleton width={80} />
            </TableCell>
            <TableCell>
              <Skeleton width={80} />
            </TableCell>
            <TableCell>
              <Skeleton width={60} />
            </TableCell>
            <TableCell>
              <Skeleton width={120} />
            </TableCell>
            <TableCell>
              <Skeleton width={60} />
            </TableCell>
            <TableCell align='right'>
              <Skeleton width={60} />
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer>
);
