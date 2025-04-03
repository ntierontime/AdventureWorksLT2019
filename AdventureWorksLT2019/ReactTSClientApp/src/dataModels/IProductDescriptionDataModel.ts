import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


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

// getProductDescriptionAvatar will be used when Display the avatar of a ProductDescription only
export function getProductDescriptionAvatar(item: IProductDescriptionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.description]);
}

// getProductDescriptionTitle will be used when Display the title of a ProductDescription only
export function getProductDescriptionTitle(item: IProductDescriptionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.description]);
}


export const productDescriptionFormValidation = Yup.object().shape({
    description: Yup.string()
        .min(1, 'The_length_of_Description_should_be_1_to_400')
        .max(400, 'The_length_of_Description_should_be_1_to_400'),
});

export const productDescriptionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

