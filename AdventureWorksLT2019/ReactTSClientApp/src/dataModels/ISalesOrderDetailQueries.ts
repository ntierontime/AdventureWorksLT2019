import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface ISalesOrderDetailIdentifier {
    // PredicateType:Equals
    salesOrderID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    salesOrderDetailID: number | string | null; // we can pass enum name to WebApi
}

export function getISalesOrderDetailIdentifier(item: ISalesOrderDetailDataModel): ISalesOrderDetailIdentifier {
    return { salesOrderID: item.salesOrderID, salesOrderDetailID: item.salesOrderDetailID };
}

export function compareISalesOrderDetailIdentifier(a: ISalesOrderDetailIdentifier, b: ISalesOrderDetailIdentifier): boolean {
    return a.salesOrderID === b.salesOrderID && a.salesOrderDetailID === b.salesOrderDetailID;
}

export function getRouteParams(item: ISalesOrderDetailIdentifier): string | number {
    return item.salesOrderID +  '/' + item.salesOrderDetailID;
}

export function getRouteParamsOfISalesOrderDetailIdentifier(item: ISalesOrderDetailDataModel): string | number {
    return item.salesOrderID +  '/' + item.salesOrderDetailID;
}

export interface ISalesOrderDetailAdvancedQuery extends IBaseQuery {
    // PredicateType:Equals
    productID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    productCategoryID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    productCategory_ParentID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    productModelID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    salesOrderID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    billToID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    shipToID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    customerID: number | string | null; // we can pass enum name to WebApi
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
}

export function defaultISalesOrderDetailAdvancedQuery(): ISalesOrderDetailAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        productID: null, // PredicateType:Equals

        productCategoryID: null, // PredicateType:Equals

        productCategory_ParentID: null, // PredicateType:Equals

        productModelID: null, // PredicateType:Equals

        salesOrderID: null, // PredicateType:Equals

        billToID: null, // PredicateType:Equals

        shipToID: null, // PredicateType:Equals

        customerID: null, // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,
    } as unknown as ISalesOrderDetailAdvancedQuery;
}


export const salesOrderDetailQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getSalesOrderDetailQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'salesOrderDetailID', direction: 'asc', displayName: 'SalesOrderDetailID', expression: 'SalesOrderDetailID~ASC' } as IQueryOrderBySetting,
        { propertyName: 'salesOrderDetailID', direction: 'desc', displayName: 'SalesOrderDetailID', expression: 'SalesOrderDetailID~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

