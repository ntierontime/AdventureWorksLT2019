import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { ISalesOrderHeaderIdentifier, ISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';

export class SalesOrderHeaderApi extends AxiosApiBase {
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

    public Search = (params: ISalesOrderHeaderAdvancedQuery): Promise<IListResponse<ISalesOrderHeaderDataModel[]>> => {
        const url_Search = "api/SalesOrderHeaderApi/Search";
        return this.post<IListResponse<ISalesOrderHeaderDataModel[]>, ISalesOrderHeaderAdvancedQuery, AxiosResponse<IListResponse<ISalesOrderHeaderDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: ISalesOrderHeaderIdentifier, params: ISalesOrderHeaderDataModel): Promise<IResponse<ISalesOrderHeaderDataModel>> => {
        const url_Put = "api/SalesOrderHeaderApi/Put";
        return this.put<IResponse<ISalesOrderHeaderDataModel>, ISalesOrderHeaderDataModel, AxiosResponse<IResponse<ISalesOrderHeaderDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: ISalesOrderHeaderIdentifier): Promise<IResponse<ISalesOrderHeaderDataModel>> => {
        const url_Get = "api/SalesOrderHeaderApi/Get";
        return this.get<IResponse<ISalesOrderHeaderDataModel>, AxiosResponse<IResponse<ISalesOrderHeaderDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: ISalesOrderHeaderDataModel): Promise<IResponse<ISalesOrderHeaderDataModel>> => {
        const url_Post = "api/SalesOrderHeaderApi/Post";
        return this.post<IResponse<ISalesOrderHeaderDataModel>, ISalesOrderHeaderDataModel, AxiosResponse<IResponse<ISalesOrderHeaderDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const salesOrderHeaderApi = new SalesOrderHeaderApi(apiConfig);

