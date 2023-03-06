import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";

export interface IPaginationResponse {
    pageSize: number;
    pageIndex: number;
    totalCount: number;
    count: number;
    lastPageIndex: number;
    startIndex: number;
    endIndex: number;
    enableFirstAndPrevious: boolean;
    enableLastAndNext: boolean;
    preCurrent: number[];
    postCurrent: number[];
    threeDotPreCurrent: boolean;
    threeDotPostCurrent: boolean;
    paginationOption: PaginationOptions;
}



export function defaultPaginationResponse(): IPaginationResponse {
    return {
        pageSize: 1,
        pageIndex: 10,
        totalCount: 0,
        count: 0,
        lastPageIndex: 0,
        startIndex: 0,
        endIndex: 0,
        enableFirstAndPrevious: false,
        enableLastAndNext: false,
        preCurrent: [],
        postCurrent: [],
        threeDotPreCurrent: false,
        threeDotPostCurrent: false,
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
    } as unknown as IPaginationResponse;
}