// NTierOnTime.React.Models.AccountViewModels.LoginViewModel
export interface RegistrationRequest<TRegistrationTypes, TPerson> {
    email: string;
    password: string;
    confirmPassword: string;
    registrationType: TRegistrationTypes;
    person: TPerson;
}
