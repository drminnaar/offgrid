// packages
import { Navigate, Outlet } from 'react-router';

// lib
import { useKeycloak } from './use-keycloak';

export interface ProtectedRouteProps {
  roles?: string[];
  resourceRoles?: { resource: string; roles: string[] };
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  roles,
  resourceRoles,
}) => {
  const { authenticated, loading, hasRealmRole, hasResourceRole } =
    useKeycloak();

  if (loading) {
    return (
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <div>Loading...</div>
      </div>
    );
  }

  if (!authenticated) {
    return <Navigate to='/login' replace />;
  }

  // Check realm roles
  if (roles && roles.length > 0) {
    const hasRequiredRole = roles.some((role) => hasRealmRole(role));
    if (!hasRequiredRole) {
      return <Navigate to='/unauthorized' replace />;
    }
  }

  // Check resource roles
  if (resourceRoles) {
    const hasRequiredResourceRole = resourceRoles.roles.some((role) =>
      hasResourceRole(role, resourceRoles.resource),
    );
    if (!hasRequiredResourceRole) {
      return <Navigate to='/unauthorized' replace />;
    }
  }

  return <Outlet />;
};
