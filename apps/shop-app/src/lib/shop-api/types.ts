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