import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductCategoryDataModel } from "./IProductCategoryDataModel";
import { IProductDataModel } from "./IProductDataModel";
import { ISalesOrderDetailDataModel } from "./ISalesOrderDetailDataModel";

export interface IProductCompositeModel extends ICompositeModel<IProductDataModel, IProductCompositeModel_DataOptions__> {
    // 2. AncestorTable = 2
    productCategory: IProductCategoryDataModel;
    // 4. ListTable = 4
    salesOrderDetails_Via_ProductID: ISalesOrderDetailDataModel[];
}

export enum IProductCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 2. AncestorTable
    ProductCategory = 'ProductCategory',

    // 4. ListTable
    SalesOrderDetails_Via_ProductID = 'SalesOrderDetails_Via_ProductID',

}

