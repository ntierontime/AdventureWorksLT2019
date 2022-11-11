import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { BooleanSearchOptions } from "src/shared/dataModels/BooleanSearchOptions";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';

export interface ISalesOrderHeaderIdentifier {

    // PredicateType:Equals
	salesOrderID: number | null;
}

export function getISalesOrderHeaderIdentifier(item: ISalesOrderHeaderDataModel): ISalesOrderHeaderIdentifier {
    return { salesOrderID: item.salesOrderID };
}

export function compareISalesOrderHeaderIdentifier(a: ISalesOrderHeaderIdentifier, b: ISalesOrderHeaderIdentifier): boolean {
    return a.salesOrderID === b.salesOrderID;
}

export function getRouteParamsOfISalesOrderHeaderIdentifier(item: ISalesOrderHeaderDataModel): string | number {
    return item.salesOrderID;
}

export interface ISalesOrderHeaderAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	billToAddressID: number | null;

    // PredicateType:Equals
	shipToAddressID: number | null;

    // PredicateType:Equals
	customerID: number | null;

    // PredicateType:Equals
	onlineOrderFlag: BooleanSearchOptions;

    // PredicateType:Range
    orderDateRange: PreDefinedDateTimeRanges | null;
    orderDateRangeLower: Date | null;
    orderDateRangeUpper: Date | null;

    // PredicateType:Range
    dueDateRange: PreDefinedDateTimeRanges | null;
    dueDateRangeLower: Date | null;
    dueDateRangeUpper: Date | null;

    // PredicateType:Range
    shipDateRange: PreDefinedDateTimeRanges | null;
    shipDateRangeLower: Date | null;
    shipDateRangeUpper: Date | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

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
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
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

