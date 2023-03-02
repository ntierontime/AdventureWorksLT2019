import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

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
        modifiedDate: new Date(),
    } as unknown as IProductDescriptionDataModel;
}

export function getProductDescriptionAvatar(item: IProductDescriptionDataModel): string {
    return !!item.description && item.description.length > 0 ? item.description.substring(0, 1) : "?";
}


export const productDescriptionFormValidationWhenCreate = Yup.object().shape({
    description: Yup.string()
        .min(1, 'The_length_of_Description_should_be_1_to_400')
        .max(400, 'The_length_of_Description_should_be_1_to_400'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productDescriptionFormValidationWhenEdit = Yup.object().shape({
    description: Yup.string()
        .min(1, 'The_length_of_Description_should_be_1_to_400')
        .max(400, 'The_length_of_Description_should_be_1_to_400'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productDescriptionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

