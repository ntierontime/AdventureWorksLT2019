import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { defaultProductDescription, IProductDescriptionDataModel } from "./IProductDescriptionDataModel";
import { IProductModelProductDescriptionDataModel } from "./IProductModelProductDescriptionDataModel";

export interface IProductDescriptionCompositeModel extends ICompositeModel<IProductDescriptionDataModel, IProductDescriptionCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    productModelProductDescriptions_Via_ProductDescriptionID: IProductModelProductDescriptionDataModel[];
}

export enum IProductDescriptionCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    ProductModelProductDescriptions_Via_ProductDescriptionID = 'ProductModelProductDescriptions_Via_ProductDescriptionID',

}

export function defaultIProductDescriptionCompositeModel(): IProductDescriptionCompositeModel {
    return {
        responses: null,
        __Master__: defaultProductDescription(),
        // 4. ListTable = 4
        productModelProductDescriptions_Via_ProductDescriptionID: [] as IProductModelProductDescriptionDataModel[],
    };
}

