import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { BooleanSearchOptions } from "src/shared/dataModels/BooleanSearchOptions";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';

export interface ICustomerIdentifier {

    // PredicateType:Equals
	customerID: number | null;
}

export function getICustomerIdentifier(item: ICustomerDataModel): ICustomerIdentifier {
    return { customerID: item.customerID };
}

export function compareICustomerIdentifier(a: ICustomerIdentifier, b: ICustomerIdentifier): boolean {
    return a.customerID === b.customerID;
}

export function getRouteParamsOfICustomerIdentifier(item: ICustomerDataModel): string | number {
    return item.customerID;
}

export interface ICustomerAdvancedQuery extends IBaseQuery {
    textSearch: string | null;
    textSearchType: TextSearchTypes;

    // PredicateType:Equals
	nameStyle: BooleanSearchOptions;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    title: string | null;
    titleSearchType: TextSearchTypes;

    // PredicateType:Contains
    firstName: string | null;
    firstNameSearchType: TextSearchTypes;

    // PredicateType:Contains
    middleName: string | null;
    middleNameSearchType: TextSearchTypes;

    // PredicateType:Contains
    lastName: string | null;
    lastNameSearchType: TextSearchTypes;

    // PredicateType:Contains
    suffix: string | null;
    suffixSearchType: TextSearchTypes;

    // PredicateType:Contains
    companyName: string | null;
    companyNameSearchType: TextSearchTypes;

    // PredicateType:Contains
    salesPerson: string | null;
    salesPersonSearchType: TextSearchTypes;

    // PredicateType:Contains
    emailAddress: string | null;
    emailAddressSearchType: TextSearchTypes;

    // PredicateType:Contains
    phone: string | null;
    phoneSearchType: TextSearchTypes;

    // PredicateType:Contains
    passwordHash: string | null;
    passwordHashSearchType: TextSearchTypes;

    // PredicateType:Contains
    passwordSalt: string | null;
    passwordSaltSearchType: TextSearchTypes;    
}

export function defaultICustomerAdvancedQuery(): ICustomerAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
        textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        nameStyle: BooleanSearchOptions.All, // PredicateType:Equals

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        title: "",
        titleSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        firstName: "",
        firstNameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        middleName: "",
        middleNameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        lastName: "",
        lastNameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        suffix: "",
        suffixSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        companyName: "",
        companyNameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        salesPerson: "",
        salesPersonSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        emailAddress: "",
        emailAddressSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        phone: "",
        phoneSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        passwordHash: "",
        passwordHashSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        passwordSalt: "",
        passwordSaltSearchType: TextSearchTypes.Contains,
    } as unknown as ICustomerAdvancedQuery;
}

export function getCustomerQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'title', direction: 'asc', displayName: 'Title', expression: 'Title~ASC' } as IQueryOrderBySetting,
        { propertyName: 'title', direction: 'desc', displayName: 'Title', expression: 'Title~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

