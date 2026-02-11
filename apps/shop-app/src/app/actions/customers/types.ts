export type UpsertCustomerRequest = {
  keycloakUserId: string;
  email: string;
  fullName?: string;
};

export type UpsertCustomerResponse = {
  customerId: string;
  keycloakUserId: string;
  customerNumber: string;
  status: string;
  firstName: string;
  lastName: string;
  email: string;
  createdDateUnixTimeSeconds: string;
};
