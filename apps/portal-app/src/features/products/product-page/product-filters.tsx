import {
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  OutlinedInput,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';

// type ProductCategoryOption = {
//   category: string;
//   subcategories: string[];
// };

type ProductFiltersProps = {
  data: {
    brands: { key: string; value: string; label: string }[];
    categories: { key: string; value: string; label: string }[];
    types: { key: string; value: string; label: string }[];
  };
  filters: {
    brand?: string;
    category?: string;
    type?: string;
  };
  onFilterChange: (key: string, value: string | number) => void;
  onRefresh: () => void;
};

export const ProductFilters: React.FC<ProductFiltersProps> = ({
  data: { brands = [], categories = [], types = [] },
  filters,
  onFilterChange,
  onRefresh,
}) => {
  const brandsWithAllFilter = [
    { key: 'allbrands', value: '', label: 'All' },
    ...brands,
  ];
  const categoriesWithAllFilter = [
    { key: 'allcategories', value: '', label: 'All' },
    ...categories,
  ];
  const typesWithAllFilter = [
    { key: 'alltypes', value: '', label: 'All' },
    ...types,
  ];
  return (
    <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
      {/* Product Types */}
      <FormControl size='small' sx={{ minWidth: 200 }}>
        <InputLabel>Product Type</InputLabel>
        <Select
          labelId='product-type-select'
          value={filters.type || ''}
          label='Product Type'
          onChange={(e) => onFilterChange('type', e.target.value)}
        >
          {typesWithAllFilter &&
            typesWithAllFilter.map((type) => (
              <MenuItem key={type.key} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
        </Select>
      </FormControl>
      {/* Categories */}
      <FormControl size='small' sx={{ minWidth: 200, ml: 2 }}>
        <InputLabel id='product-category-select'>Category</InputLabel>
        <Select
          labelId='product-category-select'
          value={filters.category || ''}
          label='Category'
          onChange={(e) => onFilterChange('category', e.target.value)}
          input={<OutlinedInput label='Category' />}
        >
          {categoriesWithAllFilter &&
            categoriesWithAllFilter.map((cat) => (
              <MenuItem key={cat.key} value={cat.value}>
                {cat.label}
              </MenuItem>
            ))}
        </Select>
      </FormControl>
      {/* Brands */}
      <FormControl size='small' sx={{ minWidth: 200, ml: 2 }}>
        <InputLabel id='product-brand-select'>Brand</InputLabel>
        <Select
          labelId='product-brand-select'
          value={filters.brand || ''}
          label='Brand'
          onChange={(e) => onFilterChange('brand', e.target.value)}
          input={<OutlinedInput label='Brand' />}
        >
          {brandsWithAllFilter &&
            brandsWithAllFilter.map((brand) => (
              <MenuItem key={brand.key} value={brand.value}>
                {brand.label}
              </MenuItem>
            ))}
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
