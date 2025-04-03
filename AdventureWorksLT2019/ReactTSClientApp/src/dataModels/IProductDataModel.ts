import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


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
    productCategory_Name: string;
    productCategoryID: number | null | '';
    productModel_Name: string;
    productModelID: number | null | '';
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
        productCategory_Name: '',
        productCategoryID: 0,
        productModel_Name: '',
        productModelID: 0,
        sellStartDate: '',
        sellEndDate: '',
        discontinuedDate: '',
        thumbNailPhoto: null,
        thumbnailPhotoFileName: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductDataModel;
}

// getProductAvatar will be used when Display the avatar of a Product only
export function getProductAvatar(item: IProductDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.name]);
}

// getProductTitle will be used when Display the title of a Product only
export function getProductTitle(item: IProductDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.name]);
}


export const productFormValidation = Yup.object().shape({
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
    productNumber: Yup.string()
        .min(1, 'The_length_of_ProductNumber_should_be_1_to_25')
        .max(25, 'The_length_of_ProductNumber_should_be_1_to_25'),
    color: Yup.string()
        .max(15, 'The_length_of_Color_should_be_0_to_15'),
    standardCost: Yup.number()
        .required('StandardCost_is_required'),
    listPrice: Yup.number()
        .required('ListPrice_is_required'),
    size: Yup.string()
        .max(5, 'The_length_of_Size_should_be_0_to_5'),
    weight: Yup.number(),
    productCategoryID: Yup.number(),
    productModelID: Yup.number(),
    thumbnailPhotoFileName: Yup.string()
        .max(50, 'The_length_of_ThumbnailPhotoFileName_should_be_0_to_50'),
});

export const productAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

