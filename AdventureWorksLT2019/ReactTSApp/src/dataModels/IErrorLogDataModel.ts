import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";

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

export function getErrorLogAvatar(item: IErrorLogDataModel): string {
    return !!item.userName && item.userName.length > 0 ? item.userName.substring(0, 1) : "?";
}


export const errorLogFormValidationWhenCreate = {
    errorTime: {
        required: 'ErrorTime_is_required',
    },
    userName: {
        minlength: {
            value: 1,
            message: 'The_length_of_UserName_should_be_1_to_128',
        },
        maxLength: {
            value: 128,
            message: 'The_length_of_UserName_should_be_1_to_128',
        },
    },
    errorNumber: {
        required: 'ErrorNumber_is_required',
    },
    errorSeverity: {
    },
    errorState: {
    },
    errorProcedure: {
        maxLength: {
            value: 126,
            message: 'The_length_of_ErrorProcedure_should_be_0_to_126',
        },
    },
    errorLine: {
    },
    errorMessage: {
        minlength: {
            value: 1,
            message: 'The_length_of_ErrorMessage_should_be_1_to_4000',
        },
        maxLength: {
            value: 4000,
            message: 'The_length_of_ErrorMessage_should_be_1_to_4000',
        },
    },
};

export const errorLogFormValidationWhenEdit = {
    errorTime: {
        required: 'ErrorTime_is_required',
    },
    userName: {
        minlength: {
            value: 1,
            message: 'The_length_of_UserName_should_be_1_to_128',
        },
        maxLength: {
            value: 128,
            message: 'The_length_of_UserName_should_be_1_to_128',
        },
    },
    errorNumber: {
        required: 'ErrorNumber_is_required',
    },
    errorSeverity: {
    },
    errorState: {
    },
    errorProcedure: {
        maxLength: {
            value: 126,
            message: 'The_length_of_ErrorProcedure_should_be_0_to_126',
        },
    },
    errorLine: {
    },
    errorMessage: {
        minlength: {
            value: 1,
            message: 'The_length_of_ErrorMessage_should_be_1_to_4000',
        },
        maxLength: {
            value: 4000,
            message: 'The_length_of_ErrorMessage_should_be_1_to_4000',
        },
    },
};

