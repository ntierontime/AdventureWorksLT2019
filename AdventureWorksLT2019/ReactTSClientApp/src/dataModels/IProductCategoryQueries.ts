import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface IProductCategoryIdentifier {
    // PredicateType:Equals
    productCategoryID: number | string | null; // we can pass enum name to WebApi
}

export function getIProductCategoryIdentifier(item: IProductCategoryDataModel): IProductCategoryIdentifier {
    return { productCategoryID: item.productCategoryID };
}

export function compareIProductCategoryIdentifier(a: IProductCategoryIdentifier, b: IProductCategoryIdentifier): boolean {
    return a.productCategoryID === b.productCategoryID;
}

export function getRouteParams(item: IProductCategoryIdentifier): string | number {
    return item.productCategoryID;
}

export function getRouteParamsOfIProductCategoryIdentifier(item: IProductCategoryDataModel): string | number {
    return item.productCategoryID;
}

export interface IProductCategoryAdvancedQuery extends IBaseQuery {
    // PredicateType:Equals
    parentProductCategoryID: number | string | null; // we can pass enum name to WebApi
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
    // PredicateType:Contains
    name: string | null;
    nameSearchType: TextSearchTypes;
}

export function defaultIProductCategoryAdvancedQuery(): IProductCategoryAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        parentProductCategoryID: null, // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        name: "",
        nameSearchType: TextSearchTypes.Contains,
    } as unknown as IProductCategoryAdvancedQuery;
}


export const productCategoryQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getProductCategoryQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'name', direction: 'asc', displayName: 'Name', expression: 'Name~ASC' } as IQueryOrderBySetting,
        { propertyName: 'name', direction: 'desc', displayName: 'Name', expression: 'Name~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

