import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


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
    customerID: number | null | '';
    shipTo_Name: string;
    shipToAddressID: number | null | '';
    billTo_Name: string;
    billToAddressID: number | null | '';
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
        orderDate: '',
        dueDate: '',
        shipDate: '',
        status: 0,
        onlineOrderFlag: false,
        salesOrderNumber: '',
        purchaseOrderNumber: '',
        accountNumber: '',
        customer_Name: '',
        customerID: 0,
        shipTo_Name: '',
        shipToAddressID: 0,
        billTo_Name: '',
        billToAddressID: 0,
        shipMethod: '',
        creditCardApprovalCode: '',
        subTotal: 0,
        taxAmt: 0,
        freight: 0,
        totalDue: 0,
        comment: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as ISalesOrderHeaderDataModel;
}

// getSalesOrderHeaderAvatar will be used when Display the avatar of a SalesOrderHeader only
export function getSalesOrderHeaderAvatar(item: ISalesOrderHeaderDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.salesOrderNumber]);
}

// getSalesOrderHeaderTitle will be used when Display the title of a SalesOrderHeader only
export function getSalesOrderHeaderTitle(item: ISalesOrderHeaderDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.salesOrderNumber]);
}


export const salesOrderHeaderFormValidation = Yup.object().shape({
    revisionNumber: Yup.number()
        .required('RevisionNumber_is_required'),
    status: Yup.number()
        .required('Status_is_required'),
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
});

export const salesOrderHeaderAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

