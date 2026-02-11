// packages
import { SearchOff } from '@mui/icons-material';
import { Button, Paper, Typography } from '@mui/material';
import { Link } from 'react-router';

export const NotFoundErrorPage = () => {
  return (
    <Paper
      elevation={12}
      sx={{
        height: '70vh',
        width: '60vw',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        p: 4,
        margin: 'auto',
        overflow: 'hidden',
      }}
    >
      <SearchOff
        sx={{ fontSize: { xs: 40, sm: 60, md: 70, lg: 80 } }}
        color='primary'
      />
      <Typography
        variant='h3'
        gutterBottom
        sx={{
          fontSize: { xs: '1.5rem', sm: '2rem', md: '2.5rem', lg: '3rem' },
        }}
      >
        Page Not Found
      </Typography>
      <Button
        component={Link}
        to='/'
        sx={{
          fontSize: {
            xs: '0.75rem',
            sm: '0.875rem',
            md: '1rem',
            lg: '1.25rem',
          },
          padding: {
            xs: '6px 16px',
            sm: '8px 20px',
            md: '10px 24px',
            lg: '12px 28px',
          },
        }}
      >
        Back to Home Page
      </Button>
    </Paper>
  );
};
