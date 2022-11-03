import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import { IErrorLogAdvancedQuery, IErrorLogIdentifier } from 'src/dataModels/IErrorLogQueries';
import { IErrorLogCompositeModel } from 'src/dataModels/IErrorLogCompositeModel';

export class ErrorLogApi extends AxiosApiBaseGeneric<IErrorLogDataModel, IErrorLogIdentifier , IErrorLogAdvancedQuery, IErrorLogCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ErrorLogApi/Search";

        this.url_GetCompositeModel = "api/ErrorLogApi/GetCompositeModel";

        this.url_BulkDelete = "api/ErrorLogApi/BulkDelete";

        this.url_MultiItemsCUD = "api/ErrorLogApi/MultiItemsCUD";

        this.url_Put = "api/ErrorLogApi/Put";

        this.url_Get = "api/ErrorLogApi/Get";

        this.url_Post = "api/ErrorLogApi/Post";

        this.url_Delete = "api/ErrorLogApi/Delete";

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
export const errorLogApi = new ErrorLogApi(apiConfig);

