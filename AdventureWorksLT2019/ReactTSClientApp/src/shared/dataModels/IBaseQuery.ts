import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";

export interface IBaseQuery {
    // PredicateType:GeographyRange
    spatialLocation: any | null;
    spatialLocationRadius: number | null;
    // PredicateType:GeographyIntersects
    spatialLocationGeographyIntersects: any | null;
    
    textSearch: string;
    
    pageSize: number;
    pageIndex: number;
    orderBys: string;
    paginationOption: PaginationOptions;
}
