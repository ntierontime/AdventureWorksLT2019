import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductCategoryDataModel, defaultProductCategory } from "./IProductCategoryDataModel";
import { IProductDataModel, defaultProduct } from "./IProductDataModel";

export interface IProductCategoryCompositeModel extends ICompositeModel<IProductCategoryDataModel, IProductCategoryCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    products_Via_ProductCategoryID: IProductDataModel[];
    productCategories_Via_ParentProductCategoryID: IProductCategoryDataModel[];
}

export enum IProductCategoryCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    Products_Via_ProductCategoryID = 'Products_Via_ProductCategoryID',
    ProductCategories_Via_ParentProductCategoryID = 'ProductCategories_Via_ParentProductCategoryID',

}

export function defaultIProductCategoryCompositeModel(): IProductCategoryCompositeModel {
    return {
        responses: null,
        __Master__: defaultProductCategory(),
        // 4. ListTable = 4
        products_Via_ProductCategoryID: [ defaultProduct() ],
        productCategories_Via_ParentProductCategoryID: [ defaultProductCategory() ],
    };
}

