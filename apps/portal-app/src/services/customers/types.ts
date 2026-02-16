export type CustomerInfo = {
  customerId: string;
  customerNumber: string;
  keycloakUserId: string;
  status: string;
  email: string;
  firstName: string;
  lastName: string;
  createdDate: string;
  updatedDate?: string;
  deletedDate?: string;
};

export type CustomerDetail = {
  customerId: string;
  customerNumber: string;
  keycloakUserId: string;
  status: string;
  email: string;
  firstName: string;
  lastName: string;
  createdDate: string;
  updatedDate?: string;
  deletedDate?: string;
};

export type GetAllCustomersQuery = {
  status?: string;
  page?: number;
  limit?: number;
};

export type SuspendCustomerRequest = {
  reason: string;
};

export type ReinstateCustomerRequest = {
  reason: string;
};

export type SuspendCustomerResult = {
  customerId: string;
  status: string;
};

export type ReinstateCustomerResult = {
  customerId: string;
  status: string;
};