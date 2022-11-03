import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface ICustomerAddressDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    customerID: number;
    addressID: number;
    addressType: string;
    _rowguid: any;
    modifiedDate: string;
    address_Name: string;
    customer_Name: string;
}

export function defaultCustomerAddress(): ICustomerAddressDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        customerID: 0,
        addressID: 0,
        addressType: '',
        _rowguid: null,
        modifiedDate: '',
        address_Name: '',
        customer_Name: '',
    } as unknown as ICustomerAddressDataModel;
}

export function getCustomerAddressAvatar(item: ICustomerAddressDataModel): string {
    return !!item.addressType && item.addressType.length > 0 ? item.addressType.substring(0, 1) : "?";
}


export const customerAddressFormValidationWhenCreate = {
    customerID: {
    },
    addressID: {
    },
    addressType: {
        minlength: {
            value: 1,
            message: 'The_length_of_AddressType_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_AddressType_should_be_1_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const customerAddressFormValidationWhenEdit = {
    customerID: {
    },
    addressID: {
    },
    addressType: {
        minlength: {
            value: 1,
            message: 'The_length_of_AddressType_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_AddressType_should_be_1_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

