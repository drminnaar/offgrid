// packages
import React, { useEffect } from 'react';
import { Box, Container } from '@mui/material';

// components
import { LoginCard } from './login-card';
import { useKeycloak } from '../../lib/auth/keycloak';
import { useNavigate } from 'react-router';

export const LoginPage: React.FC = () => {
  const { authenticated, login } = useKeycloak();
  const navigate = useNavigate();

  useEffect(() => {
    if (authenticated) {
      navigate('/dashboard');
    }
  }, [authenticated, navigate]);

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
        <LoginCard login={login} />
      </Container>
    </Box>
  );
};
