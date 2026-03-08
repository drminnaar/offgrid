// mui packages
import React from 'react';
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Chip,
  Box,
  Typography,
} from '@mui/material';
import { Info } from '@mui/icons-material';

// util packages
import { format, isValid, parseISO } from 'date-fns';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';

// custom types
import type { IndexProductStatus } from '../../../services/products/types';

export type IndexingJobRowItem = {
  jobId: string;
  createdAt: string;
  completedAt: string | null;
  status: IndexProductStatus;
};

type IndexingJobTableProps = {
  jobs: IndexingJobRowItem[];
};

const getStatusChipColor = (status: IndexProductStatus) => {
  switch (status) {
    case 'Pending':
      return 'default';
    case 'InProgress':
      return 'info';
    case 'Completed':
      return 'success';
    case 'FailedAndRetrying':
      return 'warning';
    case 'Deadlettered':
      return 'error';
    default:
      return 'default';
  }
};

const formatIsoDateTime = (value: string | null) => {
  if (!value) {
    return 'N/A';
  }

  const dateValue = parseISO(value);
  return isValid(dateValue) ? format(dateValue, 'yyyy-MM-dd HH:mm:ss') : 'N/A';
};

export const ProductIndexingJobTable: React.FC<IndexingJobTableProps> = ({
  jobs,
}) => {
  if (!jobs || jobs.length === 0) {
    return <AppNoDataAlert message='No indexing jobs found' />;
  }

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow sx={{ backgroundColor: '#f5f5f5' }}>
            <TableCell>Job ID</TableCell>
            <TableCell>Status</TableCell>
            <TableCell>Created At</TableCell>
            <TableCell>Completed At</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {jobs.map((job) => (
            <React.Fragment key={job.jobId}>
              <TableRow hover>
                <TableCell>
                  <Typography
                    variant='body2'
                    sx={{ fontFamily: 'monospace', fontSize: '0.85rem' }}
                  >
                    {job.jobId.substring(0, 8)}...
                  </Typography>
                </TableCell>
                <TableCell>
                  <Chip
                    label={job.status}
                    color={getStatusChipColor(job.status)}
                    size='small'
                    variant='outlined'
                  />
                </TableCell>
                <TableCell>
                  {formatIsoDateTime(job.createdAt)}
                </TableCell>
                <TableCell>
                  {formatIsoDateTime(job.completedAt)}
                </TableCell>
              </TableRow>
              {job.status === 'FailedAndRetrying' && (
                <TableRow>
                  <TableCell
                    colSpan={7}
                    sx={{ paddingY: 2, backgroundColor: '#fff3e0' }}
                  >
                    <Box
                      sx={{ display: 'flex', alignItems: 'flex-start', gap: 1 }}
                    >
                      <Info
                        fontSize='small'
                        sx={{ color: '#f57c00', mt: 0.5 }}
                      />
                      <Box>
                        <Typography
                          variant='caption'
                          sx={{ fontWeight: 600, color: '#f57c00' }}
                        >
                          Error Details
                        </Typography>
                        <Typography
                          variant='body2'
                          sx={{
                            color: '#d84315',
                            mt: 0.5,
                            whiteSpace: 'pre-wrap',
                            wordBreak: 'break-word',
                          }}
                        >
                          ⚠️ An error occurred during indexing. The system will
                          automatically retry this job. If the issue persists,
                          please investigate the underlying cause.
                        </Typography>
                      </Box>
                    </Box>
                  </TableCell>
                </TableRow>
              )}
              {job.status === 'Deadlettered' && (
                <TableRow>
                  <TableCell
                    colSpan={7}
                    sx={{ paddingY: 2, backgroundColor: '#ffebee' }}
                  >
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                      <Typography
                        variant='body2'
                        sx={{ color: '#c62828', fontWeight: 600 }}
                      >
                        ⚠️ This job has been marked as deadlettered and will not
                        be retried automatically.
                      </Typography>
                    </Box>
                  </TableCell>
                </TableRow>
              )}
            </React.Fragment>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
