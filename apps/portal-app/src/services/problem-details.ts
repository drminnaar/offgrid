import type { ApiError, ValidationProblemDetails } from './types';

export const isValidationProblemDetails = (error: ApiError): error is ValidationProblemDetails => {
  return 'errors' in error && typeof error.errors === 'object' && error.errors !== null;
};