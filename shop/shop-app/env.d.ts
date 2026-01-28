/**
 * Custom types for environment variables (NEXT_PUBLIC_ prefix).
 * This file is created to avoid modifying next-env.d.ts.
 */
declare namespace NodeJS {
  interface ProcessEnv {

    /**
     * The name of the application.
     */
    readonly NEXT_PUBLIC_APP_NAME: string;

    /**
     * The description of the application.
     */
    readonly NEXT_PUBLIC_APP_DESCRIPTION: string;
  }
}