import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductDataModel, defaultProduct } from "./IProductDataModel";
import { ISalesOrderDetailDataModel, defaultSalesOrderDetail } from "./ISalesOrderDetailDataModel";

export interface IProductCompositeModel extends ICompositeModel<IProductDataModel, IProductCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    salesOrderDetails_Via_ProductID: ISalesOrderDetailDataModel[];
}

export enum IProductCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    SalesOrderDetails_Via_ProductID = 'SalesOrderDetails_Via_ProductID',

}

export function defaultIProductCompositeModel(): IProductCompositeModel {
    return {
        responses: null,
        __Master__: defaultProduct(),
        // 4. ListTable = 4
        salesOrderDetails_Via_ProductID: [ defaultSalesOrderDetail() ],
    };
}

