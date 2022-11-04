import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IProductDescriptionDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productDescriptionID: number;
    description: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProductDescription(): IProductDescriptionDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productDescriptionID: 0,
        description: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductDescriptionDataModel;
}

export function getProductDescriptionAvatar(item: IProductDescriptionDataModel): string {
    return !!item.description && item.description.length > 0 ? item.description.substring(0, 1) : "?";
}


export const productDescriptionFormValidationWhenCreate = {
    description: {
        minlength: {
            value: 1,
            message: 'The_length_of_Description_should_be_1_to_400',
        },
        maxLength: {
            value: 400,
            message: 'The_length_of_Description_should_be_1_to_400',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const productDescriptionFormValidationWhenEdit = {
    description: {
        minlength: {
            value: 1,
            message: 'The_length_of_Description_should_be_1_to_400',
        },
        maxLength: {
            value: 400,
            message: 'The_length_of_Description_should_be_1_to_400',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

