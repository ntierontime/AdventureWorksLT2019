import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';

export interface IProductCategoryIdentifier {

    // PredicateType:Equals
	productCategoryID: number | null;
}

export function getIProductCategoryIdentifier(item: IProductCategoryDataModel): IProductCategoryIdentifier {
    return { productCategoryID: item.productCategoryID };
}

export function compareIProductCategoryIdentifier(a: IProductCategoryIdentifier, b: IProductCategoryIdentifier): boolean {
    return a.productCategoryID === b.productCategoryID;
}

export function getRouteParamsOfIProductCategoryIdentifier(item: IProductCategoryDataModel): string | number {
    return item.productCategoryID;
}

export interface IProductCategoryAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	parentProductCategoryID: number | string | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    name: string | null;
    nameSearchType: TextSearchTypes;    
}

export function defaultIProductCategoryAdvancedQuery(): IProductCategoryAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        parentProductCategoryID: '', // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        name: "",
        nameSearchType: TextSearchTypes.Contains,
    } as unknown as IProductCategoryAdvancedQuery;
}

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

