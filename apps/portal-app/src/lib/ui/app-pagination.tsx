import React from 'react';
import { Box, Pagination } from '@mui/material';

export type PaginationInfo = {
  currentPageNumber: number;
  itemCount: number;
  pageSize: number;
  pageCount: number;
  lastPageNumber: number;
  nextPageNumber?: number;
  previousPageNumber?: number;
  hasPrevious: boolean;
  hasNext: boolean;
};

export type AppPaginationProps = {
  paginationInfo?: PaginationInfo;
  onPageChange: (page: number) => void;
};

export const AppPagination: React.FC<AppPaginationProps> = ({
  paginationInfo,
  onPageChange,
}) => {
  if (!paginationInfo || paginationInfo.pageCount <= 1) {
    return null; // Don't render pagination if there's only one page or no info
  }

  const { pageCount, currentPageNumber } = paginationInfo;

  const handlePageChange = (
    _event: React.ChangeEvent<unknown>,
    page: number,
  ) => {
    onPageChange(page);
  };

  return (
    <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
      <Pagination
        count={pageCount}
        page={currentPageNumber}
        onChange={handlePageChange}
        color='primary'
      />
    </Box>
  );
};
