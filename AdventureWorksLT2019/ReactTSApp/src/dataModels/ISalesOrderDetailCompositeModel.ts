import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductCategoryDataModel } from "./IProductCategoryDataModel";
import { IProductDataModel } from "./IProductDataModel";
import { ISalesOrderDetailDataModel } from "./ISalesOrderDetailDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface ISalesOrderDetailCompositeModel extends ICompositeModel<ISalesOrderDetailDataModel, ISalesOrderDetailCompositeModel_DataOptions__> {
    // 2. AncestorTable = 2
    product: IProductDataModel;
    productCategory: IProductCategoryDataModel;
    salesOrderHeader: ISalesOrderHeaderDataModel;
}

export enum ISalesOrderDetailCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 2. AncestorTable
    Product = 'Product',
    ProductCategory = 'ProductCategory',
    SalesOrderHeader = 'SalesOrderHeader',

}

