import {
  Paper,
  Skeleton,
  TableContainer,
  Table,
  TableBody,
  TableRow,
  TableCell,
} from '@mui/material';

export const ProductSpecificationsSkeleton = () => {
  return (
    <Paper sx={{ p: 3 }} elevation={2}>
      <Skeleton variant='text' width={120} />
      <TableContainer>
        <Table size='small'>
          <TableBody>
            {[...Array(4)].map((_, idx) => (
              <TableRow key={idx}>
                <TableCell>
                  <Skeleton variant='text' width={100} />
                </TableCell>
                <TableCell align='right'>
                  <Skeleton variant='text' width={100} />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Paper>
  );
};
