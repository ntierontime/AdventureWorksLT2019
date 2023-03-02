import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface ISalesOrderDetailDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    salesOrderHeader_Name: string;
    salesOrderID: number | '';
    salesOrderDetailID: number;
    orderQty: number;
    product_Name: string;
    productID: number | '';
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
        salesOrderID: '',
        salesOrderDetailID: 0,
        orderQty: 0,
        product_Name: '',
        productID: '',
        unitPrice: 0,
        unitPriceDiscount: 0,
        lineTotal: 0,
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as ISalesOrderDetailDataModel;
}

export function getSalesOrderDetailAvatar(item: ISalesOrderDetailDataModel): string {
    return "?";
}


export const salesOrderDetailFormValidationWhenCreate = Yup.object().shape({
    salesOrderID: Yup.number(),
    orderQty: Yup.number()
        .required('OrderQty_is_required'),
    productID: Yup.number(),
    unitPrice: Yup.number()
        .required('UnitPrice_is_required'),
    unitPriceDiscount: Yup.number()
        .required('UnitPriceDiscount_is_required'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const salesOrderDetailFormValidationWhenEdit = Yup.object().shape({
    salesOrderID: Yup.number(),
    orderQty: Yup.number()
        .required('OrderQty_is_required'),
    productID: Yup.number(),
    unitPrice: Yup.number()
        .required('UnitPrice_is_required'),
    unitPriceDiscount: Yup.number()
        .required('UnitPriceDiscount_is_required'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const salesOrderDetailAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

