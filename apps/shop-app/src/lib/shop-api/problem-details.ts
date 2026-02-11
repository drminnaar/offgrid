import { ApiError, ValidationProblemDetails } from './types';

export const isValidationProblemDetails = (error: ApiError): error is ValidationProblemDetails => {
  return typeof error === 'object'
    && error !== null
    && 'errors' in error
    && typeof error.errors === 'object';
};