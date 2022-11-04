import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface ISalesOrderDetailDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    salesOrderID: number;
    salesOrderDetailID: number;
    orderQty: number;
    productID: number;
    unitPrice: number;
    unitPriceDiscount: number;
    lineTotal: number;
    rowguid: any;
    modifiedDate: string;
    product_Name: string;
    productCategoryID: number;
    productCategory_Name: string;
    productCategory_ParentID: number;
    productCategory_Parent_Name: string;
    productModelID: number;
    productModel_Name: string;
    salesOrderHeader_Name: string;
    billToID: number;
    billTo_Name: string;
    customerID: number;
    customer_Name: string;
    shipToID: number;
    shipTo_Name: string;
}

export function defaultSalesOrderDetail(): ISalesOrderDetailDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        salesOrderID: 0,
        salesOrderDetailID: 0,
        orderQty: 0,
        productID: 0,
        unitPrice: 0,
        unitPriceDiscount: 0,
        lineTotal: 0,
        rowguid: null,
        modifiedDate: '',
        product_Name: '',
        productCategoryID: 0,
        productCategory_Name: '',
        productCategory_ParentID: 0,
        productCategory_Parent_Name: '',
        productModelID: 0,
        productModel_Name: '',
        salesOrderHeader_Name: '',
        billToID: 0,
        billTo_Name: '',
        customerID: 0,
        customer_Name: '',
        shipToID: 0,
        shipTo_Name: '',
    } as unknown as ISalesOrderDetailDataModel;
}

export function getSalesOrderDetailAvatar(item: ISalesOrderDetailDataModel): string {
    return "?";
}


export const salesOrderDetailFormValidationWhenCreate = {
    salesOrderID: {
    },
    orderQty: {
        required: 'OrderQty_is_required',
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
    productCategoryID: {
    },
    productCategory_ParentID: {
    },
    productModelID: {
    },
    billToID: {
    },
    customerID: {
    },
    shipToID: {
    },
};

export const salesOrderDetailFormValidationWhenEdit = {
    salesOrderID: {
    },
    orderQty: {
        required: 'OrderQty_is_required',
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
    productCategoryID: {
    },
    productCategory_ParentID: {
    },
    productModelID: {
    },
    billToID: {
    },
    customerID: {
    },
    shipToID: {
    },
};

