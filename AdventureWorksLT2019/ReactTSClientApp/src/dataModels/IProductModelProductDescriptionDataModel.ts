import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface IProductModelProductDescriptionDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productModel_Name: string;
    productModelID: number | null | '';
    productDescription_Name: string;
    productDescriptionID: number | null | '';
    culture: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProductModelProductDescription(): IProductModelProductDescriptionDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productModel_Name: '',
        productModelID: 0,
        productDescription_Name: '',
        productDescriptionID: 0,
        culture: '',
        rowguid: null,
        modifiedDate: '',
    } as unknown as IProductModelProductDescriptionDataModel;
}

// getProductModelProductDescriptionAvatar will be used when Display the avatar of a ProductModelProductDescription only
export function getProductModelProductDescriptionAvatar(item: IProductModelProductDescriptionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.culture]);
}

// getProductModelProductDescriptionTitle will be used when Display the title of a ProductModelProductDescription only
export function getProductModelProductDescriptionTitle(item: IProductModelProductDescriptionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.culture]);
}



export const productModelProductDescriptionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

