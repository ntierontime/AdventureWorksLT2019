import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface ISalesOrderDetailDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    salesOrderHeader_Name: string;
    salesOrderID: number | null;
    salesOrderDetailID: number;
    orderQty: number;
    product_Name: string;
    productID: number | null;
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
        salesOrderHeader_Name: '',
        salesOrderID: null,
        salesOrderDetailID: 0,
        orderQty: 0,
        product_Name: '',
        productID: null,
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
};

