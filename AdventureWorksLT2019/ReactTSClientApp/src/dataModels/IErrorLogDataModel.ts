import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";


export interface IErrorLogDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    errorLogID: number;
    errorTime: string;
    userName: string;
    errorNumber: number;
    errorSeverity: number;
    errorState: number;
    errorProcedure: string;
    errorLine: number;
    errorMessage: string;
}

export function defaultErrorLog(): IErrorLogDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        errorLogID: 0,
        errorTime: '',
        userName: '',
        errorNumber: 0,
        errorSeverity: 0,
        errorState: 0,
        errorProcedure: '',
        errorLine: 0,
        errorMessage: '',
    } as unknown as IErrorLogDataModel;
}

// getErrorLogAvatar will be used when Display the avatar of a ErrorLog only
export function getErrorLogAvatar(item: IErrorLogDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.userName]);
}

// getErrorLogTitle will be used when Display the title of a ErrorLog only
export function getErrorLogTitle(item: IErrorLogDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.userName]);
}


export const errorLogFormValidation = Yup.object().shape({
    userName: Yup.string()
        .min(1, 'The_length_of_UserName_should_be_1_to_128')
        .max(128, 'The_length_of_UserName_should_be_1_to_128'),
    errorNumber: Yup.number()
        .required('ErrorNumber_is_required'),
    errorSeverity: Yup.number(),
    errorState: Yup.number(),
    errorProcedure: Yup.string()
        .max(126, 'The_length_of_ErrorProcedure_should_be_0_to_126'),
    errorLine: Yup.number(),
    errorMessage: Yup.string()
        .min(1, 'The_length_of_ErrorMessage_should_be_1_to_4000')
        .max(4000, 'The_length_of_ErrorMessage_should_be_1_to_4000'),
});

export const errorLogAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

