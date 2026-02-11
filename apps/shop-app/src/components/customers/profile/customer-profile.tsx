'use client';

import {
  Card,
  CardBody,
  CardHeader,
  Divider,
  Avatar,
  AvatarIcon,
  Chip,
} from '@heroui/react';
import React from 'react';

type CustomerProfileProps = {
  customer: {
    email: string;
    customerNumber: string;
    name: string;
    active: string;
  };
};

export const CustomerProfile: React.FC<CustomerProfileProps> = ({
  customer,
}) => {
  const { email, customerNumber, name, active } = customer;

  return (
    <Card className='w-full max-w-3xl mx-auto'>
      <CardHeader className='flex flex-col gap-2 sm:gap-4 sm:flex-row sm:items-center sm:justify-between'>
        <div className='flex items-center gap-4'>
          <Avatar
            isBordered
            as='div'
            className='shrink-0'
            color='primary'
            size='lg'
            icon={<AvatarIcon />}
            name={name || 'User'}
          />
          <div className='flex flex-col gap-1'>
            <p className='text-xl font-semibold'>{name || 'Unknown'}</p>
            <p className='text-sm text-default-500'>{email || 'No email'}</p>
          </div>
        </div>
        <Chip
          variant='flat'
          color={
            active === 'active'
              ? 'success'
              : active === 'pending'
                ? 'warning'
                : 'danger'
          }
          className='capitalize w-fit'
        >
          {active || 'N/A'}
        </Chip>
      </CardHeader>
      <Divider />
      <CardBody className='gap-6'>
        <div className='grid grid-cols-1 gap-4 sm:grid-cols-2'>
          <div className='flex flex-col gap-2 rounded-lg bg-default-50 p-4'>
            <span className='text-xs font-semibold uppercase tracking-wide text-default-500'>
              Customer Number
            </span>
            <code className='text-sm bg-default-100 px-2 py-1 rounded w-fit'>
              {customerNumber}
            </code>
          </div>
          <div className='flex flex-col gap-2 rounded-lg bg-default-50 p-4'>
            <span className='text-xs font-semibold uppercase tracking-wide text-default-500'>
              Contact
            </span>
            <span className='text-sm text-default-700'>
              {email || 'No email on file'}
            </span>
          </div>
        </div>
      </CardBody>
    </Card>
  );
};
