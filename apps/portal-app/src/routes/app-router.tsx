// packages
import { createBrowserRouter, Navigate } from 'react-router';

// components
import { App } from '../App';
import { NotFoundErrorPage } from '../features/errors';

// features
import { DashboardPage } from '../features/dashboard';

// lib
import { ProtectedRoute } from '../lib/auth/keycloak';
import { LoginPage } from '../features/login';

export const AppRouter = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <ProtectedRoute />,
        children: [
          {
            path: '/dashboard',
            element: <DashboardPage />,
          },
        ],
      },
      {
        path: '',
        element: <LoginPage />,
      },
      {
        path: '/login',
        element: <LoginPage />,
      },
      {
        path: '/not-found',
        element: <NotFoundErrorPage />,
      },
      {
        path: '*',
        element: <Navigate replace to='/not-found' />,
      },
    ],
  },
]);
