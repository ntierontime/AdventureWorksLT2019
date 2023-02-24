import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { defaultErrorLog, IErrorLogDataModel } from "./IErrorLogDataModel";

export interface IErrorLogCompositeModel extends ICompositeModel<IErrorLogDataModel, IErrorLogCompositeModel_DataOptions__> {

}

export enum IErrorLogCompositeModel_DataOptions__ {
    __Master__ = '__Master__',

}

export function defaultIErrorLogCompositeModel(): IErrorLogCompositeModel {
    return {
        responses: null,
        __Master__: defaultErrorLog(),

    };
}

