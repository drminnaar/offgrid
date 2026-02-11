// packages
import { useDispatch, useSelector } from 'react-redux';

// types
import type { AppDispatch, RootState } from './app-store';

// NOTE: Use throughout your app instead of plain `useDispatch` and `useSelector`
export const useAppDispatch = useDispatch.withTypes<AppDispatch>();
export const useAppSelector = useSelector.withTypes<RootState>();