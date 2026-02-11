// packages
import React from 'react';
import { Box } from '@mui/material';

export const AppContent: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  return (
    <Box
      component='main'
      sx={{
        flexGrow: 1,
        p: 3,
        mt: '64px', // Offset for fixed Navbar
      }}
    >
      {children}
    </Box>
  );
};
