export const safeParseJson = (text: string, contentType: string): unknown => {
  if (!text) {
    return null;
  }

  if (!contentType.includes('application/json')) {
    return text;
  }

  try {
    return JSON.parse(text);
  } catch (error) {
    console.error('API Error: Failed to parse JSON response.', error);
    return null;
  }
};