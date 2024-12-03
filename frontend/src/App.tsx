// import { useEffect } from 'react';
// import './App.css';

// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <p>Frontend kopplad till Backend</p>
//       </header>
//     </div>
//   );
// }

// export default App;

//-------------------------------------------------------------------------
// import { useEffect } from 'react';
// import { useDispatch, useSelector } from 'react-redux';
// import { RootState, AppDispatch } from './app/store';
// import { setTasks } from './app/taskSlice';

// function App() {
//   const dispatch = useDispatch<AppDispatch>();
//   const tasks = useSelector((state: RootState) => state.tasks.tasks);

//   useEffect(() => {
//     const mockTasks = [
//       { id: '1', title: 'Task 1', completed: false },
//       { id: '2', title: 'Task 2', completed: true },
//     ];
//     dispatch(setTasks(mockTasks));
//   }, [dispatch]);

//   return (
//     <div className="App">
//       <ul>
//         {tasks.map(task => (
//           <li key={task.id}>
//             {task.title} {task.completed ? '✅' : '❌'}
//           </li>
//         ))}
//       </ul>
//     </div>
//   );
// }

// export default App;

import { Suspense } from 'react';
import { RouterProvider, createBrowserRouter, createRoutesFromElements, Route } from 'react-router-dom';
import TaskList from './features/tasks/components/TaskList';
import Loading from './components/Loading';
import ErrorPage from './layout/ErrorPage';

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorPage />}>
      <Route
        index
        element={
          <Suspense fallback={<Loading />}>
            <TaskList />
          </Suspense>
        }
      />
    </Route>,
  ),
);

export default function App() {
  return (
    <Suspense fallback={<Loading />}>
      <RouterProvider router={router} />
    </Suspense>
  );
}

