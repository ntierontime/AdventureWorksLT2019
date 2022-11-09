import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface ISalesOrderDetailDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    shipToID: number | string;
    shipTo_Name: string;
    customerID: number | string;
    customer_Name: string;
    billToID: number | string;
    billTo_Name: string;
    salesOrderHeader_Name: string;
    salesOrderID: number | string;
    salesOrderDetailID: number;
    orderQty: number;
    productModelID: number | string;
    productModel_Name: string;
    productCategory_ParentID: number | string;
    productCategory_Parent_Name: string;
    productCategoryID: number | string;
    productCategory_Name: string;
    product_Name: string;
    productID: number | string;
    unitPrice: number;
    unitPriceDiscount: number;
    lineTotal: number;
    rowguid: any;
    modifiedDate: string;
}

export function defaultSalesOrderDetail(): ISalesOrderDetailDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        shipToID: 0,
        shipTo_Name: '',
        customerID: 0,
        customer_Name: '',
        billToID: 0,
        billTo_Name: '',
        salesOrderHeader_Name: '',
        salesOrderID: 0,
        salesOrderDetailID: 0,
        orderQty: 0,
        productModelID: 0,
        productModel_Name: '',
        productCategory_ParentID: 0,
        productCategory_Parent_Name: '',
        productCategoryID: 0,
        productCategory_Name: '',
        product_Name: '',
        productID: 0,
        unitPrice: 0,
        unitPriceDiscount: 0,
        lineTotal: 0,
        rowguid: null,
        modifiedDate: '',
    } as unknown as ISalesOrderDetailDataModel;
}

export function getSalesOrderDetailAvatar(item: ISalesOrderDetailDataModel): string {
    return "?";
}


export const salesOrderDetailFormValidationWhenCreate = {
    shipToID: {
    },
    customerID: {
    },
    billToID: {
    },
    salesOrderID: {
    },
    orderQty: {
        required: 'OrderQty_is_required',
    },
    productModelID: {
    },
    productCategory_ParentID: {
    },
    productCategoryID: {
    },
    productID: {
    },
    unitPrice: {
        required: 'UnitPrice_is_required',
    },
    unitPriceDiscount: {
        required: 'UnitPriceDiscount_is_required',
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const salesOrderDetailFormValidationWhenEdit = {
    shipToID: {
    },
    customerID: {
    },
    billToID: {
    },
    salesOrderID: {
    },
    orderQty: {
        required: 'OrderQty_is_required',
    },
    productModelID: {
    },
    productCategory_ParentID: {
    },
    productCategoryID: {
    },
    productID: {
    },
    unitPrice: {
        required: 'UnitPrice_is_required',
    },
    unitPriceDiscount: {
        required: 'UnitPriceDiscount_is_required',
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

