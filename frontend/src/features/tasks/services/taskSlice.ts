import { createSlice } from '@reduxjs/toolkit';

export interface TaskState {
  selectedTaskId: string | null;
}

export const taskSlice = createSlice({
  name: 'task',
  initialState: {
    selectedTaskId: null,
  },
  reducers: {
    setSelectedTaskId: (state, action) => {
      state.selectedTaskId = action.payload;
    },
  },
  selectors: {
    getSelectedTaskId: (state: TaskState) => state.selectedTaskId,
  },
});

export const { setSelectedTaskId } = taskSlice.actions;
export const { getSelectedTaskId } = taskSlice.selectors;
export default taskSlice;
