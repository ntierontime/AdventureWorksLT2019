import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface IProductCategoryDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productCategoryID: number;
    parent_Name: string;
    parentProductCategoryID: number | null | '';
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
        parentProductCategoryID: 0,
        name: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductCategoryDataModel;
}

// getProductCategoryAvatar will be used when Display the avatar of a ProductCategory only
export function getProductCategoryAvatar(item: IProductCategoryDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.name]);
}

// getProductCategoryTitle will be used when Display the title of a ProductCategory only
export function getProductCategoryTitle(item: IProductCategoryDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.name]);
}


export const productCategoryFormValidation = Yup.object().shape({
    parentProductCategoryID: Yup.number(),
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
});

export const productCategoryAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

