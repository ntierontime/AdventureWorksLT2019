import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { defaultSalesOrderDetail, ISalesOrderDetailDataModel } from "./ISalesOrderDetailDataModel";
import { IProductDataModel } from "./IProductDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface ISalesOrderDetailCompositeModel extends ICompositeModel<ISalesOrderDetailDataModel, ISalesOrderDetailCompositeModel_DataOptions__> {
    // 2. AncestorTable = 2
    product: IProductDataModel;
    salesOrderHeader: ISalesOrderHeaderDataModel;
}

export enum ISalesOrderDetailCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 2. AncestorTable
    Product = 'Product',
    SalesOrderHeader = 'SalesOrderHeader',

}

export function defaultISalesOrderDetailCompositeModel(): ISalesOrderDetailCompositeModel {
    return {
        responses: null,
        __Master__: defaultSalesOrderDetail(),
        // 2. AncestorTable = 2
        product: null,
        salesOrderHeader: null,
    };
}

