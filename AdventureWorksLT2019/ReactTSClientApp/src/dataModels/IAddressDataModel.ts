import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


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

// getAddressAvatar will be used when Display the avatar of a Address only
export function getAddressAvatar(item: IAddressDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.addressLine1]);
}

// getAddressTitle will be used when Display the title of a Address only
export function getAddressTitle(item: IAddressDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.addressLine1]);
}


export const addressFormValidation = Yup.object().shape({
    addressLine1: Yup.string()
        .min(1, 'The_length_of_AddressLine1_should_be_1_to_60')
        .max(60, 'The_length_of_AddressLine1_should_be_1_to_60'),
    addressLine2: Yup.string()
        .max(60, 'The_length_of_AddressLine2_should_be_0_to_60'),
    city: Yup.string()
        .min(1, 'The_length_of_City_should_be_1_to_30')
        .max(30, 'The_length_of_City_should_be_1_to_30'),
    stateProvince: Yup.string()
        .min(1, 'The_length_of_StateProvince_should_be_1_to_50')
        .max(50, 'The_length_of_StateProvince_should_be_1_to_50'),
    countryRegion: Yup.string()
        .min(1, 'The_length_of_CountryRegion_should_be_1_to_50')
        .max(50, 'The_length_of_CountryRegion_should_be_1_to_50'),
    postalCode: Yup.string()
        .min(1, 'The_length_of_PostalCode_should_be_1_to_15')
        .max(15, 'The_length_of_PostalCode_should_be_1_to_15'),
});

export const addressAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

