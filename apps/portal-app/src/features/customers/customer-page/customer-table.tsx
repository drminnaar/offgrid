// packages
import React from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  Chip,
  Tooltip,
} from '@mui/material';
import VisibilityIcon from '@mui/icons-material/Visibility';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';

// custom components
import { AppNoDataAlert } from '../../../lib/ui/alerts';

type CustomerRow = {
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

type CustomerTableProps = {
  customers?: CustomerRow[];
  onViewCustomer: (customerId: string) => void;
};

export const CustomerTable: React.FC<CustomerTableProps> = ({
  customers,
  onViewCustomer,
}) => {
  const handleView = (customerId: string) => {
    onViewCustomer(customerId);
  };

  if (!customers || customers.length === 0) {
    return <AppNoDataAlert message='No customers found' />;
  }

  const copyToClipboard = (text: string) => {
    navigator.clipboard.writeText(text);
  };

  return (
    <TableContainer component={Paper} className='shadow-md rounded-lg'>
      <Table>
        <TableHead>
          <TableRow className='bg-gray-100'>
            <TableCell className='font-bold'>Status</TableCell>
            <TableCell className='font-bold'>ID</TableCell>
            <TableCell className='font-bold'>Customer Number</TableCell>
            <TableCell className='font-bold'>Name</TableCell>
            <TableCell className='font-bold'>Email</TableCell>
            <TableCell className='font-bold'>Created On</TableCell>
            <TableCell className='font-bold'>Updated On</TableCell>
            <TableCell className='font-bold'>Deleted On</TableCell>
            <TableCell className='font-bold'>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {customers.map((customer) => (
            <TableRow key={customer.customerId} className='hover:bg-gray-50'>
              <TableCell>
                <Chip
                  label={customer.status}
                  color={
                    customer.status.toLowerCase() === 'active'
                      ? 'success'
                      : 'default'
                  }
                />
              </TableCell>
              <TableCell>
                {customer.customerId}{' '}
                <Tooltip title='Copy customer ID'>
                  <IconButton
                    aria-label='Copy customer ID'
                    size='small'
                    onClick={() => copyToClipboard(customer.customerId)}
                    sx={{ ml: 1 }}
                  >
                    <ContentCopyIcon fontSize='small' />
                  </IconButton>
                </Tooltip>
              </TableCell>
              <TableCell>
                {customer.customerNumber}
                <Tooltip title='Copy customer number'>
                  <IconButton
                    aria-label='Copy customer number'
                    size='small'
                    onClick={() => copyToClipboard(customer.customerNumber)}
                    sx={{ ml: 1 }}
                  >
                    <ContentCopyIcon fontSize='small' />
                  </IconButton>
                </Tooltip>
              </TableCell>
              <TableCell>{`${customer.firstName} ${customer.lastName}`}</TableCell>
              <TableCell>{customer.email}</TableCell>
              <TableCell>
                {new Date(customer.createdDate).toLocaleString()}
              </TableCell>
              <TableCell>
                {customer.updatedDate
                  ? new Date(customer.updatedDate).toLocaleString()
                  : '-'}
              </TableCell>
              <TableCell>
                {customer.deletedDate
                  ? new Date(customer.deletedDate).toLocaleString()
                  : '-'}
              </TableCell>
              <TableCell>
                <IconButton onClick={() => handleView(customer.customerId)}>
                  <VisibilityIcon />
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
