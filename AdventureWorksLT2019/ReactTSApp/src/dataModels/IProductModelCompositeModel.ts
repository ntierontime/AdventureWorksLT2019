import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductDataModel, defaultProduct } from "./IProductDataModel";
import { IProductModelDataModel, defaultProductModel } from "./IProductModelDataModel";
import { IProductModelProductDescriptionDataModel, defaultProductModelProductDescription } from "./IProductModelProductDescriptionDataModel";

export interface IProductModelCompositeModel extends ICompositeModel<IProductModelDataModel, IProductModelCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    products_Via_ProductModelID: IProductDataModel[];
    productModelProductDescriptions_Via_ProductModelID: IProductModelProductDescriptionDataModel[];
}

export enum IProductModelCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    Products_Via_ProductModelID = 'Products_Via_ProductModelID',
    ProductModelProductDescriptions_Via_ProductModelID = 'ProductModelProductDescriptions_Via_ProductModelID',

}

export function defaultIProductModelCompositeModel(): IProductModelCompositeModel {
    return {
        responses: null,
        __Master__: defaultProductModel(),
        // 4. ListTable = 4
        products_Via_ProductModelID: [ defaultProduct() ],
        productModelProductDescriptions_Via_ProductModelID: [ defaultProductModelProductDescription() ],
    };
}

