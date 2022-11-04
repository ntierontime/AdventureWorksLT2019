import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

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

export function getCustomerAvatar(item: ICustomerDataModel): string {
    return !!item.title && item.title.length > 0 ? item.title.substring(0, 1) : "?";
}


export const customerFormValidationWhenCreate = {
    nameStyle: {
        required: 'NameStyle_is_required',
    },
    title: {
        maxLength: {
            value: 8,
            message: 'The_length_of_Title_should_be_0_to_8',
        },
    },
    firstName: {
        minlength: {
            value: 1,
            message: 'The_length_of_FirstName_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_FirstName_should_be_1_to_50',
        },
    },
    middleName: {
        maxLength: {
            value: 50,
            message: 'The_length_of_MiddleName_should_be_0_to_50',
        },
    },
    lastName: {
        minlength: {
            value: 1,
            message: 'The_length_of_LastName_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_LastName_should_be_1_to_50',
        },
    },
    suffix: {
        maxLength: {
            value: 10,
            message: 'The_length_of_Suffix_should_be_0_to_10',
        },
    },
    companyName: {
        maxLength: {
            value: 128,
            message: 'The_length_of_CompanyName_should_be_0_to_128',
        },
    },
    salesPerson: {
        maxLength: {
            value: 256,
            message: 'The_length_of_SalesPerson_should_be_0_to_256',
        },
    },
    emailAddress: {
        maxLength: {
            value: 50,
            message: 'The_length_of_EmailAddress_should_be_0_to_50',
        },
    },
    phone: {
        maxLength: {
            value: 25,
            message: 'The_length_of_Phone_should_be_0_to_25',
        },
    },
    passwordHash: {
        minlength: {
            value: 1,
            message: 'The_length_of_PasswordHash_should_be_1_to_128',
        },
        maxLength: {
            value: 128,
            message: 'The_length_of_PasswordHash_should_be_1_to_128',
        },
    },
    passwordSalt: {
        minlength: {
            value: 1,
            message: 'The_length_of_PasswordSalt_should_be_1_to_10',
        },
        maxLength: {
            value: 10,
            message: 'The_length_of_PasswordSalt_should_be_1_to_10',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const customerFormValidationWhenEdit = {
    nameStyle: {
        required: 'NameStyle_is_required',
    },
    title: {
        maxLength: {
            value: 8,
            message: 'The_length_of_Title_should_be_0_to_8',
        },
    },
    firstName: {
        minlength: {
            value: 1,
            message: 'The_length_of_FirstName_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_FirstName_should_be_1_to_50',
        },
    },
    middleName: {
        maxLength: {
            value: 50,
            message: 'The_length_of_MiddleName_should_be_0_to_50',
        },
    },
    lastName: {
        minlength: {
            value: 1,
            message: 'The_length_of_LastName_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_LastName_should_be_1_to_50',
        },
    },
    suffix: {
        maxLength: {
            value: 10,
            message: 'The_length_of_Suffix_should_be_0_to_10',
        },
    },
    companyName: {
        maxLength: {
            value: 128,
            message: 'The_length_of_CompanyName_should_be_0_to_128',
        },
    },
    salesPerson: {
        maxLength: {
            value: 256,
            message: 'The_length_of_SalesPerson_should_be_0_to_256',
        },
    },
    emailAddress: {
        maxLength: {
            value: 50,
            message: 'The_length_of_EmailAddress_should_be_0_to_50',
        },
    },
    phone: {
        maxLength: {
            value: 25,
            message: 'The_length_of_Phone_should_be_0_to_25',
        },
    },
    passwordHash: {
        minlength: {
            value: 1,
            message: 'The_length_of_PasswordHash_should_be_1_to_128',
        },
        maxLength: {
            value: 128,
            message: 'The_length_of_PasswordHash_should_be_1_to_128',
        },
    },
    passwordSalt: {
        minlength: {
            value: 1,
            message: 'The_length_of_PasswordSalt_should_be_1_to_10',
        },
        maxLength: {
            value: 10,
            message: 'The_length_of_PasswordSalt_should_be_1_to_10',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

