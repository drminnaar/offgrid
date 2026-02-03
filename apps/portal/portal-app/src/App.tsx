// packages
import { Outlet } from 'react-router';
import { AppContent } from './features/layout';
import { Box } from '@mui/material';

// styles
import './App.css';

// components
import { LoginPage } from './features/login/login-page';

export const App = () => {
  const authenticated = false; // --- IGNORE ---

  if (!authenticated) {
    return <LoginPage />;
  }

  return (
    <Box sx={{ display: 'flex' }}>
      <AppContent>
        <Outlet />
      </AppContent>
    </Box>
  );
};
