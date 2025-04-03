import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { AutocompleteSetting } from "src/shared/views/AutocompleteSetting";
import * as Yup from 'yup';
import dayjs from "dayjs";

import { getAvatar } from "src/shared/avatarUtility";
import { getTitle } from "src/shared/captionTextUtility";
import { NotificationTypeOptions, NotificationStatusOptions, EntityStatusCodeOptions, VisibilityOptions } from 'src/dataModels/Enums';

export interface INotificationDataModel {
    itemUIStatus______: ItemUIStatus;
    isDeleted______: boolean;
    notificationID: any;
    entity_Name: string;
    shortGuid: string;
    uniqueName: string;
    entityID: number | null | '';
    notificationType_Name: string;
    notificationTypeID: NotificationTypeOptions | null | '';
    notificationStatus_Name: string;
    notificationStatusID: NotificationStatusOptions | null | '';
    task: string;
    message: string;
    modifiedDate: string;
}

export function defaultNotification(): INotificationDataModel {
    return {
        itemUIStatus______: ItemUIStatus.New,
        isDeleted______: false,
        notificationID: null,
        entity_Name: '',
        shortGuid: '',
        uniqueName: '',
        entityID: 0,
        notificationType_Name: '',
        notificationTypeID: NotificationTypeOptions.LaunchWizard,
        notificationStatus_Name: '',
        notificationStatusID: NotificationStatusOptions.Pending,
        task: '',
        message: '',
        modifiedDate: '',
    } as unknown as INotificationDataModel;
}

// getNotificationAvatar will be used when Display the avatar of a Notification only
export function getNotificationAvatar(item: INotificationDataModel): string {
    if(!!!item) {
        return null;
    }
    return getAvatar([item.task]);
}

// getNotificationTitle will be used when Display the title of a Notification only
export function getNotificationTitle(item: INotificationDataModel): string {
    if(!!!item) {
        return null;
    }
    return getTitle([item.task]);
}


export const notificationFormValidation = Yup.object().shape({
    entityID: Yup.number(),
    task: Yup.string()
        .max(100, 'The_length_of_Task_should_be_0_to_100'),
    message: Yup.string(),
});

export const notificationAutocompleteSetting = {
    minCharacters: 2,
    stopCount: 20
} as unknown as AutocompleteSetting;

