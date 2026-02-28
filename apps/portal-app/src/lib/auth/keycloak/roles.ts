
/**
 * Represents the set of realm roles used for authorization within the application.
 * Each static readonly property corresponds to a specific role name as defined in the authentication provider.
 *
 * @remarks
 * - The `Admin` role typically grants full access to all resources and administrative functions.
 * - The `CustomerManager` role allows managing customer-related resources and operations.
 * - The `ProductManager` role allows managing product-related resources and operations.
 * - The `All` property provides an array of all defined realm roles for easy reference and iteration.
 *
 * These roles can be used in conjunction with the `ProtectedRoute` component to restrict access to certain routes based on the user's assigned roles.
 */
export class RealmRole {
  /** The 'admin' role grants full access to all resources and administrative functions. */
  static readonly Admin = 'admin';

  /** The 'customer_manager' role allows managing customer-related resources and operations. */
  static readonly CustomerManager = 'customer-manager';

  /** The 'product_manager' role allows managing product-related resources and operations. */
  static readonly ProductManager = 'product-manager';

  /** An array containing all defined realm roles for easy reference and iteration. */
  static readonly All = [RealmRole.Admin, RealmRole.CustomerManager, RealmRole.ProductManager];
}