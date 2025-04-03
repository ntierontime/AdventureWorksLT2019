import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { IProductModelIdentifier, IProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';

export class ProductModelApi extends AxiosApiBase {
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

    public Search = (params: IProductModelAdvancedQuery): Promise<IListResponse<IProductModelDataModel[]>> => {
        const url_Search = "api/ProductModelApi/Search";
        return this.post<IListResponse<IProductModelDataModel[]>, IProductModelAdvancedQuery, AxiosResponse<IListResponse<IProductModelDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IProductModelIdentifier, params: IProductModelDataModel): Promise<IResponse<IProductModelDataModel>> => {
        const url_Put = "api/ProductModelApi/Put";
        return this.put<IResponse<IProductModelDataModel>, IProductModelDataModel, AxiosResponse<IResponse<IProductModelDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IProductModelIdentifier): Promise<IResponse<IProductModelDataModel>> => {
        const url_Get = "api/ProductModelApi/Get";
        return this.get<IResponse<IProductModelDataModel>, AxiosResponse<IResponse<IProductModelDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IProductModelDataModel): Promise<IResponse<IProductModelDataModel>> => {
        const url_Post = "api/ProductModelApi/Post";
        return this.post<IResponse<IProductModelDataModel>, IProductModelDataModel, AxiosResponse<IResponse<IProductModelDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const productModelApi = new ProductModelApi(apiConfig);

