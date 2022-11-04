import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IProductModelProductDescriptionDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productModelID: number;
    productDescriptionID: number;
    culture: string;
    rowguid: any;
    modifiedDate: string;
    productDescription_Name: string;
    productModel_Name: string;
}

export function defaultProductModelProductDescription(): IProductModelProductDescriptionDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productModelID: 0,
        productDescriptionID: 0,
        culture: '',
        rowguid: null,
        modifiedDate: '',
        productDescription_Name: '',
        productModel_Name: '',
    } as unknown as IProductModelProductDescriptionDataModel;
}

export function getProductModelProductDescriptionAvatar(item: IProductModelProductDescriptionDataModel): string {
    return !!item.culture && item.culture.length > 0 ? item.culture.substring(0, 1) : "?";
}


export const productModelProductDescriptionFormValidationWhenCreate = {
    productModelID: {
    },
    productDescriptionID: {
    },
    culture: {
        minlength: {
            value: 1,
            message: 'The_length_of_Culture_should_be_1_to_6',
        },
        maxLength: {
            value: 6,
            message: 'The_length_of_Culture_should_be_1_to_6',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const productModelProductDescriptionFormValidationWhenEdit = {
    productModelID: {
    },
    productDescriptionID: {
    },
    culture: {
        minlength: {
            value: 1,
            message: 'The_length_of_Culture_should_be_1_to_6',
        },
        maxLength: {
            value: 6,
            message: 'The_length_of_Culture_should_be_1_to_6',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

