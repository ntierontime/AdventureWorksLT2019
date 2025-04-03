import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import { IErrorLogIdentifier, IErrorLogAdvancedQuery } from 'src/dataModels/IErrorLogQueries';

export class ErrorLogApi extends AxiosApiBase {
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

    public Search = (params: IErrorLogAdvancedQuery): Promise<IListResponse<IErrorLogDataModel[]>> => {
        const url_Search = "api/ErrorLogApi/Search";
        return this.post<IListResponse<IErrorLogDataModel[]>, IErrorLogAdvancedQuery, AxiosResponse<IListResponse<IErrorLogDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IErrorLogIdentifier, params: IErrorLogDataModel): Promise<IResponse<IErrorLogDataModel>> => {
        const url_Put = "api/ErrorLogApi/Put";
        return this.put<IResponse<IErrorLogDataModel>, IErrorLogDataModel, AxiosResponse<IResponse<IErrorLogDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IErrorLogIdentifier): Promise<IResponse<IErrorLogDataModel>> => {
        const url_Get = "api/ErrorLogApi/Get";
        return this.get<IResponse<IErrorLogDataModel>, AxiosResponse<IResponse<IErrorLogDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IErrorLogDataModel): Promise<IResponse<IErrorLogDataModel>> => {
        const url_Post = "api/ErrorLogApi/Post";
        return this.post<IResponse<IErrorLogDataModel>, IErrorLogDataModel, AxiosResponse<IResponse<IErrorLogDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const errorLogApi = new ErrorLogApi(apiConfig);

