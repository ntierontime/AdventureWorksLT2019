import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface IProductModelDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productModelID: number;
    name: string;
    catalogDescription: any;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProductModel(): IProductModelDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productModelID: 0,
        name: '',
        catalogDescription: null,
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductModelDataModel;
}

// getProductModelAvatar will be used when Display the avatar of a ProductModel only
export function getProductModelAvatar(item: IProductModelDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.name]);
}

// getProductModelTitle will be used when Display the title of a ProductModel only
export function getProductModelTitle(item: IProductModelDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.name]);
}


export const productModelFormValidation = Yup.object().shape({
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
});

export const productModelAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

