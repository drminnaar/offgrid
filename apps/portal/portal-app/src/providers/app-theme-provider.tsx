// packages
import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material';

// custom state/hooks
import { useAppSelector } from '../store';

export const AppThemeProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const { paletteMode } = useAppSelector((state) => state.globalUI);

  const theme = createTheme({
    palette: {
      mode: paletteMode,
      background: {
        default: paletteMode === 'light' ? '#E0E0E0' : '#263238',
      },
    },
  });

  return <ThemeProvider theme={theme}>{children}</ThemeProvider>;
};
