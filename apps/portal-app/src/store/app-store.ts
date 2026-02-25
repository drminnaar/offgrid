// packages
import { configureStore } from '@reduxjs/toolkit';

// custom slices
import { globalUISlice } from './global-ui-slice';

// api slices
import { customerApi } from '../services/customers/customer-api';
import { productApi, productBrandApi, productTypeApi, productCategoryApi } from '../services/products';

export const appStore = configureStore({
  reducer: {
    // global ui state
    [globalUISlice.name]: globalUISlice.reducer,
    // api
    [customerApi.reducerPath]: customerApi.reducer,
    [productApi.reducerPath]: productApi.reducer,
    [productTypeApi.reducerPath]: productTypeApi.reducer,
    [productBrandApi.reducerPath]: productBrandApi.reducer,
    [productCategoryApi.reducerPath]: productCategoryApi.reducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware()
    .concat(customerApi.middleware)
    .concat(productApi.middleware)
    .concat(productTypeApi.middleware)
    .concat(productBrandApi.middleware)
    .concat(productCategoryApi.middleware),
});

/**
 * Infer the `RootState` and `AppDispatch` types from the store itself
 * - `RootState` will have the type of the state tree
 * - `AppDispatch` will have the type of the dispatch function
 */
export type RootState = ReturnType<typeof appStore.getState>;
export type AppDispatch = typeof appStore.dispatch;