import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface IProductModelProductDescriptionDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    productModel_Name: string;
    productModelID: number | '';
    productDescription_Name: string;
    productDescriptionID: number | '';
    culture: string;
    rowguid: any;
    modifiedDate: string;
}

export function defaultProductModelProductDescription(): IProductModelProductDescriptionDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        productModel_Name: '',
        productModelID: '',
        productDescription_Name: '',
        productDescriptionID: '',
        culture: '',
        rowguid: null,
        modifiedDate: new Date(),
    } as unknown as IProductModelProductDescriptionDataModel;
}

export function getProductModelProductDescriptionAvatar(item: IProductModelProductDescriptionDataModel): string {
    return !!item.culture && item.culture.length > 0 ? item.culture.substring(0, 1) : "?";
}


export const productModelProductDescriptionFormValidationWhenCreate = Yup.object().shape({
    productModelID: Yup.number(),
    productDescriptionID: Yup.number(),
    culture: Yup.string()
        .min(1, 'The_length_of_Culture_should_be_1_to_6')
        .max(6, 'The_length_of_Culture_should_be_1_to_6'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productModelProductDescriptionFormValidationWhenEdit = Yup.object().shape({
    productModelID: Yup.number(),
    productDescriptionID: Yup.number(),
    culture: Yup.string()
        .min(1, 'The_length_of_Culture_should_be_1_to_6')
        .max(6, 'The_length_of_Culture_should_be_1_to_6'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const productModelProductDescriptionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

