export const isValidUrl = (urlPath: string): boolean => {
  try {
    new URL(urlPath);
    return true;
  } catch {
    return false;
  }
};
