import { Paper, Typography, List, ListItem, ListItemText } from '@mui/material';

type ProductFeaturesProps = {
  features: string[];
};

export const ProductFeatures = ({ features }: ProductFeaturesProps) => {
  return (
    <Paper sx={{ p: 3, mb: 3 }} elevation={2}>
      <Typography variant='h6' gutterBottom>
        Key Features
      </Typography>
      <List dense>
        {features.map((feature, idx) => (
          <ListItem key={idx} disablePadding>
            <ListItemText primary={`• ${feature}`} />
          </ListItem>
        ))}
      </List>
    </Paper>
  );
};
