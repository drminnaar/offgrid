// packages
import { createBrowserRouter, Navigate } from 'react-router';

// components
import { App } from '../App';
import { NotFoundErrorPage } from '../features/errors';

// lib
import { ProtectedRoute } from '../lib/auth/keycloak';

// pages
import { LoginPage } from '../features/login';
import { DashboardPage } from '../features/dashboard';
import { CustomerPage } from '../features/customers';
import { CustomerDetailPage } from '../features/customers';

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
          {
            path: '/customers',
            element: <CustomerPage />,
          },
          {
            path: '/customers/:customerId',
            element: <CustomerDetailPage />,
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
