import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductDataModel } from "./IProductDataModel";
import { IProductModelDataModel } from "./IProductModelDataModel";
import { IProductModelProductDescriptionDataModel } from "./IProductModelProductDescriptionDataModel";

export interface IProductModelCompositeModel extends ICompositeModel<IProductModelDataModel, IProductModelCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    Products_Via_ProductModelID: IProductDataModel[];
    ProductModelProductDescriptions_Via_ProductModelID: IProductModelProductDescriptionDataModel[];
}

export enum IProductModelCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    Products_Via_ProductModelID = 'Products_Via_ProductModelID',
    ProductModelProductDescriptions_Via_ProductModelID = 'ProductModelProductDescriptions_Via_ProductModelID',

}

