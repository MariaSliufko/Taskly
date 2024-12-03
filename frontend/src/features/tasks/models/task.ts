export interface Task {
    id: string; 
    title: string; 
    description: string; 
    priority: number; 
    dueDate: string | null; 
    isCompleted: boolean; 
    createdAt: string; 
  }
  