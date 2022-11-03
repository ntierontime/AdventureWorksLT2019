import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { apiConfig } from 'src/apiConfig';
import { AuthenticationResponse } from 'src/shared/apis/AuthenticationResponse';
import { LoginViewModel } from 'src/shared/viewModels/LoginViewModel';

// AuthenticationApi: no token
export class AuthenticationApi extends AxiosApiBase {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);

        // this middleware is been called right before the http request is made.
        this.interceptors.request.use((param: AxiosRequestConfig) => ({
            ...param,
        }));

        // this middleware is been called right before the response is get it by the method that triggers the request
        this.interceptors.response.use((param: AxiosResponse) => ({
            ...param
        }));

        this.login = this.login.bind(this);
    }

    public login = (credentials: LoginViewModel): Promise<AuthenticationResponse> => {
        const url = "api/AuthenticationApi/Login";
        return this.post<AuthenticationResponse, LoginViewModel, AxiosResponse<AuthenticationResponse>>(url, credentials)
            .then(res => {
                this.setToken(res.data.token);
                return this.success(res);
            });
    }
}

export const authenticationApi = new AuthenticationApi(apiConfig);