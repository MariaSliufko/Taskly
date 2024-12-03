import { configureStore, combineReducers } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import taskSlice from '../features/tasks/services/taskSlice';
import { tasksAPI } from '../features/tasks/services/tasksAPI';

const rootReducer = combineReducers({
  tasks: taskSlice,
  [tasksAPI.reducerPath]: tasksAPI.reducer,
});

export const store = configureStore({
  reducer: rootReducer,
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware().concat(tasksAPI.middleware),
});

setupListeners(store.dispatch);

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;
