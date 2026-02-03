// packages
import React from 'react';
import { Box, Container } from '@mui/material';

// components
import { LoginCard } from './login-card';

export const LoginPage: React.FC = () => {
  return (
    <Box
      sx={{
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        background: 'linear-gradient(135deg, #1565c0 0%, #42a5f5 100%)',
        position: 'relative',
        overflow: 'hidden',
      }}
    >
      <Container maxWidth='sm'>
        <LoginCard login={() => console.log('Login not implemented')} />
      </Container>
    </Box>
  );
};
