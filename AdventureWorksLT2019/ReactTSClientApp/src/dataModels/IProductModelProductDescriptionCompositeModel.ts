import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IProductModelProductDescriptionDataModel, defaultProductModelProductDescription } from "./IProductModelProductDescriptionDataModel";

export interface IProductModelProductDescriptionCompositeModel extends ICompositeModel<IProductModelProductDescriptionDataModel, IProductModelProductDescriptionCompositeModel_DataOptions__> {

}

export enum IProductModelProductDescriptionCompositeModel_DataOptions__ {
    __Master__ = '__Master__',

}

export function defaultIProductModelProductDescriptionCompositeModel(): IProductModelProductDescriptionCompositeModel {
    return {
        responses: null,
        __Master__: defaultProductModelProductDescription(),

    };
}

