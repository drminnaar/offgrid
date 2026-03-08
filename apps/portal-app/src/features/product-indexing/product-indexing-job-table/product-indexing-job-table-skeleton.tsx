// react packages
import React from 'react';

// mui packages
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

export const ProductIndexingJobTableSkeleton: React.FC = () => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow sx={{ backgroundColor: '#f5f5f5' }}>
            <TableCell>
              <Skeleton variant='text' width={80} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={100} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={120} />
            </TableCell>
            <TableCell>
              <Skeleton variant='text' width={120} />
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {[...Array(5)].map((_, idx) => (
            <TableRow key={idx}>
              <TableCell>
                <Skeleton variant='text' width={100} />
              </TableCell>
              <TableCell>
                <Skeleton variant='rectangular' width={80} height={32} />
              </TableCell>
              <TableCell>
                <Skeleton variant='text' width={120} />
              </TableCell>
              <TableCell>
                <Skeleton variant='text' width={120} />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
