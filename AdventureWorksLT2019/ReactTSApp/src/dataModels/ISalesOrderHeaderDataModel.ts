import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

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
    customerID: number | string;
    shipTo_Name: string;
    shipToAddressID: number | string;
    billTo_Name: string;
    billToAddressID: number | string;
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

export function getSalesOrderHeaderAvatar(item: ISalesOrderHeaderDataModel): string {
    return !!item.salesOrderNumber && item.salesOrderNumber.length > 0 ? item.salesOrderNumber.substring(0, 1) : "?";
}


export const salesOrderHeaderFormValidationWhenCreate = {
    revisionNumber: {
        required: 'RevisionNumber_is_required',
    },
    orderDate: {
        required: 'OrderDate_is_required',
    },
    dueDate: {
        required: 'DueDate_is_required',
    },
    shipDate: {
    },
    status: {
        required: 'Status_is_required',
    },
    onlineOrderFlag: {
        required: 'OnlineOrderFlag_is_required',
    },
    purchaseOrderNumber: {
        maxLength: {
            value: 25,
            message: 'The_length_of_PurchaseOrderNumber_should_be_0_to_25',
        },
    },
    accountNumber: {
        maxLength: {
            value: 15,
            message: 'The_length_of_AccountNumber_should_be_0_to_15',
        },
    },
    customerID: {
    },
    shipToAddressID: {
    },
    billToAddressID: {
    },
    shipMethod: {
        minlength: {
            value: 1,
            message: 'The_length_of_ShipMethod_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_ShipMethod_should_be_1_to_50',
        },
    },
    creditCardApprovalCode: {
        maxLength: {
            value: 15,
            message: 'The_length_of_CreditCardApprovalCode_should_be_0_to_15',
        },
    },
    subTotal: {
        required: 'SubTotal_is_required',
    },
    taxAmt: {
        required: 'TaxAmt_is_required',
    },
    freight: {
        required: 'Freight_is_required',
    },
    comment: {
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const salesOrderHeaderFormValidationWhenEdit = {
    revisionNumber: {
        required: 'RevisionNumber_is_required',
    },
    orderDate: {
        required: 'OrderDate_is_required',
    },
    dueDate: {
        required: 'DueDate_is_required',
    },
    shipDate: {
    },
    status: {
        required: 'Status_is_required',
    },
    onlineOrderFlag: {
        required: 'OnlineOrderFlag_is_required',
    },
    purchaseOrderNumber: {
        maxLength: {
            value: 25,
            message: 'The_length_of_PurchaseOrderNumber_should_be_0_to_25',
        },
    },
    accountNumber: {
        maxLength: {
            value: 15,
            message: 'The_length_of_AccountNumber_should_be_0_to_15',
        },
    },
    customerID: {
    },
    shipToAddressID: {
    },
    billToAddressID: {
    },
    shipMethod: {
        minlength: {
            value: 1,
            message: 'The_length_of_ShipMethod_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_ShipMethod_should_be_1_to_50',
        },
    },
    creditCardApprovalCode: {
        maxLength: {
            value: 15,
            message: 'The_length_of_CreditCardApprovalCode_should_be_0_to_15',
        },
    },
    subTotal: {
        required: 'SubTotal_is_required',
    },
    taxAmt: {
        required: 'TaxAmt_is_required',
    },
    freight: {
        required: 'Freight_is_required',
    },
    comment: {
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

