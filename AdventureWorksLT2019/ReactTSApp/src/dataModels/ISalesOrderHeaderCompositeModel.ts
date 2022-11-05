import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { ISalesOrderDetailDataModel } from "./ISalesOrderDetailDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface ISalesOrderHeaderCompositeModel extends ICompositeModel<ISalesOrderHeaderDataModel, ISalesOrderHeaderCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    salesOrderDetails_Via_SalesOrderID: ISalesOrderDetailDataModel[];
}

export enum ISalesOrderHeaderCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    salesOrderDetails_Via_SalesOrderID = 'salesOrderDetails_Via_SalesOrderID',

}

