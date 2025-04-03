import { IBaseQuery } from "src/shared/dataModels/IBaseQuery";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { IQueryOrderBySetting } from "src/shared/viewModels/IQueryOrderBySetting";
import { TextSearchTypes } from "src/shared/views/TextSearchTypes";
import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { PreDefinedDateTimeRanges } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


export interface IProductModelIdentifier {
    // PredicateType:Equals
    productModelID: number | string | null; // we can pass enum name to WebApi
}

export function getIProductModelIdentifier(item: IProductModelDataModel): IProductModelIdentifier {
    return { productModelID: item.productModelID };
}

export function compareIProductModelIdentifier(a: IProductModelIdentifier, b: IProductModelIdentifier): boolean {
    return a.productModelID === b.productModelID;
}

export function getRouteParams(item: IProductModelIdentifier): string | number {
    return item.productModelID;
}

export function getRouteParamsOfIProductModelIdentifier(item: IProductModelDataModel): string | number {
    return item.productModelID;
}

export interface IProductModelAdvancedQuery extends IBaseQuery {
        // PredicateType:Range
    modifiedDateRange: PreDefinedDateTimeRanges | null;
    modifiedDateRangeLower: string | null;
    modifiedDateRangeUpper: string | null;
    // PredicateType:Contains
    name: string | null;
    nameSearchType: TextSearchTypes;
}

export function defaultIProductModelAdvancedQuery(): IProductModelAdvancedQuery {
    return {
        pageSize: 10,
        pageIndex: 1,
        orderBys: '',
        paginationOption: PaginationOptions.Paged,
		textSearch: '',
        textSearchType: TextSearchTypes.Contains,

        // PredicateType:Range
        modifiedDateRange: PreDefinedDateTimeRanges.AllTime,
        modifiedDateRangeLower: null,
        modifiedDateRangeUpper: null,

        // PredicateType:Contains
        name: "",
        nameSearchType: TextSearchTypes.Contains,
    } as unknown as IProductModelAdvancedQuery;
}


export const productModelQueryOrderBySettings =    
[

] as unknown as IQueryOrderBySetting[];
export function getProductModelQueryOrderBySettings(): IQueryOrderBySetting[] {
    const orderBys =    
    [
        { propertyName: 'name', direction: 'asc', displayName: 'Name', expression: 'Name~ASC' } as IQueryOrderBySetting,
        { propertyName: 'name', direction: 'desc', displayName: 'Name', expression: 'Name~DESC' } as IQueryOrderBySetting, 
        { propertyName: 'modifiedDate', direction: 'asc', displayName: 'ModifiedDate', expression: 'ModifiedDate~ASC' } as IQueryOrderBySetting,
        { propertyName: 'modifiedDate', direction: 'desc', displayName: 'ModifiedDate', expression: 'ModifiedDate~DESC' } as IQueryOrderBySetting, 
    ] as unknown as IQueryOrderBySetting[];

    return orderBys;
}

