import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

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
        modifiedDate: new Date(),
    } as unknown as IProductModelDataModel;
}

export function getProductModelAvatar(item: IProductModelDataModel): string {
    return !!item.name && item.name.length > 0 ? item.name.substring(0, 1) : "?";
}


export const productModelFormValidationWhenCreate = Yup.object().shape({
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productModelFormValidationWhenEdit = Yup.object().shape({
    name: Yup.string()
        .min(1, 'The_length_of_Name_should_be_1_to_50')
        .max(50, 'The_length_of_Name_should_be_1_to_50'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productModelAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

