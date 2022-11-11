import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IProductCategoryDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productCategoryID: number;
    parent_Name: string;
    parentProductCategoryID: number | null;
    name: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProductCategory(): IProductCategoryDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productCategoryID: 0,
        parent_Name: '',
        parentProductCategoryID: null,
        name: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductCategoryDataModel;
}

export function getProductCategoryAvatar(item: IProductCategoryDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productCategoryFormValidationWhenCreate = {
    parentProductCategoryID: {
    },
    name: {
        minlength: {
            value: 1,
            message: 'The_length_of_Name_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_Name_should_be_1_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const productCategoryFormValidationWhenEdit = {
    parentProductCategoryID: {
    },
    name: {
        minlength: {
            value: 1,
            message: 'The_length_of_Name_should_be_1_to_50',
        },
        maxLength: {
            value: 50,
            message: 'The_length_of_Name_should_be_1_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

