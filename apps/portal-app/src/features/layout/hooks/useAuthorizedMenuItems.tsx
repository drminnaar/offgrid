// packages
import { useMemo } from 'react';
import {
  Dashboard as DashboardIcon,
  People as CustomersIcon,
  ShoppingCart as ProductsIcon,
} from '@mui/icons-material';

// libs
import { RealmRole, useKeycloak } from '../../../lib/auth/keycloak';

const menuConfig = [
  {
    text: 'Dashboard',
    icon: <DashboardIcon />,
    path: '/dashboard',
    requiredRoles: [...RealmRole.All],
  },
  {
    text: 'Customers',
    icon: <CustomersIcon />,
    path: '/customers',
    requiredRoles: [RealmRole.CustomerManager],
  },
  {
    text: 'Products',
    icon: <ProductsIcon />,
    path: '/products',
    requiredRoles: [RealmRole.ProductManager],
  },
];

export const useAuthorizedMenuItems = () => {
  const { keycloak } = useKeycloak();
  return useMemo(
    () =>
      menuConfig.filter((item) =>
        item.requiredRoles.some((role) => keycloak?.hasRealmRole?.(role)),
      ),
    [keycloak],
  );
};
