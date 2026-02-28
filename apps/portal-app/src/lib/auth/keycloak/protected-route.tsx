// packages
import { Navigate, Outlet } from 'react-router';

// lib
import { useKeycloak } from './use-keycloak';
import { EnvSetting } from '../../env';

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
    let message = '';
    if (!hasRequiredRole) {
      if (EnvSetting.APP_ENV === 'development') {
        const missingRoles = roles.filter((role) => !hasRealmRole(role));
        message = `You are missing one of the following required realm roles: ${missingRoles.join(
          ', ',
        )}`;
        console.warn(message);
      }
      return <Navigate to='/not-authorized' state={{ message }} replace />;
    }
  }

  // Check resource roles
  if (resourceRoles) {
    const hasRequiredResourceRole = resourceRoles.roles.some((role) =>
      hasResourceRole(role, resourceRoles.resource),
    );
    if (!hasRequiredResourceRole) {
      let message = '';
      if (EnvSetting.APP_ENV === 'development') {
        const missingResourceRoles = resourceRoles.roles.filter(
          (role) => !hasResourceRole(role, resourceRoles.resource),
        );
        message = `You are missing one of the following required resource roles: ${missingResourceRoles.join(
          ', ',
        )} for resource: ${resourceRoles.resource}`;
        console.warn(message);
      }
      return <Navigate to='/not-authorized' state={{ message }} replace />;
    }
  }

  return <Outlet />;
};
