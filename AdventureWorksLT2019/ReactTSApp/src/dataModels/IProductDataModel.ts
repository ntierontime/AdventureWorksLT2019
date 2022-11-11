import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

export interface IProductDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productID: number;
    name: string;
    productNumber: string;
    color: string;
    standardCost: number;
    listPrice: number;
    size: string;
    weight: number;
    parentID: number | null;
    parent_Name: string;
    productCategory_Name: string;
    productCategoryID: number | null;
    productModel_Name: string;
    productModelID: number | null;
    sellStartDate: string;
    sellEndDate: string;
    discontinuedDate: string;
    thumbNailPhoto: any;
    thumbnailPhotoFileName: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProduct(): IProductDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productID: 0,
        name: '',
        productNumber: '',
        color: '',
        standardCost: 0,
        listPrice: 0,
        size: '',
        weight: 0,
        parentID: null,
        parent_Name: '',
        productCategory_Name: '',
        productCategoryID: null,
        productModel_Name: '',
        productModelID: null,
        sellStartDate: '',
        sellEndDate: '',
        discontinuedDate: '',
        thumbNailPhoto: null,
        thumbnailPhotoFileName: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductDataModel;
}

export function getProductAvatar(item: IProductDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productFormValidationWhenCreate = {
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
    productNumber: {
        minlength: {
            value: 1,
            message: 'The_length_of_ProductNumber_should_be_1_to_25',
        },
        maxLength: {
            value: 25,
            message: 'The_length_of_ProductNumber_should_be_1_to_25',
        },
    },
    color: {
        maxLength: {
            value: 15,
            message: 'The_length_of_Color_should_be_0_to_15',
        },
    },
    standardCost: {
        required: 'StandardCost_is_required',
    },
    listPrice: {
        required: 'ListPrice_is_required',
    },
    size: {
        maxLength: {
            value: 5,
            message: 'The_length_of_Size_should_be_0_to_5',
        },
    },
    weight: {
    },
    parentID: {
    },
    productCategoryID: {
    },
    productModelID: {
    },
    sellStartDate: {
        required: 'SellStartDate_is_required',
    },
    sellEndDate: {
    },
    discontinuedDate: {
    },
    thumbNailPhoto: {
    },
    thumbnailPhotoFileName: {
        maxLength: {
            value: 50,
            message: 'The_length_of_ThumbnailPhotoFileName_should_be_0_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const productFormValidationWhenEdit = {
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
    productNumber: {
        minlength: {
            value: 1,
            message: 'The_length_of_ProductNumber_should_be_1_to_25',
        },
        maxLength: {
            value: 25,
            message: 'The_length_of_ProductNumber_should_be_1_to_25',
        },
    },
    color: {
        maxLength: {
            value: 15,
            message: 'The_length_of_Color_should_be_0_to_15',
        },
    },
    standardCost: {
        required: 'StandardCost_is_required',
    },
    listPrice: {
        required: 'ListPrice_is_required',
    },
    size: {
        maxLength: {
            value: 5,
            message: 'The_length_of_Size_should_be_0_to_5',
        },
    },
    weight: {
    },
    parentID: {
    },
    productCategoryID: {
    },
    productModelID: {
    },
    sellStartDate: {
        required: 'SellStartDate_is_required',
    },
    sellEndDate: {
    },
    discontinuedDate: {
    },
    thumbNailPhoto: {
    },
    thumbnailPhotoFileName: {
        maxLength: {
            value: 50,
            message: 'The_length_of_ThumbnailPhotoFileName_should_be_0_to_50',
        },
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

