import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IBuildVersionDataModel, defaultBuildVersion } from "./IBuildVersionDataModel";

export interface IBuildVersionCompositeModel extends ICompositeModel<IBuildVersionDataModel, IBuildVersionCompositeModel_DataOptions__> {

}

export enum IBuildVersionCompositeModel_DataOptions__ {
    __Master__ = '__Master__',

}

export function defaultIBuildVersionCompositeModel(): IBuildVersionCompositeModel {
    return {
        responses: null,
        __Master__: defaultBuildVersion(),

    };
}

