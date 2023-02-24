import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { defaultSalesOrderHeader, ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";
import { ISalesOrderDetailDataModel } from "./ISalesOrderDetailDataModel";

export interface ISalesOrderHeaderCompositeModel extends ICompositeModel<ISalesOrderHeaderDataModel, ISalesOrderHeaderCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    salesOrderDetails_Via_SalesOrderID: ISalesOrderDetailDataModel[];
}

export enum ISalesOrderHeaderCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    SalesOrderDetails_Via_SalesOrderID = 'SalesOrderDetails_Via_SalesOrderID',

}

export function defaultISalesOrderHeaderCompositeModel(): ISalesOrderHeaderCompositeModel {
    return {
        responses: null,
        __Master__: defaultSalesOrderHeader(),
        // 4. ListTable = 4
        salesOrderDetails_Via_SalesOrderID: [] as ISalesOrderDetailDataModel[],
    };
}

