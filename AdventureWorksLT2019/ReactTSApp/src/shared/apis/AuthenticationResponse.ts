// NTierOnTime.WebApiMessage.AuthenticationResponse
export interface AuthenticationResponse {
    succeeded: boolean;
    isLockedOut: boolean;
    isNotAllowed: boolean;
    requiresTwoFactor: boolean;
    token: string | null;
    expiresIn: number;
    refreshToken: string | null;
    entityID: number | null;
    roles: string[];
}
