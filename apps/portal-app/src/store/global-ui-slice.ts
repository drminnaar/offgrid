// packages
import type { PaletteMode } from '@mui/material';
import { createSlice } from '@reduxjs/toolkit';

const PALETTE_MODE_KEY = 'palette-mode';

const initialPaletteMode = (): PaletteMode => {
  const storedMode = localStorage.getItem(PALETTE_MODE_KEY);
  try {
    if (storedMode) {
      return JSON.parse(storedMode) as PaletteMode;
    }
    return 'light';
  } catch {
    localStorage.removeItem(PALETTE_MODE_KEY);
    return 'light';
  }
};

export type GlobalUIState = {
  isLoading: boolean;
  paletteMode: PaletteMode;
};

const initialState: GlobalUIState = {
  isLoading: false,
  paletteMode: initialPaletteMode(),
};

const GLOBAL_UI_SLICE_NAME = 'globalUI';

export const globalUISlice = createSlice({
  name: GLOBAL_UI_SLICE_NAME,
  initialState: initialState,
  reducers: {
    startLoading: (state) => {
      state.isLoading = true;
    },
    stopLoading: (state) => {
      state.isLoading = false;
    },
    togglePaletteMode: (state) => {
      const newPaletteMode = state.paletteMode === 'light'
        ? 'dark'
        : 'light';

      localStorage.setItem(
        PALETTE_MODE_KEY,
        JSON.stringify(newPaletteMode)
      );

      state.paletteMode = newPaletteMode;
    },
  }
});

export const {
  startLoading,
  stopLoading,
  togglePaletteMode
} = globalUISlice.actions;