import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';

export interface IProductModelProductDescriptionIdentifier {

    // PredicateType:Equals
	productModelID: number | null;

    // PredicateType:Equals
	productDescriptionID: number | null;

    // PredicateType:Equals
	culture: string | null;
}

export function getIProductModelProductDescriptionIdentifier(item: IProductModelProductDescriptionDataModel): IProductModelProductDescriptionIdentifier {
    return { productModelID: item.productModelID, productDescriptionID: item.productDescriptionID, culture: item.culture };
}

export function compareIProductModelProductDescriptionIdentifier(a: IProductModelProductDescriptionIdentifier, b: IProductModelProductDescriptionIdentifier): boolean {
    return a.productModelID === b.productModelID && a.productDescriptionID === b.productDescriptionID && a.culture === b.culture;
}

export function getRouteParamsOfIProductModelProductDescriptionIdentifier(item: IProductModelProductDescriptionDataModel): string | number {
    return item.productModelID +  '/' + item.productDescriptionID +  '/' + item.culture;
}

export interface IProductModelProductDescriptionAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	productDescriptionID: number | null;

    // PredicateType:Equals
	productModelID: number | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    culture: string | null;
    cultureSearchType: TextSearchTypes;    
}

export function defaultIProductModelProductDescriptionAdvancedQuery(): IProductModelProductDescriptionAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        productDescriptionID: null, // PredicateType:Equals

        productModelID: null, // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        culture: "",
        cultureSearchType: TextSearchTypes.Contains,
    } as unknown as IProductModelProductDescriptionAdvancedQuery;
}

export function getProductModelProductDescriptionQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'culture', direction: 'asc', displayName: 'Culture', expression: 'Culture~ASC' } as IQueryOrderBySetting,
        { propertyName: 'culture', direction: 'desc', displayName: 'Culture', expression: 'Culture~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

