import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

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
    parentID: number | '';
    parent_Name: string;
    productCategory_Name: string;
    productCategoryID: number | '';
    productModel_Name: string;
    productModelID: number | '';
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
        parentID: '',
        parent_Name: '',
        productCategory_Name: '',
        productCategoryID: '',
        productModel_Name: '',
        productModelID: '',
        sellStartDate: new Date(),
        sellEndDate: new Date(),
        discontinuedDate: new Date(),
        thumbNailPhoto: null,
        thumbnailPhotoFileName: '',
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as IProductDataModel;
}

export function getProductAvatar(item: IProductDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productFormValidationWhenCreate = Yup.object().shape({
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
    parentID: Yup.number(),
    productCategoryID: Yup.number(),
    productModelID: Yup.number(),
    sellStartDate: Yup.string()
        .required('SellStartDate_is_required'),
    sellEndDate: Yup.string(),
    discontinuedDate: Yup.string(),
    thumbnailPhotoFileName: Yup.string()
        .max(50, 'The_length_of_ThumbnailPhotoFileName_should_be_0_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productFormValidationWhenEdit = Yup.object().shape({
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
    parentID: Yup.number(),
    productCategoryID: Yup.number(),
    productModelID: Yup.number(),
    sellStartDate: Yup.string()
        .required('SellStartDate_is_required'),
    sellEndDate: Yup.string(),
    discontinuedDate: Yup.string(),
    thumbnailPhotoFileName: Yup.string()
        .max(50, 'The_length_of_ThumbnailPhotoFileName_should_be_0_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

