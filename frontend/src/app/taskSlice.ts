// import { createSlice, PayloadAction } from '@reduxjs/toolkit';

// interface Task {
//   id: string;
//   title: string;
//   completed: boolean;
// }

// interface TaskState {
//   tasks: Task[];
// }

// const initialState: TaskState = {
//   tasks: [],
// };

// const taskSlice = createSlice({
//   name: 'tasks',
//   initialState,
//   reducers: {
//     setTasks(state, action: PayloadAction<Task[]>) {
//       state.tasks = action.payload;
//     },
//     toggleTaskCompleted(state, action: PayloadAction<string>) {
//       const task = state.tasks.find(task => task.id === action.payload);
//       if (task) {
//         task.completed = !task.completed;
//       }
//     },
//   },
// });

// export const { setTasks, toggleTaskCompleted } = taskSlice.actions;
// export default taskSlice.reducer;
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface Task {
  id: string;
  title: string;
  completed: boolean;
}

interface TaskState {
  tasks: Task[];
}

const initialState: TaskState = {
  tasks: [],
};

const taskSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {
    setTasks(state, action: PayloadAction<Task[]>) {
      state.tasks = action.payload;
    },
    toggleTaskCompleted(state, action: PayloadAction<string>) {
      const task = state.tasks.find(task => task.id === action.payload);
      if (task) {
        task.completed = !task.completed;
      }
    },
  },
});

export const { setTasks, toggleTaskCompleted } = taskSlice.actions;
export default taskSlice.reducer;
