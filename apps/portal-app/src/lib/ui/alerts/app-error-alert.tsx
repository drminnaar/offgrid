import { Alert } from '@mui/material';

export type DefaultErrorAlertProps = {
  message?: string;
};

export const AppErrorAlert = ({ message }: DefaultErrorAlertProps) => {
  return (
    <Alert severity='error'>{message || 'Error whilst loading content'}</Alert>
  );
};
