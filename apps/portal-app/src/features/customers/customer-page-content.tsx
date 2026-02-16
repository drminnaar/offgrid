// packages
import { useState } from 'react';

// customer components
import { AppPagination } from '../../lib/ui';
import { CustomerTable } from './customer-table';
import { CustomerPageContentSkeleton } from './customer-page-content-skeleton';
import { CustomerPageFilters } from './customer-page-filters';

// services
import { useGetCustomersQuery } from '../../services/customers/customer-api';

// routing
import { useNavigate } from 'react-router';

export const CustomerPageContent = () => {
  const navigate = useNavigate();

  const [filters, setFilters] = useState({
    status: '',
    page: 1,
    limit: 10,
  });

  const {
    data: customers,
    refetch,
    isLoading,
    isError,
    error,
  } = useGetCustomersQuery(filters);

  if (isLoading) return <CustomerPageContentSkeleton />;

  if (isError) throw error;

  const handlePageChange = (page: number) => {
    setFilters((prev) => ({ ...prev, page }));
  };

  const handleFilterChange = (key: string, value: string | number) => {
    setFilters((prev) => ({ ...prev, [key]: value, page: 1 })); // Reset to page 1 on filter change
  };

  const handleViewCustomer = (customerId: string): void => {
    navigate(`/customers/${customerId}`);
  };

  return (
    <>
      <CustomerPageFilters
        filters={filters}
        onFilterChange={handleFilterChange}
        onRefresh={refetch}
      />

      <CustomerTable
        customers={customers?.items}
        onViewCustomer={handleViewCustomer}
      />

      <AppPagination
        paginationInfo={customers}
        onPageChange={handlePageChange}
      />
    </>
  );
};
