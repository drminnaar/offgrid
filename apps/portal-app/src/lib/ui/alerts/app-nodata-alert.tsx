import { Alert, Button, Typography } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';

export type AppNoDataAlertProps = {
  message?: string;
  action?: boolean | (() => void);
};

export const AppNoDataAlert = ({ message, action }: AppNoDataAlertProps) => {
  return (
    <Alert
      variant='filled'
      severity='info'
      sx={{ mt: 2 }}
      action={
        !action ? null : (
          <Button
            color='inherit'
            size='small'
            variant='outlined'
            startIcon={<RefreshIcon />}
            onClick={typeof action === 'function' ? action : undefined}
          >
            Refresh
          </Button>
        )
      }
    >
      <Typography
        variant='subtitle2'
        component='div'
        sx={{ textAlign: 'center' }}
      >
        {message || 'No data available'}
      </Typography>
    </Alert>
  );
};
