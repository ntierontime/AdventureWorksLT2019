import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";
import { GenderOptions, CultureOptions, EntityStatusCodeOptions, VisibilityOptions } from 'src/dataModels/Enums';
import { IFileUploadModel } from '../shared/dataModels/IFileUploadModel';


export interface IPersonAvatarDataModel {
    nickName: string;
    title: string;
    firstName: string;
    lastName: string;
    avatarUrl: string;
    coverUrl: string;
    uniqueName: string;
}


export interface IPersonDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    visibilityID: VisibilityOptions | null | '';
    visibility_Name: string;
    entityStatusCodeID: EntityStatusCodeOptions | null | '';
    entityStatusCode_Name: string;
    entity_Name: string;
    avatarUrl: string;
    avatarUrlLocalFile?: IFileUploadModel;
    coverUrl: string;
    coverUrlLocalFile?: IFileUploadModel;
    caption: string;
    shortName: string;
    uniqueName: string;
    customUniqueName: string;
    shortGuid: string;
    summary: string;
    descriptionJSON: string;
    createdDate: string;
    modifiedDate: string;
    entityID: number | null | '';
    aspNetUserId: string;
    birthDate: string;
    gender_Name: string;
    genderID: GenderOptions | null | '';
    culture_Name: string;
    cultureID: string;
    nameStyle: boolean;
    nickName: string;
    title: string;
    firstName: string;
    middleName: string;
    lastName: string;
    suffix: string;
    rowguid: any;
}

export function defaultPerson(): IPersonDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        visibilityID: VisibilityOptions.Private,
        visibility_Name: '',
        entityStatusCodeID: EntityStatusCodeOptions.Active,
        entityStatusCode_Name: '',
        entity_Name: '',
        avatarUrl: '',
        coverUrl: '',
        caption: '',
        shortName: '',
        uniqueName: '',
        customUniqueName: '',
        shortGuid: '',
        summary: '',
        descriptionJSON: '',
        createdDate: '',
        modifiedDate: '',
        entityID: 0,
        aspNetUserId: '',
        birthDate: '',
        gender_Name: '',
        genderID: GenderOptions.Male,
        culture_Name: '',
        cultureID: '',
        nameStyle: false,
        nickName: '',
        title: '',
        firstName: '',
        middleName: '',
        lastName: '',
        suffix: '',
        rowguid: null,
    } as unknown as IPersonDataModel;
}

// getPersonAvatar will be used when Display the avatar of a Person only
export function getPersonAvatar(item: IPersonDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.firstName, item.lastName]);
}

// getPersonTitle will be used when Display the title of a Person only
export function getPersonTitle(item: IPersonDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.title, item.firstName, item.lastName]);
}


export const personFormValidation = Yup.object().shape({
    cultureID: Yup.string()
        .min(1, 'The_length_of_CultureID_should_be_1_to_6')
        .max(6, 'The_length_of_CultureID_should_be_1_to_6'),
    nickName: Yup.string()
        .max(50, 'The_length_of_NickName_should_be_0_to_50'),
    title: Yup.string()
        .max(8, 'The_length_of_Title_should_be_0_to_8'),
    firstName: Yup.string()
        .min(1, 'The_length_of_FirstName_should_be_1_to_50')
        .max(50, 'The_length_of_FirstName_should_be_1_to_50'),
    middleName: Yup.string()
        .max(50, 'The_length_of_MiddleName_should_be_0_to_50'),
    lastName: Yup.string()
        .min(1, 'The_length_of_LastName_should_be_1_to_50')
        .max(50, 'The_length_of_LastName_should_be_1_to_50'),
    suffix: Yup.string()
        .max(10, 'The_length_of_Suffix_should_be_0_to_10'),
});

export const personAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

