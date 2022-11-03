import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";

export interface IBaseQuery {
    pageSize: number;
    pageIndex: number;
    orderBys: string;
    paginationOption: PaginationOptions;
}
