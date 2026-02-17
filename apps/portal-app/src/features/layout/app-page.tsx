// packages
import React from 'react';
import Divider from '@mui/material/Divider';
import Typography from '@mui/material/Typography';
import { ErrorBoundary } from 'react-error-boundary';

// custom components
import { AppErrorAlert } from '../../lib/ui/alerts';

export type AppPageProps = {
  title: string;
  children: React.ReactNode;
  errorFallback?: React.ReactNode;
};

export const AppPage: React.FC<AppPageProps> = ({
  title: pageTitle,
  children: pageContent,
  errorFallback,
}) => {
  const fallBack = errorFallback || (
    <AppErrorAlert message='An error occurred while loading the page.' />
  );
  return (
    <>
      <Typography variant='h4'>{pageTitle}</Typography>
      <Divider sx={{ my: 2 }} />
      <ErrorBoundary fallback={fallBack}>{pageContent}</ErrorBoundary>
    </>
  );
};
