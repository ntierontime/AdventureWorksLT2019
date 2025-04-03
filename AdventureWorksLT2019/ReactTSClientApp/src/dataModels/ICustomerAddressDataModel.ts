import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface ICustomerAddressDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    customer_Name: string;
    customerID: number | null | '';
    address_Name: string;
    addressID: number | null | '';
    addressType: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultCustomerAddress(): ICustomerAddressDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        customer_Name: '',
        customerID: 0,
        address_Name: '',
        addressID: 0,
        addressType: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as ICustomerAddressDataModel;
}

// getCustomerAddressAvatar will be used when Display the avatar of a CustomerAddress only
export function getCustomerAddressAvatar(item: ICustomerAddressDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.addressType]);
}

// getCustomerAddressTitle will be used when Display the title of a CustomerAddress only
export function getCustomerAddressTitle(item: ICustomerAddressDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.addressType]);
}



export const customerAddressAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

