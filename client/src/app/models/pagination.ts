import { Product } from "./product";

export interface Pagination {
  pageIndex: number;
  pageSize: number;
  totalItemsCount: number;
  items: Product[];
}
