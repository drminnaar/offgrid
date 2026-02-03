// packages
import { createBrowserRouter, Navigate } from 'react-router';

// components
import { App } from '../App';
import { NotFoundErrorPage } from '../features/errors';

export const AppRouter = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
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
