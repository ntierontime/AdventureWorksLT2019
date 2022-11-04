import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IAddressDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    addressID: number;
    addressLine1: string;
    addressLine2: string;
    city: string;
    stateProvince: string;
    countryRegion: string;
    postalCode: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultAddress(): IAddressDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        addressID: 0,
        addressLine1: '',
        addressLine2: '',
        city: '',
        stateProvince: '',
        countryRegion: '',
        postalCode: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IAddressDataModel;
}

export function getAddressAvatar(item: IAddressDataModel): string {
    return !!item.addressLine1 && item.addressLine1.length > 0 ? item.addressLine1.substring(0, 1) : "?";
}


export const addressFormValidationWhenCreate = {
    addressLine1: {
        minlength: {
            value: 1,
            message: 'The_length_of_AddressLine1_should_be_1_to_60',
        },
        maxLength: {
            value: 60,
            message: 'The_length_of_AddressLine1_should_be_1_to_60',
        },
    },
    addressLine2: {
        maxLength: {
            value: 60,
            message: 'The_length_of_AddressLine2_should_be_0_to_60',
        },
    },
    city: {
        minlength: {
            value: 1,
            message: 'The_length_of_City_should_be_1_to_30',
        },
        maxLength: {
            value: 30,
            message: 'The_length_of_City_should_be_1_to_30',
        },
    },
    stateProvince: {
        minlength: {
            value: 1,
            message: 'The_length_of_StateProvince_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_StateProvince_should_be_1_to_50',
        },
    },
    countryRegion: {
        minlength: {
            value: 1,
            message: 'The_length_of_CountryRegion_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_CountryRegion_should_be_1_to_50',
        },
    },
    postalCode: {
        minlength: {
            value: 1,
            message: 'The_length_of_PostalCode_should_be_1_to_15',
        },
        maxLength: {
            value: 15,
            message: 'The_length_of_PostalCode_should_be_1_to_15',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const addressFormValidationWhenEdit = {
    addressLine1: {
        minlength: {
            value: 1,
            message: 'The_length_of_AddressLine1_should_be_1_to_60',
        },
        maxLength: {
            value: 60,
            message: 'The_length_of_AddressLine1_should_be_1_to_60',
        },
    },
    addressLine2: {
        maxLength: {
            value: 60,
            message: 'The_length_of_AddressLine2_should_be_0_to_60',
        },
    },
    city: {
        minlength: {
            value: 1,
            message: 'The_length_of_City_should_be_1_to_30',
        },
        maxLength: {
            value: 30,
            message: 'The_length_of_City_should_be_1_to_30',
        },
    },
    stateProvince: {
        minlength: {
            value: 1,
            message: 'The_length_of_StateProvince_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_StateProvince_should_be_1_to_50',
        },
    },
    countryRegion: {
        minlength: {
            value: 1,
            message: 'The_length_of_CountryRegion_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_CountryRegion_should_be_1_to_50',
        },
    },
    postalCode: {
        minlength: {
            value: 1,
            message: 'The_length_of_PostalCode_should_be_1_to_15',
        },
        maxLength: {
            value: 15,
            message: 'The_length_of_PostalCode_should_be_1_to_15',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

