import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

export interface ISalesOrderDetailIdentifier {

    // PredicateType:Equals
	salesOrderID: number | null;

    // PredicateType:Equals
	salesOrderDetailID: number | null;
}

export interface ISalesOrderDetailAdvancedQuery extends IBaseQuery {
    textSearch: string | null;
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	productID: number | null;

    // PredicateType:Equals
	productCategoryID: number | null;

    // PredicateType:Equals
	productCategory_ParentID: number | null;

    // PredicateType:Equals
	productModelID: number | null;

    // PredicateType:Equals
	salesOrderID: number | null;

    // PredicateType:Equals
	billToID: number | null;

    // PredicateType:Equals
	shipToID: number | null;

    // PredicateType:Equals
	customerID: number | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;    
}

export function defaultISalesOrderDetailAdvancedQuery(): ISalesOrderDetailAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
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

export function getSalesOrderDetailQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'salesOrderID', direction: 'asc', displayName: 'SalesOrderID', expression: 'SalesOrderID~ASC' } as IQueryOrderBySetting,
        { propertyName: 'salesOrderID', direction: 'desc', displayName: 'SalesOrderID', expression: 'SalesOrderID~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

