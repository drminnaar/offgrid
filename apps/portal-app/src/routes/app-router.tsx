// packages
import { createBrowserRouter, Navigate } from 'react-router';

// components
import { App } from '../App';

// lib
import { ProtectedRoute, RealmRole } from '../lib/auth/keycloak';

// pages
import { NotAuthorizedErrorPage, NotFoundErrorPage } from '../features/errors';
import { LoginPage } from '../features/login';
import { DashboardPage } from '../features/dashboard';
import { CustomerPage, CustomerDetailPage } from '../features/customers';
import { ProductDetailsPage, ProductPage } from '../features/products';
import { ProductIndexingPage } from '../features/product-indexing';

export const AppRouter = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <ProtectedRoute roles={[...RealmRole.All]} />,
        children: [
          {
            path: '/dashboard',
            element: <DashboardPage />,
          },
        ],
      },
      {
        element: (
          <ProtectedRoute
            roles={[RealmRole.Admin, RealmRole.CustomerManager]}
          />
        ),
        children: [
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
        element: (
          <ProtectedRoute roles={[RealmRole.Admin, RealmRole.ProductManager]} />
        ),
        children: [
          {
            path: '/products',
            element: <ProductPage />,
          },
          {
            path: '/products/:productId',
            element: <ProductDetailsPage />,
          },
          {
            path: '/products/indexing',
            element: <ProductIndexingPage />,
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
        path: '/not-authorized',
        element: <NotAuthorizedErrorPage />,
      },
      {
        path: '*',
        element: <Navigate replace to='/not-found' />,
      },
    ],
  },
]);
