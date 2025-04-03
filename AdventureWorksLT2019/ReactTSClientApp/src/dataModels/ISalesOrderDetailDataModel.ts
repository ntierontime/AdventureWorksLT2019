import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface ISalesOrderDetailDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    salesOrderHeader_Name: string;
    salesOrderID: number | null | '';
    salesOrderDetailID: number;
    orderQty: number;
    product_Name: string;
    productID: number | null | '';
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
        salesOrderID: 0,
        salesOrderDetailID: 0,
        orderQty: 0,
        product_Name: '',
        productID: 0,
        unitPrice: 0,
        unitPriceDiscount: 0,
        lineTotal: 0,
        rowguid: null,
        modifiedDate: '',
    } as unknown as ISalesOrderDetailDataModel;
}

// getSalesOrderDetailAvatar will be used when Display the avatar of a SalesOrderDetail only
export function getSalesOrderDetailAvatar(item: ISalesOrderDetailDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.salesOrderDetailID.toString()]);
}

// getSalesOrderDetailTitle will be used when Display the title of a SalesOrderDetail only
export function getSalesOrderDetailTitle(item: ISalesOrderDetailDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.salesOrderDetailID.toString()]);
}



export const salesOrderDetailAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

