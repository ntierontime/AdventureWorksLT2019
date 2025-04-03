import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { IProductDataModel } from 'src/dataModels/IProductDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface IProductIdentifier {
    // PredicateType:Equals
    productID: number | string | null; // we can pass enum name to WebApi
}

export function getIProductIdentifier(item: IProductDataModel): IProductIdentifier {
    return { productID: item.productID };
}

export function compareIProductIdentifier(a: IProductIdentifier, b: IProductIdentifier): boolean {
    return a.productID === b.productID;
}

export function getRouteParams(item: IProductIdentifier): string | number {
    return item.productID;
}

export function getRouteParamsOfIProductIdentifier(item: IProductDataModel): string | number {
    return item.productID;
}

export interface IProductAdvancedQuery extends IBaseQuery {
    // PredicateType:Equals
    productCategoryID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    parentID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    productModelID: number | string | null; // we can pass enum name to WebApi
        // PredicateType:Range
    sellStartDateRange: PreDefinedDateTimeRanges | null;
    sellStartDateRangeLower: string | null;
    sellStartDateRangeUpper: string | null;
        // PredicateType:Range
    sellEndDateRange: PreDefinedDateTimeRanges | null;
    sellEndDateRangeLower: string | null;
    sellEndDateRangeUpper: string | null;
        // PredicateType:Range
    discontinuedDateRange: PreDefinedDateTimeRanges | null;
    discontinuedDateRangeLower: string | null;
    discontinuedDateRangeUpper: string | null;
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
    // PredicateType:Contains
    name: string | null;
    nameSearchType: TextSearchTypes;
    // PredicateType:Contains
    productNumber: string | null;
    productNumberSearchType: TextSearchTypes;
    // PredicateType:Contains
    color: string | null;
    colorSearchType: TextSearchTypes;
    // PredicateType:Contains
    size: string | null;
    sizeSearchType: TextSearchTypes;
    // PredicateType:Contains
    thumbnailPhotoFileName: string | null;
    thumbnailPhotoFileNameSearchType: TextSearchTypes;
}

export function defaultIProductAdvancedQuery(): IProductAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        productCategoryID: null, // PredicateType:Equals

        parentID: null, // PredicateType:Equals

        productModelID: null, // PredicateType:Equals

        // PredicateType:Range
        sellStartDateRange: PreDefinedDateTimeRanges.AllTime,
        sellStartDateRangeLower: null,
        sellStartDateRangeUpper: null,

        // PredicateType:Range
        sellEndDateRange: PreDefinedDateTimeRanges.AllTime,
        sellEndDateRangeLower: null,
        sellEndDateRangeUpper: null,

        // PredicateType:Range
        discontinuedDateRange: PreDefinedDateTimeRanges.AllTime,
        discontinuedDateRangeLower: null,
        discontinuedDateRangeUpper: null,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        name: "",
        nameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        productNumber: "",
        productNumberSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        color: "",
        colorSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        size: "",
        sizeSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        thumbnailPhotoFileName: "",
        thumbnailPhotoFileNameSearchType: TextSearchTypes.Contains,
    } as unknown as IProductAdvancedQuery;
}


export const productQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getProductQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'name', direction: 'asc', displayName: 'Name', expression: 'Name~ASC' } as IQueryOrderBySetting,
        { propertyName: 'name', direction: 'desc', displayName: 'Name', expression: 'Name~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'sellStartDate', direction: 'asc', displayName: 'SellStartDate', expression: 'SellStartDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'sellStartDate', direction: 'desc', displayName: 'SellStartDate', expression: 'SellStartDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

