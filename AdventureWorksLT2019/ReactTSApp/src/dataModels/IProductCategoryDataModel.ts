import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface IProductCategoryDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productCategoryID: number;
    parent_Name: string;
    parentProductCategoryID: number | '';
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
        parentProductCategoryID: '',
        name: '',
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as IProductCategoryDataModel;
}

export function getProductCategoryAvatar(item: IProductCategoryDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productCategoryFormValidationWhenCreate = Yup.object().shape({
    parentProductCategoryID: Yup.number(),
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productCategoryFormValidationWhenEdit = Yup.object().shape({
    parentProductCategoryID: Yup.number(),
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productCategoryAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

