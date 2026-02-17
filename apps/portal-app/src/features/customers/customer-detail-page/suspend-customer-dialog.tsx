import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
  DialogContentText,
} from '@mui/material';
import { useState } from 'react';

export type SuspendCustomerDialogProps = {
  open: boolean;
  onClose: () => void;
  onSuspend: (reason: string) => Promise<void>;
  isSuspending?: boolean;
};

export const SuspendCustomerDialog = ({
  open,
  onClose,
  onSuspend,
  isSuspending,
}: SuspendCustomerDialogProps) => {
  const [suspendReason, setSuspendReason] = useState('');

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const formJson = Object.fromEntries(formData.entries());
    const reason = formJson.reason as string;
    await onSuspend(reason);
    setSuspendReason('');
    onClose();
  };

  return (
    <>
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Suspend Customer</DialogTitle>
        <DialogContent>
          <DialogContentText>
            To suspend this customer, please enter a reason for the suspension.
          </DialogContentText>
          <form onSubmit={handleSubmit} id='suspend-form'>
            <TextField
              id='reason'
              name='reason'
              label='Reason'
              autoFocus
              required
              value={suspendReason}
              onChange={(e) => setSuspendReason(e.target.value)}
              fullWidth
              margin='normal'
            />
          </form>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button
            type='submit'
            form='suspend-form'
            variant='contained'
            color='warning'
            loading={isSuspending}
            loadingPosition='start'
            disabled={isSuspending}
          >
            Suspend
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};
