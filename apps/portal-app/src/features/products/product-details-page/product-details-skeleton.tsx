// packages
import {
  Box,
  Grid,
  Card,
  CardMedia,
  Stack,
  Skeleton,
  Paper,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
} from '@mui/material';

export const ProductDetailsSkeleton = () => (
  <Box sx={{ p: 3, maxWidth: '1400px', mx: 'auto' }}>
    <Skeleton variant='text' width={300} height={40} sx={{ mb: 2 }} />
    <Grid container spacing={4}>
      {/* Left Column: Images */}
      <Grid size={{ xs: 12, md: 6 }}>
        <Card elevation={3}>
          <CardMedia>
            <Skeleton
              variant='rectangular'
              height={400}
              sx={{ bgcolor: '#f0f0f0' }}
            />
          </CardMedia>
        </Card>
        <Stack
          direction='row'
          spacing={1}
          mt={2}
          justifyContent='center'
          sx={{ flexWrap: 'wrap', gap: 1 }}
        >
          {[...Array(4)].map((_, idx) => (
            <Skeleton key={idx} variant='circular' width={80} height={80} />
          ))}
        </Stack>
      </Grid>
      {/* Right Column: Details */}
      <Grid size={{ xs: 12, md: 6 }}>
        {/* Basic Info */}
        <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
          <Skeleton
            variant='rectangular'
            width={120}
            height={36}
            sx={{ mb: 2 }}
          />
          <Stack spacing={2}>
            <Skeleton variant='text' width={200} />
            <Skeleton variant='text' width={100} />
            <Skeleton variant='text' width={400} />
            <Skeleton variant='text' width={120} />
            <Stack direction='row' spacing={2}>
              <Skeleton variant='rectangular' width={80} height={32} />
              <Skeleton variant='rectangular' width={80} height={32} />
            </Stack>
            <Stack direction='row' spacing={2}>
              <Skeleton variant='rectangular' width={80} height={32} />
              <Skeleton variant='rectangular' width={80} height={32} />
            </Stack>
            <Skeleton variant='text' width={180} />
            <Skeleton variant='text' width={180} />
          </Stack>
        </Paper>
        {/* Features */}
        <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
          <Skeleton variant='text' width={120} />
          {[...Array(4)].map((_, idx) => (
            <Skeleton key={idx} variant='text' width={300} />
          ))}
        </Paper>
        {/* Specifications */}
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
      </Grid>
    </Grid>
    {/* Variants Table */}
    <Box mt={5}>
      <Skeleton variant='text' width={220} />
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
    </Box>
  </Box>
);
