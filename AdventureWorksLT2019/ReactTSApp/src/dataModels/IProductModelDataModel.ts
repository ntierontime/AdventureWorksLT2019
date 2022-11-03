import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IProductModelDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productModelID: number;
    name: string;
    catalogDescription: any;
    _rowguid: any;
    modifiedDate: string;
}

export function defaultProductModel(): IProductModelDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productModelID: 0,
        name: '',
        catalogDescription: null,
        _rowguid: null,
        modifiedDate: '',
    } as unknown as IProductModelDataModel;
}

export function getProductModelAvatar(item: IProductModelDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productModelFormValidationWhenCreate = {
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
    catalogDescription: {
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const productModelFormValidationWhenEdit = {
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
    catalogDescription: {
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

