// mui packages
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  DialogContentText,
} from '@mui/material';
import CheckIcon from '@mui/icons-material/Check';
import CloseIcon from '@mui/icons-material/Close';

// types
export type IndexProductsDialogProps = {
  open: boolean;
  onClose: () => void;
  onConfirm: () => Promise<void>;
};

export const IndexProductsDialog = ({
  open,
  onClose,
  onConfirm,
}: IndexProductsDialogProps) => {
  return (
    <>
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Confirm Product Indexing</DialogTitle>
        <DialogContent>
          <DialogContentText>
            To index products, please confirm your action.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} startIcon={<CloseIcon />}>
            Cancel
          </Button>
          <Button
            variant='contained'
            color='warning'
            onClick={onConfirm}
            startIcon={<CheckIcon />}
          >
            Confirm
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};
