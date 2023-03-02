import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface ICustomerAddressDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    customer_Name: string;
    customerID: number | '';
    address_Name: string;
    addressID: number | '';
    addressType: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultCustomerAddress(): ICustomerAddressDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        customer_Name: '',
        customerID: '',
        address_Name: '',
        addressID: '',
        addressType: '',
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as ICustomerAddressDataModel;
}

export function getCustomerAddressAvatar(item: ICustomerAddressDataModel): string {
    return !!item.addressType && item.addressType.length > 0 ? item.addressType.substring(0, 1) : "?";
}


export const customerAddressFormValidationWhenCreate = Yup.object().shape({
    customerID: Yup.number(),
    addressID: Yup.number(),
    addressType: Yup.string()
        .min(1, 'The_length_of_AddressType_should_be_1_to_50')
        .max(50, 'The_length_of_AddressType_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const customerAddressFormValidationWhenEdit = Yup.object().shape({
    customerID: Yup.number(),
    addressID: Yup.number(),
    addressType: Yup.string()
        .min(1, 'The_length_of_AddressType_should_be_1_to_50')
        .max(50, 'The_length_of_AddressType_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const customerAddressAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

