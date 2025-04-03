import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { BooleanSearchOptions } from 'src/shared/dataModels/BooleanSearchOptions';
import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface ISalesOrderHeaderIdentifier {
    // PredicateType:Equals
    salesOrderID: number | string | null; // we can pass enum name to WebApi
}

export function getISalesOrderHeaderIdentifier(item: ISalesOrderHeaderDataModel): ISalesOrderHeaderIdentifier {
    return { salesOrderID: item.salesOrderID };
}

export function compareISalesOrderHeaderIdentifier(a: ISalesOrderHeaderIdentifier, b: ISalesOrderHeaderIdentifier): boolean {
    return a.salesOrderID === b.salesOrderID;
}

export function getRouteParams(item: ISalesOrderHeaderIdentifier): string | number {
    return item.salesOrderID;
}

export function getRouteParamsOfISalesOrderHeaderIdentifier(item: ISalesOrderHeaderDataModel): string | number {
    return item.salesOrderID;
}

export interface ISalesOrderHeaderAdvancedQuery extends IBaseQuery {
    // PredicateType:Equals
    billToAddressID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    shipToAddressID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    customerID: number | string | null; // we can pass enum name to WebApi
    // PredicateType:Equals
    onlineOrderFlag: BooleanSearchOptions; // we can pass enum name to WebApi
        // PredicateType:Range
    orderDateRange: PreDefinedDateTimeRanges | null;
    orderDateRangeLower: string | null;
    orderDateRangeUpper: string | null;
        // PredicateType:Range
    dueDateRange: PreDefinedDateTimeRanges | null;
    dueDateRangeLower: string | null;
    dueDateRangeUpper: string | null;
        // PredicateType:Range
    shipDateRange: PreDefinedDateTimeRanges | null;
    shipDateRangeLower: string | null;
    shipDateRangeUpper: string | null;
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
    // PredicateType:Contains
    salesOrderNumber: string | null;
    salesOrderNumberSearchType: TextSearchTypes;
    // PredicateType:Contains
    purchaseOrderNumber: string | null;
    purchaseOrderNumberSearchType: TextSearchTypes;
    // PredicateType:Contains
    accountNumber: string | null;
    accountNumberSearchType: TextSearchTypes;
    // PredicateType:Contains
    shipMethod: string | null;
    shipMethodSearchType: TextSearchTypes;
    // PredicateType:Contains
    creditCardApprovalCode: string | null;
    creditCardApprovalCodeSearchType: TextSearchTypes;
    // PredicateType:Contains
    comment: string | null;
    commentSearchType: TextSearchTypes;
}

export function defaultISalesOrderHeaderAdvancedQuery(): ISalesOrderHeaderAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        billToAddressID: null, // PredicateType:Equals

        shipToAddressID: null, // PredicateType:Equals

        customerID: null, // PredicateType:Equals

        onlineOrderFlag: BooleanSearchOptions.All, // PredicateType:Equals

        // PredicateType:Range
        orderDateRange: PreDefinedDateTimeRanges.AllTime,
        orderDateRangeLower: null,
        orderDateRangeUpper: null,

        // PredicateType:Range
        dueDateRange: PreDefinedDateTimeRanges.AllTime,
        dueDateRangeLower: null,
        dueDateRangeUpper: null,

        // PredicateType:Range
        shipDateRange: PreDefinedDateTimeRanges.AllTime,
        shipDateRangeLower: null,
        shipDateRangeUpper: null,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        salesOrderNumber: "",
        salesOrderNumberSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        purchaseOrderNumber: "",
        purchaseOrderNumberSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        accountNumber: "",
        accountNumberSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        shipMethod: "",
        shipMethodSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        creditCardApprovalCode: "",
        creditCardApprovalCodeSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        comment: "",
        commentSearchType: TextSearchTypes.Contains,
    } as unknown as ISalesOrderHeaderAdvancedQuery;
}


export const salesOrderHeaderQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getSalesOrderHeaderQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'salesOrderNumber', direction: 'asc', displayName: 'SalesOrderNumber', expression: 'SalesOrderNumber~ASC' } as IQueryOrderBySetting,
        { propertyName: 'salesOrderNumber', direction: 'desc', displayName: 'SalesOrderNumber', expression: 'SalesOrderNumber~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'orderDate', direction: 'asc', displayName: 'OrderDate', expression: 'OrderDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'orderDate', direction: 'desc', displayName: 'OrderDate', expression: 'OrderDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

