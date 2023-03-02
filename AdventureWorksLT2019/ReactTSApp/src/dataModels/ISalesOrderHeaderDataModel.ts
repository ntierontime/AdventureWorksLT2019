import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface ISalesOrderHeaderDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    salesOrderID: number;
    revisionNumber: number;
    orderDate: string;
    dueDate: string;
    shipDate: string;
    status: number;
    onlineOrderFlag: boolean;
    salesOrderNumber: string;
    purchaseOrderNumber: string;
    accountNumber: string;
    customer_Name: string;
    customerID: number | '';
    shipTo_Name: string;
    shipToAddressID: number | '';
    billTo_Name: string;
    billToAddressID: number | '';
    shipMethod: string;
    creditCardApprovalCode: string;
    subTotal: number;
    taxAmt: number;
    freight: number;
    totalDue: number;
    comment: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultSalesOrderHeader(): ISalesOrderHeaderDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        salesOrderID: 0,
        revisionNumber: 0,
        orderDate: new Date(),
        dueDate: new Date(),
        shipDate: new Date(),
        status: 0,
        onlineOrderFlag: false,
        salesOrderNumber: '',
        purchaseOrderNumber: '',
        accountNumber: '',
        customer_Name: '',
        customerID: '',
        shipTo_Name: '',
        shipToAddressID: '',
        billTo_Name: '',
        billToAddressID: '',
        shipMethod: '',
        creditCardApprovalCode: '',
        subTotal: 0,
        taxAmt: 0,
        freight: 0,
        totalDue: 0,
        comment: '',
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as ISalesOrderHeaderDataModel;
}

export function getSalesOrderHeaderAvatar(item: ISalesOrderHeaderDataModel): string {
    return !!item.salesOrderNumber && item.salesOrderNumber.length > 0 ? item.salesOrderNumber.substring(0, 1) : "?";
}


export const salesOrderHeaderFormValidationWhenCreate = Yup.object().shape({
    revisionNumber: Yup.number()
        .required('RevisionNumber_is_required'),
    orderDate: Yup.string()
        .required('OrderDate_is_required'),
    dueDate: Yup.string()
        .required('DueDate_is_required'),
    shipDate: Yup.string(),
    status: Yup.number()
        .required('Status_is_required'),
    onlineOrderFlag: Yup.boolean()
        .required('OnlineOrderFlag_is_required'),
    purchaseOrderNumber: Yup.string()
        .max(25, 'The_length_of_PurchaseOrderNumber_should_be_0_to_25'),
    accountNumber: Yup.string()
        .max(15, 'The_length_of_AccountNumber_should_be_0_to_15'),
    customerID: Yup.number(),
    shipToAddressID: Yup.number(),
    billToAddressID: Yup.number(),
    shipMethod: Yup.string()
        .min(1, 'The_length_of_ShipMethod_should_be_1_to_50')
        .max(50, 'The_length_of_ShipMethod_should_be_1_to_50'),
    creditCardApprovalCode: Yup.string()
        .max(15, 'The_length_of_CreditCardApprovalCode_should_be_0_to_15'),
    subTotal: Yup.number()
        .required('SubTotal_is_required'),
    taxAmt: Yup.number()
        .required('TaxAmt_is_required'),
    freight: Yup.number()
        .required('Freight_is_required'),
    comment: Yup.string(),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const salesOrderHeaderFormValidationWhenEdit = Yup.object().shape({
    revisionNumber: Yup.number()
        .required('RevisionNumber_is_required'),
    orderDate: Yup.string()
        .required('OrderDate_is_required'),
    dueDate: Yup.string()
        .required('DueDate_is_required'),
    shipDate: Yup.string(),
    status: Yup.number()
        .required('Status_is_required'),
    onlineOrderFlag: Yup.boolean()
        .required('OnlineOrderFlag_is_required'),
    purchaseOrderNumber: Yup.string()
        .max(25, 'The_length_of_PurchaseOrderNumber_should_be_0_to_25'),
    accountNumber: Yup.string()
        .max(15, 'The_length_of_AccountNumber_should_be_0_to_15'),
    customerID: Yup.number(),
    shipToAddressID: Yup.number(),
    billToAddressID: Yup.number(),
    shipMethod: Yup.string()
        .min(1, 'The_length_of_ShipMethod_should_be_1_to_50')
        .max(50, 'The_length_of_ShipMethod_should_be_1_to_50'),
    creditCardApprovalCode: Yup.string()
        .max(15, 'The_length_of_CreditCardApprovalCode_should_be_0_to_15'),
    subTotal: Yup.number()
        .required('SubTotal_is_required'),
    taxAmt: Yup.number()
        .required('TaxAmt_is_required'),
    freight: Yup.number()
        .required('Freight_is_required'),
    comment: Yup.string(),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const salesOrderHeaderAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

