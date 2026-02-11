// packages
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router';

// styles
import { CssBaseline } from '@mui/material';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import './index.css';

// components
import { AppRouter } from './routes';
import { AppRootProvider } from './providers';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AppRootProvider>
      <CssBaseline />
      <RouterProvider router={AppRouter} />
    </AppRootProvider>
  </StrictMode>,
);
