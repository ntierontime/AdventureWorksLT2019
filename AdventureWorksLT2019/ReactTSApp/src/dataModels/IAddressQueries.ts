import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';

export interface IAddressIdentifier {

    // PredicateType:Equals
	addressID: number | null;
}

export function getIAddressIdentifier(item: IAddressDataModel): IAddressIdentifier {
    return { addressID: item.addressID };
}

export function compareIAddressIdentifier(a: IAddressIdentifier, b: IAddressIdentifier): boolean {
    return a.addressID === b.addressID;
}

export function getRouteParamsOfIAddressIdentifier(item: IAddressDataModel): string | number {
    return item.addressID;
}

export interface IAddressAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    addressLine1: string | null;
    addressLine1SearchType: TextSearchTypes;

    // PredicateType:Contains
    addressLine2: string | null;
    addressLine2SearchType: TextSearchTypes;

    // PredicateType:Contains
    city: string | null;
    citySearchType: TextSearchTypes;

    // PredicateType:Contains
    stateProvince: string | null;
    stateProvinceSearchType: TextSearchTypes;

    // PredicateType:Contains
    countryRegion: string | null;
    countryRegionSearchType: TextSearchTypes;

    // PredicateType:Contains
    postalCode: string | null;
    postalCodeSearchType: TextSearchTypes;    
}

export function defaultIAddressAdvancedQuery(): IAddressAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        addressLine1: "",
        addressLine1SearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        addressLine2: "",
        addressLine2SearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        city: "",
        citySearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        stateProvince: "",
        stateProvinceSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        countryRegion: "",
        countryRegionSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        postalCode: "",
        postalCodeSearchType: TextSearchTypes.Contains,
    } as unknown as IAddressAdvancedQuery;
}

export function getAddressQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'addressLine1', direction: 'asc', displayName: 'AddressLine1', expression: 'AddressLine1~ASC' } as IQueryOrderBySetting,
        { propertyName: 'addressLine1', direction: 'desc', displayName: 'AddressLine1', expression: 'AddressLine1~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

