import { ApiError, ApiResponse } from '@/lib/shop-api/types';
import { getShopApiBaseUrl } from './config';
import { isValidationProblemDetails } from './problem-details';
import { joinUrl } from '../utils/url-utils';
import { safeParseJson } from '../utils/json-utils';

type HttpMethod = 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE';

type FetchOptions = {
  method: HttpMethod;
  body?: unknown;
  cache?: RequestCache;
  signal?: AbortSignal;
};

export const shopApiClient = {
  async request<TResponse>(
    endpoint: string,
    accessToken: string | undefined,
    options: FetchOptions
  ): Promise<ApiResponse<TResponse>> {
    const headers: HeadersInit = {
      'Accept': 'application/json',
    };

    if (options.body !== undefined) {
      headers['Content-Type'] = 'application/json';
    }

    if (accessToken) {
      headers['Authorization'] = `Bearer ${accessToken}`;
    }

    const baseUrl = getShopApiBaseUrl();

    const response = await fetch(joinUrl(baseUrl, endpoint), {
      method: options.method,
      headers,
      cache: options.cache ?? 'no-store',
      signal: options.signal,
      body: options.body !== undefined ? JSON.stringify(options.body) : undefined,
    });

    const text = await response.text();
    const contentType = response.headers.get('content-type') ?? '';
    const data = safeParseJson(text, contentType);

    if (response.ok) {
      return { success: true, data: data as TResponse };
    } else {
      return handleApiError(data, response.status, response.statusText);
    }
  },
};

const handleApiError = <TResponse>(
  data: unknown,
  status: number,
  statusText: string
): ApiResponse<TResponse> => {
  if (!data) {
    console.error(`API Error: Received status ${status} ${statusText} but no error body.`);
    return { success: false, error: null, status };
  }

  if (!isApiError(data)) {
    console.error(`API Error: Received status ${status} ${statusText} with non-standard error body.`, data);
    return { success: false, error: null, status };
  }

  const error = data as ApiError;

  if (isValidationProblemDetails(error)) {
    console.error('Validation errors:', error.errors);
    return { success: false, error, status };
  }

  console.error(`API error: ${error.title ?? statusText}. ${error.detail ?? ''}`.trim());
  return { success: false, error, status };
};

const isApiError = (data: unknown): data is ApiError => {
  if (!data || typeof data !== 'object') {
    return false;
  }

  return 'title' in data || 'detail' in data;
};
