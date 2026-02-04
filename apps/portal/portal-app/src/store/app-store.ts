// packages
import { configureStore } from '@reduxjs/toolkit';
import { globalUISlice } from './global-ui-slice';

export const appStore = configureStore({
  reducer: {
    // global ui state
    [globalUISlice.name]: globalUISlice.reducer,
  },
});

/**
 * Infer the `RootState` and `AppDispatch` types from the store itself
 * - `RootState` will have the type of the state tree
 * - `AppDispatch` will have the type of the dispatch function
 */
export type RootState = ReturnType<typeof appStore.getState>;
export type AppDispatch = typeof appStore.dispatch;