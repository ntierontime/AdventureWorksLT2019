import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductCategoryDataModel } from "./IProductCategoryDataModel";
import { IProductDataModel } from "./IProductDataModel";

export interface IProductCategoryCompositeModel extends ICompositeModel<IProductCategoryDataModel, IProductCategoryCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    products_Via_ProductCategoryID: IProductDataModel[];
    productCategories_Via_ParentProductCategoryID: IProductCategoryDataModel[];
}

export enum IProductCategoryCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    products_Via_ProductCategoryID = 'products_Via_ProductCategoryID',
    productCategories_Via_ParentProductCategoryID = 'productCategories_Via_ParentProductCategoryID',

}

