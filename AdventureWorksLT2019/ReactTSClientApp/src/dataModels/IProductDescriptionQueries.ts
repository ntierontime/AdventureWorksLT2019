import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface IProductDescriptionIdentifier {
    // PredicateType:Equals
    productDescriptionID: number | string | null; // we can pass enum name to WebApi
}

export function getIProductDescriptionIdentifier(item: IProductDescriptionDataModel): IProductDescriptionIdentifier {
    return { productDescriptionID: item.productDescriptionID };
}

export function compareIProductDescriptionIdentifier(a: IProductDescriptionIdentifier, b: IProductDescriptionIdentifier): boolean {
    return a.productDescriptionID === b.productDescriptionID;
}

export function getRouteParams(item: IProductDescriptionIdentifier): string | number {
    return item.productDescriptionID;
}

export function getRouteParamsOfIProductDescriptionIdentifier(item: IProductDescriptionDataModel): string | number {
    return item.productDescriptionID;
}

export interface IProductDescriptionAdvancedQuery extends IBaseQuery {
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
    // PredicateType:Contains
    description: string | null;
    descriptionSearchType: TextSearchTypes;
}

export function defaultIProductDescriptionAdvancedQuery(): IProductDescriptionAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        description: "",
        descriptionSearchType: TextSearchTypes.Contains,
    } as unknown as IProductDescriptionAdvancedQuery;
}


export const productDescriptionQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getProductDescriptionQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'description', direction: 'asc', displayName: 'Description', expression: 'Description~ASC' } as IQueryOrderBySetting,
        { propertyName: 'description', direction: 'desc', displayName: 'Description', expression: 'Description~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

