import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';


export class SiteDataApi extends AxiosApiBase {
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
    }

}
export const siteDataApi = new SiteDataApi(apiConfig);

