export const toPlaceholderImage = (url: string) => {
  if (!url || url.trim().length === 0 || url.trim().toLowerCase().includes('example')) {
    return '/placeholder.png';
  }
  return url;
};
