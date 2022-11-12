import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { IBuildVersionAdvancedQuery, IBuildVersionIdentifier } from 'src/dataModels/IBuildVersionQueries';

export class BuildVersionApi extends AxiosApiBaseGeneric<IBuildVersionDataModel, IBuildVersionIdentifier , IBuildVersionAdvancedQuery, any> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/BuildVersionApi/Search";

        this.url_Get = "api/BuildVersionApi/Get";

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
export const buildVersionApi = new BuildVersionApi(apiConfig);

