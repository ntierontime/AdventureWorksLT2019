import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface ICustomerDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    customerID: number;
    nameStyle: boolean;
    title: string;
    firstName: string;
    middleName: string;
    lastName: string;
    suffix: string;
    companyName: string;
    salesPerson: string;
    emailAddress: string;
    phone: string;
    passwordHash: string;
    passwordSalt: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultCustomer(): ICustomerDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        customerID: 0,
        nameStyle: false,
        title: '',
        firstName: '',
        middleName: '',
        lastName: '',
        suffix: '',
        companyName: '',
        salesPerson: '',
        emailAddress: '',
        phone: '',
        passwordHash: '',
        passwordSalt: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as ICustomerDataModel;
}

// getCustomerAvatar will be used when Display the avatar of a Customer only
export function getCustomerAvatar(item: ICustomerDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.title]);
}

// getCustomerTitle will be used when Display the title of a Customer only
export function getCustomerTitle(item: ICustomerDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.title]);
}


export const customerFormValidation = Yup.object().shape({
    title: Yup.string()
        .max(8, 'The_length_of_Title_should_be_0_to_8'),
    firstName: Yup.string()
        .min(1, 'The_length_of_FirstName_should_be_1_to_50')
        .max(50, 'The_length_of_FirstName_should_be_1_to_50'),
    middleName: Yup.string()
        .max(50, 'The_length_of_MiddleName_should_be_0_to_50'),
    lastName: Yup.string()
        .min(1, 'The_length_of_LastName_should_be_1_to_50')
        .max(50, 'The_length_of_LastName_should_be_1_to_50'),
    suffix: Yup.string()
        .max(10, 'The_length_of_Suffix_should_be_0_to_10'),
    companyName: Yup.string()
        .max(128, 'The_length_of_CompanyName_should_be_0_to_128'),
    salesPerson: Yup.string()
        .max(256, 'The_length_of_SalesPerson_should_be_0_to_256'),
    emailAddress: Yup.string()
        .max(50, 'The_length_of_EmailAddress_should_be_0_to_50'),
    phone: Yup.string()
        .max(25, 'The_length_of_Phone_should_be_0_to_25'),
    passwordHash: Yup.string()
        .min(1, 'The_length_of_PasswordHash_should_be_1_to_128')
        .max(128, 'The_length_of_PasswordHash_should_be_1_to_128'),
    passwordSalt: Yup.string()
        .min(1, 'The_length_of_PasswordSalt_should_be_1_to_10')
        .max(10, 'The_length_of_PasswordSalt_should_be_1_to_10'),
});

export const customerAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

