import {
  Paper,
  Typography,
  TableContainer,
  Table,
  TableBody,
  TableRow,
  TableCell,
} from '@mui/material';

type ProductSpecificationsProps = {
  specifications: Record<string, string>;
};

export const ProductSpecifications = ({
  specifications,
}: ProductSpecificationsProps) => {
  return (
    <Paper sx={{ p: 3 }} elevation={2}>
      <Typography variant='h6' gutterBottom>
        Specifications
      </Typography>
      <TableContainer>
        <Table size='small'>
          <TableBody>
            {Object.entries(specifications).map(([key, value]) => (
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
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Paper>
  );
};
