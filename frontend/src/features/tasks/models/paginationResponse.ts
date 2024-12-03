export interface PaginationResponse<T> {
    data: T;
    totalSize: number;
    continueKeys?: string[];
  }
  