import { ICompositeModel } from "src/shared/dataModels/ICompositeModel";
import { ICustomerAddressDataModel } from "./ICustomerAddressDataModel";
import { ICustomerDataModel } from "./ICustomerDataModel";
import { ISalesOrderHeaderDataModel } from "./ISalesOrderHeaderDataModel";

export interface ICustomerCompositeModel extends ICompositeModel<ICustomerDataModel, ICustomerCompositeModel_DataOptions__> {
    // 4. ListTable = 4
    customerAddresses_Via_CustomerID: ICustomerAddressDataModel[];
    salesOrderHeaders_Via_CustomerID: ISalesOrderHeaderDataModel[];
}

export enum ICustomerCompositeModel_DataOptions__ {
    __Master__ = '__Master__',
    // 4. ListTable
    CustomerAddresses_Via_CustomerID = 'CustomerAddresses_Via_CustomerID',
    SalesOrderHeaders_Via_CustomerID = 'SalesOrderHeaders_Via_CustomerID',

}

