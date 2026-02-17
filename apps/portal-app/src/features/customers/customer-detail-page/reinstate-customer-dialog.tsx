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

export type ReinstateCustomerDialogProps = {
  open: boolean;
  onClose: () => void;
  onReinstate: (reason: string) => Promise<void>;
  isReinstating?: boolean;
};

export const ReinstateCustomerDialog = ({
  open,
  onClose,
  onReinstate,
  isReinstating,
}: ReinstateCustomerDialogProps) => {
  const [reinstateReason, setReinstateReason] = useState('');

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const formJson = Object.fromEntries(formData.entries());
    const reason = formJson.reason as string;
    await onReinstate(reason);
    setReinstateReason('');
    onClose();
  };

  return (
    <>
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Reinstate Customer</DialogTitle>
        <DialogContent>
          <DialogContentText>
            To reinstate this customer, please enter a reason for the
            reinstatement.
          </DialogContentText>
          <form onSubmit={handleSubmit} id='reinstate-form'>
            <TextField
              id='reason'
              name='reason'
              label='Reason'
              autoFocus
              required
              value={reinstateReason}
              onChange={(e) => setReinstateReason(e.target.value)}
              fullWidth
              margin='normal'
            />
          </form>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button
            type='submit'
            form='reinstate-form'
            variant='contained'
            color='success'
            loading={isReinstating}
            loadingPosition='start'
            disabled={isReinstating}
          >
            Reinstate
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};
