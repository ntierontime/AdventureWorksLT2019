import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { defaultCustomerAddress, ICustomerAddressDataModel } from "./ICustomerAddressDataModel";

export interface ICustomerAddressCompositeModel extends ICompositeModel<ICustomerAddressDataModel, ICustomerAddressCompositeModel_DataOptions__> {

}

export enum ICustomerAddressCompositeModel_DataOptions__ {
    __Master__ = '__Master__',

}

export function defaultICustomerAddressCompositeModel(): ICustomerAddressCompositeModel {
    return {
        responses: null,
        __Master__: defaultCustomerAddress(),

    };
}

