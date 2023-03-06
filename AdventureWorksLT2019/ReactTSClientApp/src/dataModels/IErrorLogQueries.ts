import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';

export interface IErrorLogIdentifier {

    // PredicateType:Equals
	errorLogID: number | '';
}

export function getIErrorLogIdentifier(item: IErrorLogDataModel): IErrorLogIdentifier {
    return { errorLogID: item.errorLogID };
}

export function compareIErrorLogIdentifier(a: IErrorLogIdentifier, b: IErrorLogIdentifier): boolean {
    return a.errorLogID === b.errorLogID;
}

export function getRouteParamsOfIErrorLogIdentifier(item: IErrorLogDataModel): string | number {
    return item.errorLogID;
}

export interface IErrorLogAdvancedQuery extends IBaseQuery {
    textSearchType: TextSearchTypes;

    // PredicateType:Range
    errorTimeRange: PreDefinedDateTimeRanges | null;
    errorTimeRangeLower: Date | null;
    errorTimeRangeUpper: Date | null;

    // PredicateType:Contains
    userName: string | null;
    userNameSearchType: TextSearchTypes;

    // PredicateType:Contains
    errorProcedure: string | null;
    errorProcedureSearchType: TextSearchTypes;

    // PredicateType:Contains
    errorMessage: string | null;
    errorMessageSearchType: TextSearchTypes;    
}

export function defaultIErrorLogAdvancedQuery(): IErrorLogAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        // PredicateType:Range
        errorTimeRange: PreDefinedDateTimeRanges.AllTime,
        errorTimeRangeLower: null,
        errorTimeRangeUpper: null,

        // PredicateType:Contains
        userName: "",
        userNameSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        errorProcedure: "",
        errorProcedureSearchType: TextSearchTypes.Contains,

        // PredicateType:Contains
        errorMessage: "",
        errorMessageSearchType: TextSearchTypes.Contains,
    } as unknown as IErrorLogAdvancedQuery;
}

export function getErrorLogQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'userName', direction: 'asc', displayName: 'UserName', expression: 'UserName~ASC' } as IQueryOrderBySetting,
        { propertyName: 'userName', direction: 'desc', displayName: 'UserName', expression: 'UserName~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'errorTime', direction: 'asc', displayName: 'ErrorTime', expression: 'ErrorTime~ASC' } as IQueryOrderBySetting,
        { propertyName: 'errorTime', direction: 'desc', displayName: 'ErrorTime', expression: 'ErrorTime~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

