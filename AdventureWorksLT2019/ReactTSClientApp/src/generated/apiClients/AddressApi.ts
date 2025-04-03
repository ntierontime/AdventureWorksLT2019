import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { IAddressIdentifier, IAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';

export class AddressApi extends AxiosApiBase {
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

    public Search = (params: IAddressAdvancedQuery): Promise<IListResponse<IAddressDataModel[]>> => {
        const url_Search = "api/AddressApi/Search";
        return this.post<IListResponse<IAddressDataModel[]>, IAddressAdvancedQuery, AxiosResponse<IListResponse<IAddressDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IAddressIdentifier, params: IAddressDataModel): Promise<IResponse<IAddressDataModel>> => {
        const url_Put = "api/AddressApi/Put";
        return this.put<IResponse<IAddressDataModel>, IAddressDataModel, AxiosResponse<IResponse<IAddressDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IAddressIdentifier): Promise<IResponse<IAddressDataModel>> => {
        const url_Get = "api/AddressApi/Get";
        return this.get<IResponse<IAddressDataModel>, AxiosResponse<IResponse<IAddressDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IAddressDataModel): Promise<IResponse<IAddressDataModel>> => {
        const url_Post = "api/AddressApi/Post";
        return this.post<IResponse<IAddressDataModel>, IAddressDataModel, AxiosResponse<IResponse<IAddressDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const addressApi = new AddressApi(apiConfig);

