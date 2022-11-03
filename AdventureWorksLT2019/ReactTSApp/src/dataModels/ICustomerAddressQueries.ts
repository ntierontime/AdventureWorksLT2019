import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

export interface ICustomerAddressIdentifier {

    // PredicateType:Equals
	customerID: number | null;

    // PredicateType:Equals
	addressID: number | null;
}

export interface ICustomerAddressAdvancedQuery extends IBaseQuery {
    textSearch: string | null;
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	addressID: number | null;

    // PredicateType:Equals
	customerID: number | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    addressType: string | null;
    addressTypeSearchType: TextSearchTypes;    
}

export function defaultICustomerAddressAdvancedQuery(): ICustomerAddressAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
        textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        addressID: null, // PredicateType:Equals

        customerID: null, // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        addressType: "",
        addressTypeSearchType: TextSearchTypes.Contains,
    } as unknown as ICustomerAddressAdvancedQuery;
}

export function getCustomerAddressQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'addressType', direction: 'asc', displayName: 'AddressType', expression: 'AddressType~ASC' } as IQueryOrderBySetting,
        { propertyName: 'addressType', direction: 'desc', displayName: 'AddressType', expression: 'AddressType~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

