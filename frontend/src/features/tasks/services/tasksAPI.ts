// import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
// import { PaginationResponse } from '../models/paginationResponse';
// import { ListRequest } from '../models/listRequest';
// import { Task } from '../models/task'

// // const apiUrl = process.env.REACT_APP_BACKEND_URL as string;
// const apiUrl = import.meta.env.VITE_BACKEND_URL as string;

// export const tasksAPI = createApi({
//   reducerPath: 'tasksAPI',
//   baseQuery: fetchBaseQuery({
//     baseUrl: `${apiUrl}/tasks/`,
//   }),
//   tagTypes: ['Task'], 
//   endpoints: (builder) => ({
//     getTasks: builder.query<PaginationResponse<Task[]>, ListRequest>({
//       query: (params) => ({
//         url: 'list', 
//         method: 'GET',
//         params, 
//       }),
//       providesTags: ['Task'], 
//     }),
//   }),
// });

// export const { useGetTasksQuery, useLazyGetTasksQuery } = tasksAPI;

import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { PaginationResponse } from '../models/paginationResponse';
import { ListRequest } from '../models/listRequest';
import { Task } from '../models/task'

const apiUrl = import.meta.env.VITE_BACKEND_URL as string;

export const tasksAPI = createApi({
  reducerPath: 'tasksAPI',
  baseQuery: fetchBaseQuery({
    baseUrl: `${apiUrl}/tasks/`,
  }),
  tagTypes: ['Task'], 
  endpoints: (builder) => ({
    getTasks: builder.query<PaginationResponse<Task[]>, ListRequest>({
      query: (params) => ({
        url: 'list', 
        method: 'GET',
        params: {
            Take: params.pagination.pageSize, 
            Skip: (params.pagination.pageNumber - 1) * params.pagination.pageSize, 
            Search: params.search || '',
            SortBy: params.sortBy || 'title',
        }, 
      }),
      providesTags: ['Task'], 
    }),
  }),
});

export const { useGetTasksQuery, useLazyGetTasksQuery } = tasksAPI;

