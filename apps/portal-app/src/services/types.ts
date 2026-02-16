export type ProblemDetails = {
  type: string;
  title: string;
  status: number;
  detail?: string;
  instance?: string;
  [key: string]: unknown;
};

export type ValidationProblemDetails = ProblemDetails & {
  errors: Record<string, string[]>;
};

export type ApiError = ProblemDetails | ValidationProblemDetails;

export type ApiResponse<T> = { success: true; data: T; } | { success: false; error?: ApiError | null; status: number; };

export type PagedListResult<T> = {
  items: T[];
  currentPageNumber: number;
  itemCount: number;
  pageSize: number;
  pageCount: number;
  lastPageNumber: number;
  nextPageNumber?: number;
  previousPageNumber?: number;
  hasPrevious: boolean;
  hasNext: boolean;
};