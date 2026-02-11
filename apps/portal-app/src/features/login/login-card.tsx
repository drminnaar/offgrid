// packages
import React from 'react';
import { Landscape } from '@mui/icons-material';
import { Box, Button, Paper, Typography } from '@mui/material';

type LoginCardProps = {
  login: () => void;
};

export const LoginCard: React.FC<LoginCardProps> = ({ login }) => {
  return (
    <Paper
      elevation={12}
      sx={{
        p: 6,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        borderRadius: 3,
        backdropFilter: 'blur(10px)',
        background: 'rgba(255, 255, 255, 0.95)',
      }}
    >
      {/* Logo/Icon */}
      <Box
        sx={{
          mb: 2,
          p: 2,
          borderRadius: '50%',
          background: 'linear-gradient(135deg, #1565c0 0%, #42a5f5 100%)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}
      >
        <Landscape
          sx={{
            display: { xs: 'flex' },
            fontSize: 60,
            color: 'white',
          }}
        />
      </Box>

      {/* Title */}
      <Typography
        variant='h4'
        component='h1'
        gutterBottom
        sx={{
          fontWeight: 700,
          color: '#333',
          mb: 1,
        }}
      >
        Offgrid
      </Typography>

      <Typography
        variant='subtitle1'
        sx={{
          color: '#666',
          mb: 4,
          textAlign: 'center',
        }}
      >
        Admin Portal
      </Typography>

      {/* Login Button */}
      <Button
        variant='contained'
        size='large'
        onClick={login}
        sx={{
          px: 6,
          py: 1.5,
          borderRadius: 2,
          textTransform: 'none',
          fontSize: '1.1rem',
          fontWeight: 600,
          background: 'linear-gradient(135deg, #1565c0 0%, #42a5f5 100%)',
          boxShadow: '0 4px 15px rgba(102, 126, 234, 0.4)',
          transition: 'all 0.3s ease',
          '&:hover': {
            transform: 'translateY(-2px)',
            boxShadow: '0 6px 20px rgba(102, 126, 234, 0.5)',
            background: 'linear-gradient(135deg, #2962FF 0%, #40C4FF 100%)',
          },
        }}
      >
        Login
      </Button>
    </Paper>
  );
};
