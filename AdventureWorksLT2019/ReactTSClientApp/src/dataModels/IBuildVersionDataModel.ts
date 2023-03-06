import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';

export interface IBuildVersionDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    systemInformationID: number;
    database_Version: string;
    versionDate: string;
    modifiedDate: string;
}

export function defaultBuildVersion(): IBuildVersionDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        systemInformationID: 0,
        database_Version: '',
        versionDate: new Date(),
        modifiedDate: new Date(),
    } as unknown as IBuildVersionDataModel;
}

export function getBuildVersionAvatar(item: IBuildVersionDataModel): string {
    return !!item.database_Version && item.database_Version.length > 0 ? item.database_Version.substring(0, 1) : "?";
}


export const buildVersionFormValidationWhenCreate = Yup.object().shape({
    database_Version: Yup.string()
        .min(1, 'The_length_of_Database_Version_should_be_1_to_25')
        .max(25, 'The_length_of_Database_Version_should_be_1_to_25'),
    versionDate: Yup.string()
        .required('VersionDate_is_required'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const buildVersionFormValidationWhenEdit = Yup.object().shape({
    database_Version: Yup.string()
        .min(1, 'The_length_of_Database_Version_should_be_1_to_25')
        .max(25, 'The_length_of_Database_Version_should_be_1_to_25'),
    versionDate: Yup.string()
        .required('VersionDate_is_required'),
    modifiedDate: Yup.string()
        .required('ModifiedDate_is_required'),
});

export const buildVersionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

