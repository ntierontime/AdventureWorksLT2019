import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { ICustomerIdentifier, ICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

export class CustomerApi extends AxiosApiBase {
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

    public Search = (params: ICustomerAdvancedQuery): Promise<IListResponse<ICustomerDataModel[]>> => {
        const url_Search = "api/CustomerApi/Search";
        return this.post<IListResponse<ICustomerDataModel[]>, ICustomerAdvancedQuery, AxiosResponse<IListResponse<ICustomerDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: ICustomerIdentifier, params: ICustomerDataModel): Promise<IResponse<ICustomerDataModel>> => {
        const url_Put = "api/CustomerApi/Put";
        return this.put<IResponse<ICustomerDataModel>, ICustomerDataModel, AxiosResponse<IResponse<ICustomerDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: ICustomerIdentifier): Promise<IResponse<ICustomerDataModel>> => {
        const url_Get = "api/CustomerApi/Get";
        return this.get<IResponse<ICustomerDataModel>, AxiosResponse<IResponse<ICustomerDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: ICustomerDataModel): Promise<IResponse<ICustomerDataModel>> => {
        const url_Post = "api/CustomerApi/Post";
        return this.post<IResponse<ICustomerDataModel>, ICustomerDataModel, AxiosResponse<IResponse<ICustomerDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const customerApi = new CustomerApi(apiConfig);

