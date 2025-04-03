import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { IProductDescriptionIdentifier, IProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';

export class ProductDescriptionApi extends AxiosApiBase {
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

    public Search = (params: IProductDescriptionAdvancedQuery): Promise<IListResponse<IProductDescriptionDataModel[]>> => {
        const url_Search = "api/ProductDescriptionApi/Search";
        return this.post<IListResponse<IProductDescriptionDataModel[]>, IProductDescriptionAdvancedQuery, AxiosResponse<IListResponse<IProductDescriptionDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IProductDescriptionIdentifier, params: IProductDescriptionDataModel): Promise<IResponse<IProductDescriptionDataModel>> => {
        const url_Put = "api/ProductDescriptionApi/Put";
        return this.put<IResponse<IProductDescriptionDataModel>, IProductDescriptionDataModel, AxiosResponse<IResponse<IProductDescriptionDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IProductDescriptionIdentifier): Promise<IResponse<IProductDescriptionDataModel>> => {
        const url_Get = "api/ProductDescriptionApi/Get";
        return this.get<IResponse<IProductDescriptionDataModel>, AxiosResponse<IResponse<IProductDescriptionDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IProductDescriptionDataModel): Promise<IResponse<IProductDescriptionDataModel>> => {
        const url_Post = "api/ProductDescriptionApi/Post";
        return this.post<IResponse<IProductDescriptionDataModel>, IProductDescriptionDataModel, AxiosResponse<IResponse<IProductDescriptionDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const productDescriptionApi = new ProductDescriptionApi(apiConfig);

