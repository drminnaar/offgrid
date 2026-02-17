import { useState } from 'react';
import { useParams } from 'react-router';
import {
  Box,
  Card,
  CardContent,
  CardHeader,
  Button,
  Divider,
  Typography,
  Chip,
  Tooltip,
} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import { format } from 'date-fns';

// services
import {
  useGetCustomerByIdQuery,
  useReinstateCustomerMutation,
  useSuspendCustomerMutation,
} from '../../../services/customers/customer-api';
import { SuspendCustomerDialog } from './suspend-customer-dialog';
import { ReinstateCustomerDialog } from './reinstate-customer-dialog';

// custom components
import { CustomerDetailContentSkeleton } from './customer-detail-content-skeleton';
import { AppNoDataAlert } from '../../../lib/ui/alerts';

// types
export type CustomerDetail = {
  customerId: string;
  customerNumber: string;
  status: string;
  email: string;
  firstName: string;
  lastName: string;
  createdDate: string;
  updatedDate?: string;
  deletedDate?: string;
};

export const CustomerDetailContent = () => {
  const { customerId } = useParams<{ customerId: string }>();
  const [suspendCustomer, { isLoading: isSuspending }] =
    useSuspendCustomerMutation();
  const [reinstateCustomer, { isLoading: isReinstating }] =
    useReinstateCustomerMutation();

  const [suspendModal, setSuspendModal] = useState<{
    open: boolean;
    customerId: string | null;
  }>({ open: false, customerId: null });

  const [reinstateModal, setReinstateModal] = useState<{
    open: boolean;
    customerId: string | null;
  }>({ open: false, customerId: null });

  const {
    data: customer,
    isLoading,
    isError,
    error,
    refetch,
    isFetching,
  } = useGetCustomerByIdQuery(customerId ?? '', { skip: !customerId });

  if (isLoading) return <CustomerDetailContentSkeleton />;

  if (isError) {
    if ('status' in error) {
      if (error.status === 404) {
        return <AppNoDataAlert message='Customer not found' />;
      }
      throw error;
    }
  }

  if (!customer) return <AppNoDataAlert message='Customer not found' />;

  const handleSuspendClick = () => {
    setSuspendModal({ open: true, customerId: customer.customerId });
  };

  const handleReinstateClick = async () => {
    setReinstateModal({ open: true, customerId: customer.customerId });
  };

  return (
    <>
      <SuspendCustomerDialog
        open={suspendModal.open}
        onClose={() => {
          setSuspendModal({ open: false, customerId: null });
        }}
        onSuspend={async (reason: string) => {
          await suspendCustomer({
            customerId: suspendModal.customerId!,
            request: { reason },
          });
          refetch();
        }}
        isSuspending={isSuspending}
      />

      <ReinstateCustomerDialog
        open={reinstateModal.open}
        onClose={() => {
          setReinstateModal({ open: false, customerId: null });
        }}
        onReinstate={async (reason: string) => {
          await reinstateCustomer({
            customerId: reinstateModal.customerId!,
            request: { reason },
          });
          refetch();
        }}
        isReinstating={isReinstating}
      />
      <Box className='max-w-xl mt-8'>
        <Card>
          <CardHeader
            title={
              <Box className='flex justify-between items-center'>
                <Box className='flex items-center gap-2'>
                  <Chip
                    label={customer.status}
                    color={
                      customer.status.toLowerCase() === 'active'
                        ? 'success'
                        : 'default'
                    }
                  />
                  <Typography variant='h6'>
                    {customer.firstName} {customer.lastName} -{' '}
                    {customer.customerNumber}{' '}
                  </Typography>
                </Box>
                <Box>
                  {customer.status === 'Active' ? (
                    <Button
                      variant='contained'
                      color='warning'
                      onClick={handleSuspendClick}
                      size='small'
                      sx={{ minWidth: 100 }}
                      disabled={isSuspending || isFetching}
                    >
                      Suspend
                    </Button>
                  ) : (
                    <Button
                      variant='contained'
                      color='success'
                      onClick={handleReinstateClick}
                      size='small'
                      sx={{ minWidth: 100 }}
                      disabled={isReinstating || isFetching}
                    >
                      Reinstate
                    </Button>
                  )}
                </Box>
              </Box>
            }
            sx={{
              backgroundColor: 'background.paper',
              borderBottom: 1,
              borderColor: 'divider',
            }}
          />
          <CardContent>
            <Box className='flex flex-col gap-4'>
              <Typography>
                <span className='font-semibold'>Customer ID:</span>{' '}
                {customer.customerId}
                <Tooltip title='Copy customer ID'>
                  <IconButton
                    aria-label='Copy customer ID'
                    size='small'
                    onClick={() =>
                      navigator.clipboard.writeText(customer.customerId)
                    }
                    sx={{ ml: 1 }}
                  >
                    <ContentCopyIcon fontSize='small' />
                  </IconButton>
                </Tooltip>
              </Typography>
              <Typography>
                <span className='font-semibold'>Customer Number:</span>{' '}
                {customer.customerNumber}
                <Tooltip title='Copy customer number'>
                  <IconButton
                    aria-label='Copy customer number'
                    size='small'
                    onClick={() =>
                      navigator.clipboard.writeText(customer.customerNumber)
                    }
                    sx={{ ml: 1 }}
                  >
                    <ContentCopyIcon fontSize='small' />
                  </IconButton>
                </Tooltip>
              </Typography>
              <Typography>
                <span className='font-semibold'>Email:</span> {customer.email}
              </Typography>
              <Typography>
                <span className='font-semibold'>Name:</span>{' '}
                {customer.firstName} {customer.lastName}
              </Typography>
              <Divider className='my-4' />
              <Typography>
                <span className='font-semibold'>Created Date:</span>{' '}
                {(customer.createdDate &&
                  format(
                    new Date(customer.createdDate),
                    'dd MMM yyyy h:mm a',
                  )) ||
                  '—'}
              </Typography>
              <Typography>
                <span className='font-semibold'>Updated Date:</span>{' '}
                {(customer.updatedDate &&
                  format(
                    new Date(customer.updatedDate),
                    'dd MMM yyyy h:mm a',
                  )) ||
                  '—'}
              </Typography>
              {customer.deletedDate && (
                <Typography>
                  <span className='font-semibold'>Deleted Date:</span>{' '}
                  {(customer.deletedDate &&
                    format(
                      new Date(customer.deletedDate),
                      'dd MMM yyyy h:mm a',
                    )) ||
                    '—'}
                </Typography>
              )}
            </Box>
          </CardContent>
        </Card>
      </Box>
    </>
  );
};
