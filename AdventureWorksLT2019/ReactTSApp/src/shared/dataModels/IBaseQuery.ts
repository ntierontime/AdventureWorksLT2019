import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";

export interface IBaseQuery {
    textSearch: string;
    pageSize: number;
    pageIndex: number;
    orderBys: string;
    paginationOption: PaginationOptions;
}
