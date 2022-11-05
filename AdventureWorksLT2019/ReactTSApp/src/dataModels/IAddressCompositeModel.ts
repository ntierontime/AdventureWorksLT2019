import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IAddressDataModel } from "./IAddressDataModel";
import { ICustomerAddressDataModel } from "./ICustomerAddressDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface IAddressCompositeModel extends ICompositeModel<IAddressDataModel, IAddressCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    customerAddresses_Via_AddressID: ICustomerAddressDataModel[];
    salesOrderHeaders_Via_BillToAddressID: ISalesOrderHeaderDataModel[];
    salesOrderHeaders_Via_ShipToAddressID: ISalesOrderHeaderDataModel[];
}

export enum IAddressCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    customerAddresses_Via_AddressID = 'customerAddresses_Via_AddressID',
    salesOrderHeaders_Via_BillToAddressID = 'salesOrderHeaders_Via_BillToAddressID',
    salesOrderHeaders_Via_ShipToAddressID = 'salesOrderHeaders_Via_ShipToAddressID',

}

