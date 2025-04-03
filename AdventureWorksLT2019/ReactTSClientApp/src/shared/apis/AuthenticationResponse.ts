import { UIRouteLinkSetting } from "../dataModels/UIRouteLinkSetting";

// NTierOnTime.WebApiMessage.AuthenticationResponse
export interface AuthenticationResponse<TPerson, TNotification>{
    succeeded: boolean;
    isLockedOut: boolean;
    isNotAllowed: boolean;
    requiresTwoFactor: boolean;
    token: string | null;
    expiresIn: number;
    refreshToken: string | null;
    entityID: number | null;
    roles: string[];
    person: TPerson;
    notifications: TNotification[]
    userData: {
        partnerConsoleList: UIRouteLinkSetting[];
        employeeConsoleList: UIRouteLinkSetting[];
    },
}
