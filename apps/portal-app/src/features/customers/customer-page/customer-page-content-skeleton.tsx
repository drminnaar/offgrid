// packages
import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Skeleton,
} from '@mui/material';

export const CustomerPageContentSkeleton = () => {
  return (
    <>
      {/* Filter Skeleton */}
      <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
        <Skeleton variant='rectangular' width={120} height={40} />
        <Skeleton variant='rectangular' width={100} height={36} />
      </Box>

      {/* Table Skeleton */}
      <TableContainer component={Paper} className='shadow-md rounded-lg'>
        <Table>
          <TableHead>
            <TableRow className='bg-gray-100'>
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
              <TableCell>
                <Skeleton variant='text' />
              </TableCell>
              <TableCell>
                <Skeleton variant='text' />
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {Array.from({ length: 5 }).map((_, index) => (
              <TableRow key={index} className='hover:bg-gray-50'>
                <TableCell>
                  <Skeleton variant='circular' width={40} height={40} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={100} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={120} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={150} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={100} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={150} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={120} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={120} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='text' width={120} />
                </TableCell>
                <TableCell>
                  <Skeleton variant='rectangular' width={40} height={40} />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Pagination Skeleton */}
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
        <Skeleton variant='rectangular' width={300} height={40} />
      </Box>
    </>
  );
};
