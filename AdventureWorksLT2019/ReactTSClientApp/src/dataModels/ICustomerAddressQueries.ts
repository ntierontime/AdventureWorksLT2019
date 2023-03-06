import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';

export interface ICustomerAddressIdentifier {

    // PredicateType:Equals
	customerID: number | '';

    // PredicateType:Equals
	addressID: number | '';
}

export function getICustomerAddressIdentifier(item: ICustomerAddressDataModel): ICustomerAddressIdentifier {
    return { customerID: item.customerID, addressID: item.addressID };
}

export function compareICustomerAddressIdentifier(a: ICustomerAddressIdentifier, b: ICustomerAddressIdentifier): boolean {
    return a.customerID === b.customerID && a.addressID === b.addressID;
}

export function getRouteParamsOfICustomerAddressIdentifier(item: ICustomerAddressDataModel): string | number {
    return item.customerID +  '/' + item.addressID;
}

export interface ICustomerAddressAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	addressID: number | '';

    // PredicateType:Equals
	customerID: number | '';

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

