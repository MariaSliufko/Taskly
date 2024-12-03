// import { useState } from "react";
// import { PageSize } from "../../../components/Pagination";
// import { useGetTasksQuery } from "../services/tasksAPI";

// export default function TaskList(){
//     const [currentPage, setCurrentPage] = useState(1);
//     // const [pageSize, setPageSize] = useState<PageSize>(10);
//     const { data: tasksResponse, isLoading } = useGetTasksQuery({ pagination: { pageNumber: 1, pageSize: 10 } });

//     const { data } = useGetTasksQuery({ 
//         pagination: { pageNumber: 1, pageSize: 10 }
//     });

//     return (
//         <ul>
//         {data?.data.map(task => (
//           <li key={task.id}>{task.title}</li>
//         ))}
//       </ul>

//     );
// }

import { useState } from "react";
import Pagination from "../../../components/Pagination"; 
import { PageSize } from "../../../components/Pagination";
import { useGetTasksQuery } from "../services/tasksAPI";

export default function TaskList() {
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState<PageSize>(10);

  const { data: tasksResponse, isLoading, isError } = useGetTasksQuery({ 
    pagination: { pageNumber: currentPage, pageSize }
  });

  console.log("tasksResponse", tasksResponse);

  if (isLoading) {
    return <div>Loading tasks...</div>;
  }

  if (isError || !tasksResponse) {
    return <div>Error fetching tasks.</div>;
  }

  return (
    <div>
      <ul>
        {tasksResponse.data.map((task) => (
          <li key={task.id}>
            <strong>{task.title}</strong>
            <p>{task.description}</p>
            <small>Due: {task.dueDate ? task.dueDate : "No due date"}</small>
          </li>
        ))}
      </ul>
      <Pagination
        totalSize={tasksResponse.totalSize}
        pageSize={pageSize}
        currentPage={currentPage}
        onPageChange={(page) => setCurrentPage(page)}
        onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
      />
    </div>
  );
}
