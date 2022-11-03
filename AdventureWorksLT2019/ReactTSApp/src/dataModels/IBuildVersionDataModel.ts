import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

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
        versionDate: '',
        modifiedDate: '',
    } as unknown as IBuildVersionDataModel;
}

export function getBuildVersionAvatar(item: IBuildVersionDataModel): string {
    return !!item.database_Version && item.database_Version.length > 0 ? item.database_Version.substring(0, 1) : "?";
}


export const buildVersionFormValidationWhenCreate = {
    database_Version: {
        minlength: {
            value: 1,
            message: 'The_length_of_Database_Version_should_be_1_to_25',
        },
        maxLength: {
            value: 25,
            message: 'The_length_of_Database_Version_should_be_1_to_25',
        },
    },
    versionDate: {
        required: 'VersionDate_is_required',
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

export const buildVersionFormValidationWhenEdit = {
    database_Version: {
        minlength: {
            value: 1,
            message: 'The_length_of_Database_Version_should_be_1_to_25',
        },
        maxLength: {
            value: 25,
            message: 'The_length_of_Database_Version_should_be_1_to_25',
        },
    },
    versionDate: {
        required: 'VersionDate_is_required',
    },
    modifiedDate: {
        required: 'ModifiedDate_is_required',
    },
};

