import { Product } from "./product";

export interface Pagination {
  pageIndex: number;
  pageSize: number;
  totalItemsCount: number;
  items: Product[];
}

export class PaginationCache implements Pagination{
  pageIndex: number;
  pageSize: number;
  totalItemsCount: number;
  items: Product[] =[];
}