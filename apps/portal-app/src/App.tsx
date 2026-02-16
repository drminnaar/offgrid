// packages
import { Outlet } from 'react-router';
import { Box } from '@mui/material';

// styles
import './App.css';

// custom components
import { LoginPage } from './features/login';
import { AppContent, AppDrawer, AppHeader } from './features/layout';

// custom hooks
import { useKeycloak } from './lib/auth/keycloak';

// custom state
import { useAppDispatch, useAppSelector } from './store';
import { togglePaletteMode } from './store/global-ui-slice';

export const App = () => {
  const { authenticated, user, logout } = useKeycloak();
  const { paletteMode } = useAppSelector((state) => state.globalUI);
  const dispatch = useAppDispatch();

  if (!authenticated) {
    return <LoginPage />;
  }

  return (
    <Box sx={{ display: 'flex' }}>
      <AppHeader
        username={user?.username ?? ''}
        email={user?.email ?? ''}
        logout={logout}
        paletteMode={paletteMode}
        togglePaletteMode={() => dispatch(togglePaletteMode())}
      />
      <AppDrawer />
      <AppContent>
        <Outlet />
      </AppContent>
    </Box>
  );
};
