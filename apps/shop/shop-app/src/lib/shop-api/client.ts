import { ApiError, ApiResponse } from '@/lib/shop-api/types';
import { getShopApiBaseUrl } from './config';
import { isValidationProblemDetails } from './problems';

type HttpMethod = 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE';

type FetchOptions = {
  method: HttpMethod;
  body?: unknown;
  cache?: RequestCache;
};

export const shopApiClient = {
  async request<TResponse>(
    endpoint: string,
    accessToken: string,
    options: FetchOptions
  ): Promise<ApiResponse<TResponse>> {
    const headers: HeadersInit = {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${accessToken}`,
    };

    const baseUrl = getShopApiBaseUrl();

    const response = await fetch(`${baseUrl}${endpoint}`, {
      method: options.method,
      headers,
      cache: options.cache ?? 'no-store',
      body: options.body ? JSON.stringify(options.body) : undefined,
    });

    const text = await response.text();
    const data = text && JSON.parse(text);

    if (response.ok) {
      return { success: true, data: data as TResponse };
    } else {
      return handleApiError(data, response.status);
    }
  },
};

const handleApiError = <TResponse>(data: unknown, status: number): ApiResponse<TResponse> => {
  const error = data as ApiError;

  if (!error) {
    console.error(`API Error: Received a response having status code of ${status} but no error body.`);
    return { success: false, error: null, status };
  }

  if (isValidationProblemDetails(error)) {
    console.error('Validation errors:', error.errors);
    return { success: false, error, status };
  }

  console.error(`API error: ${error.title}. ${error.detail}`);
  return { success: false, error, status };
};