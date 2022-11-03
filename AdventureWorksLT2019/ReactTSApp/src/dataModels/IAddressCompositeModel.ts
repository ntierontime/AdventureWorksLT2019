import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { IAddressDataModel } from "./IAddressDataModel";
import { ICustomerAddressDataModel } from "./ICustomerAddressDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface IAddressCompositeModel extends ICompositeModel<IAddressDataModel, IAddressCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    CustomerAddresses_Via_AddressID: ICustomerAddressDataModel[];
    SalesOrderHeaders_Via_BillToAddressID: ISalesOrderHeaderDataModel[];
    SalesOrderHeaders_Via_ShipToAddressID: ISalesOrderHeaderDataModel[];
}

export enum IAddressCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    CustomerAddresses_Via_AddressID = 'CustomerAddresses_Via_AddressID',
    SalesOrderHeaders_Via_BillToAddressID = 'SalesOrderHeaders_Via_BillToAddressID',
    SalesOrderHeaders_Via_ShipToAddressID = 'SalesOrderHeaders_Via_ShipToAddressID',

}

