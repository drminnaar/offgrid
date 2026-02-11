'use client';

import { CustomerProfile } from '@/components/customers/profile';
import { signIn, useSession } from 'next-auth/react';

const handleSignin = () => {
  signIn('keycloak', { redirectTo: '/profile' }, { prompt: 'login' });
};

const mapToCustomer = (user?: {
  active?: string | null;
  customerNumber?: string | null;
  email?: string | null;
  name?: string | null;
}) => {
  return {
    active: user?.active || 'N/A',
    customerNumber: user?.customerNumber || 'N/A',
    email: user?.email || 'N/A',
    name: user?.name || 'N/A',
  };
};

export default function ProfilePage() {
  const { data: session, status } = useSession({
    required: true,
    onUnauthenticated() {
      handleSignin();
    },
  });

  if (status === 'loading') {
    return null;
  }

  return (
    <div className='container mx-auto max-w-4xl py-8 px-4'>
      <div className='mb-8'>
        <h1 className='text-3xl font-bold'>Profile</h1>
      </div>
      <CustomerProfile customer={mapToCustomer(session?.user)} />
    </div>
  );
}
