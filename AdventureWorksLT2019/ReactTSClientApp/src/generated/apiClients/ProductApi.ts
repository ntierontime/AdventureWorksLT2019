import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IProductDataModel } from 'src/dataModels/IProductDataModel';
import { IProductIdentifier, IProductAdvancedQuery } from 'src/dataModels/IProductQueries';

export class ProductApi extends AxiosApiBase {
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

    public Search = (params: IProductAdvancedQuery): Promise<IListResponse<IProductDataModel[]>> => {
        const url_Search = "api/ProductApi/Search";
        return this.post<IListResponse<IProductDataModel[]>, IProductAdvancedQuery, AxiosResponse<IListResponse<IProductDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IProductIdentifier, params: IProductDataModel): Promise<IResponse<IProductDataModel>> => {
        const url_Put = "api/ProductApi/Put";
        return this.put<IResponse<IProductDataModel>, IProductDataModel, AxiosResponse<IResponse<IProductDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IProductIdentifier): Promise<IResponse<IProductDataModel>> => {
        const url_Get = "api/ProductApi/Get";
        return this.get<IResponse<IProductDataModel>, AxiosResponse<IResponse<IProductDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IProductDataModel): Promise<IResponse<IProductDataModel>> => {
        const url_Post = "api/ProductApi/Post";
        return this.post<IResponse<IProductDataModel>, IProductDataModel, AxiosResponse<IResponse<IProductDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const productApi = new ProductApi(apiConfig);

