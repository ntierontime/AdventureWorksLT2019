import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { PreDefinedDateTimeRanges } from "src/shared/dataModels/PreDefinedDateTimeRanges";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";

export interface IBuildVersionIdentifier {

    // PredicateType:Equals
	systemInformationID: number | null;

    // PredicateType:Equals
	versionDate: string | null;

    // PredicateType:Equals
	modifiedDate: string | null;
}

export interface IBuildVersionAdvancedQuery extends IBaseQuery {
    textSearch: string | null;
    textSearchType: TextSearchTypes;

    // PredicateType:Range
    versionDateRange: PreDefinedDateTimeRanges | null;
    versionDateRangeLower: Date | null;
    versionDateRangeUpper: Date | null;

    // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: Date | null;
    modifiedDateRangeUpper: Date | null;

    // PredicateType:Contains
    database_Version: string | null;
    database_VersionSearchType: TextSearchTypes;    
}

export function defaultIBuildVersionAdvancedQuery(): IBuildVersionAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.PageIndexesAndAllButtons,
        textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        // PredicateType:Range
        versionDateRange: PreDefinedDateTimeRanges.AllTime,
        versionDateRangeLower: null,
        versionDateRangeUpper: null,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        database_Version: "",
        database_VersionSearchType: TextSearchTypes.Contains,
    } as unknown as IBuildVersionAdvancedQuery;
}

export function getBuildVersionQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'database_Version', direction: 'asc', displayName: 'Database_Version', expression: 'Database_Version~ASC' } as IQueryOrderBySetting,
        { propertyName: 'database_Version', direction: 'desc', displayName: 'Database_Version', expression: 'Database_Version~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'versionDate', direction: 'asc', displayName: 'VersionDate', expression: 'VersionDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'versionDate', direction: 'desc', displayName: 'VersionDate', expression: 'VersionDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

