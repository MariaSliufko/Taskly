import type { PaginationRequest } from '../models/paginationRequest'

// export interface ListRequest {
//   pagination: PaginationRequest;
//   search?: string;
//   filters?: FilterModel[];
// }
// export interface FilterModel {
//   field: string;
//   value: string[];
// }

export interface ListRequest {
  pagination: {
    pageNumber: number;
    pageSize: number;   
  };
  search?: string;      
  sortBy?: string;     
}

