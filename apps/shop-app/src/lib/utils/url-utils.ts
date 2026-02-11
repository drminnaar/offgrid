export const isValidUrl = (urlPath: string): boolean => {
  try {
    new URL(urlPath);
    return true;
  } catch {
    return false;
  }
};

export const joinUrl = (baseUrl: string, endpoint: string): string => {
  const base = baseUrl.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
  const path = endpoint.startsWith('/') ? endpoint : `/${endpoint}`;
  return `${base}${path}`;
};