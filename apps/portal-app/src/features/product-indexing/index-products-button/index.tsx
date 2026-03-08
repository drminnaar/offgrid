// react packages
import { useState } from 'react';

// mui packages
import { Button } from '@mui/material';
import SyncIcon from '@mui/icons-material/Sync';

// api services
import {
  useGetCurrentProductIndexQuery,
  useIndexProductsMutation,
} from '../../../services/products/product-api';

// custom components
import { IndexProductsDialog } from './index-products-dialog';
import { AppErrorAlert } from '../../../lib/ui/alerts';

const toErrorMessage = (error: unknown) => {
  if (!error || typeof error !== 'object') {
    return 'Failed to start product indexing. Please try again.';
  }

  if ('status' in error && 'data' in error) {
    const apiError = error as { data?: unknown };
    if (typeof apiError.data === 'string') {
      return apiError.data;
    }

    if (
      apiError.data &&
      typeof apiError.data === 'object' &&
      'message' in apiError.data &&
      typeof (apiError.data as { message?: unknown }).message === 'string'
    ) {
      return (apiError.data as { message: string }).message;
    }
  }

  if (
    'message' in error &&
    typeof (error as { message?: unknown }).message === 'string'
  ) {
    return (error as { message: string }).message;
  }

  return 'Failed to start product indexing. Please try again.';
};

type IndexProductsButtonProps = {
  onConfirmIndexStarted: () => void;
};

export const IndexProductsButton: React.FC<IndexProductsButtonProps> = ({
  onConfirmIndexStarted,
}) => {
  const [isConfirmDialogOpen, setIsConfirmDialogOpen] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const {
    isLoading: isCurrentIndexLoading,
    data: currentIndexingJob,
    refetch: refetchCurrentIndexingJob,
  } = useGetCurrentProductIndexQuery();

  const [indexProducts, { isLoading: isIndexProductsLoading }] =
    useIndexProductsMutation();

  const isLoading = isCurrentIndexLoading || isIndexProductsLoading;

  const isIndexing =
    currentIndexingJob?.status === 'Pending' ||
    currentIndexingJob?.status === 'InProgress' ||
    currentIndexingJob?.status === 'FailedAndRetrying';

  const isBusy = isConfirmDialogOpen || isLoading || isIndexing;

  return (
    <>
      <Button
        variant='outlined'
        onClick={() => {
          setErrorMessage(null);
          setIsConfirmDialogOpen(true);
        }}
        startIcon={<SyncIcon />}
        loadingPosition='start'
        loading={isBusy}
        size='large'
        sx={{
          backgroundColor: isBusy ? '#C8E6C9' : '',
        }}
      >
        {isBusy ? 'Indexing Products ...' : 'Index Products'}
      </Button>

      <IndexProductsDialog
        open={isConfirmDialogOpen}
        onClose={() => {
          setErrorMessage(null);
          setIsConfirmDialogOpen(false);
        }}
        onConfirm={async () => {
          setIsConfirmDialogOpen(false);

          try {
            await indexProducts().unwrap();
            await refetchCurrentIndexingJob();
            onConfirmIndexStarted();
          } catch (error) {
            setErrorMessage(toErrorMessage(error));
          }
        }}
      />

      {errorMessage && <AppErrorAlert message={errorMessage} />}
    </>
  );
};
