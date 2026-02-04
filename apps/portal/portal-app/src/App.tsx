// packages
import { Outlet } from 'react-router';
import { AppContent, AppHeader } from './features/layout';
import { Box } from '@mui/material';

// styles
import './App.css';

// components
import { LoginPage } from './features/login';
import { useKeycloak } from './lib/auth/keycloak';

export const App = () => {
  const { authenticated, user, logout } = useKeycloak();

  if (!authenticated) {
    return <LoginPage />;
  }

  return (
    <Box sx={{ display: 'flex' }}>
      <AppHeader
        username={user?.username ?? ''}
        email={user?.email ?? ''}
        logout={logout}
      />
      <AppContent>
        <Outlet />
      </AppContent>
    </Box>
  );
};
