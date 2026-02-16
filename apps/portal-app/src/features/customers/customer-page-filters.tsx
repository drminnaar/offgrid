import {
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';

export type CustomerPageFiltersProps = {
  filters: {
    status: string;
  };
  onFilterChange: (key: string, value: string | number) => void;
  onRefresh: () => void;
};

export const CustomerPageFilters: React.FC<CustomerPageFiltersProps> = ({
  filters,
  onFilterChange,
  onRefresh,
}) => {
  return (
    <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
      <FormControl size='small' sx={{ minWidth: 120 }}>
        <InputLabel>Status</InputLabel>
        <Select
          value={filters.status}
          label='Status'
          onChange={(e) => onFilterChange('status', e.target.value)}
        >
          <MenuItem value=''>All</MenuItem>
          <MenuItem value='active'>Active</MenuItem>
          <MenuItem value='suspended'>Suspended</MenuItem>
        </Select>
      </FormControl>
      <Button
        variant='outlined'
        onClick={onRefresh}
        startIcon={<RefreshIcon />}
      >
        Refresh
      </Button>
    </Box>
  );
};
