import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from '../apiConfig';
import { AxiosApiBaseGeneric } from '../shared/apis/AxiosApiBaseGeneric';
import { IBuildVersionDataModel } from '../dataModels/IBuildVersionDataModel';
import { IBuildVersionAdvancedQuery, IBuildVersionIdentifier } from '../dataModels/IBuildVersionQueries';
import { IBuildVersionCompositeModel } from '../dataModels/IBuildVersionCompositeModel';

export class BuildVersionApi extends AxiosApiBaseGeneric<IBuildVersionDataModel, IBuildVersionIdentifier , IBuildVersionAdvancedQuery, IBuildVersionCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/BuildVersionApi/Search";

        this.url_GetCompositeModel = "api/BuildVersionApi/GetCompositeModel";

        this.url_BulkDelete = "api/BuildVersionApi/BulkDelete";

        this.url_MultiItemsCUD = "api/BuildVersionApi/MultiItemsCUD";

        this.url_Put = "api/BuildVersionApi/Put";

        this.url_Get = "api/BuildVersionApi/Get";

        this.url_Post = "api/BuildVersionApi/Post";

        this.url_Delete = "api/BuildVersionApi/Delete";

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

