export const toEmoji = (productType: string): string => {
  const type = productType.toLowerCase();

  switch (type) {
    case 'kayak':
      return '🛶';
    case 'surfboard':
      return '🏄🏼‍♂️';
    case 'bike':
      return '🚲';
    case 'snowboard':
      return '🏂🏽';
    case 'ski':
      return '⛷️';
    default:
      return '';
  }
};
