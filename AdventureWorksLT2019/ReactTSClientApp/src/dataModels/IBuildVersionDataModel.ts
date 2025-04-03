import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


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

// getBuildVersionAvatar will be used when Display the avatar of a BuildVersion only
export function getBuildVersionAvatar(item: IBuildVersionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.database_Version]);
}

// getBuildVersionTitle will be used when Display the title of a BuildVersion only
export function getBuildVersionTitle(item: IBuildVersionDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.database_Version]);
}



export const buildVersionAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

