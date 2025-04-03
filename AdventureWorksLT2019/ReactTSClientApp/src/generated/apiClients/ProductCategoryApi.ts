import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IResponse } from 'src/shared/apis/IResponse';
import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { IProductCategoryIdentifier, IProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';

export class ProductCategoryApi extends AxiosApiBase {
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

    public Search = (params: IProductCategoryAdvancedQuery): Promise<IListResponse<IProductCategoryDataModel[]>> => {
        const url_Search = "api/ProductCategoryApi/Search";
        return this.post<IListResponse<IProductCategoryDataModel[]>, IProductCategoryAdvancedQuery, AxiosResponse<IListResponse<IProductCategoryDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

    public Put = (identifier: IProductCategoryIdentifier, params: IProductCategoryDataModel): Promise<IResponse<IProductCategoryDataModel>> => {
        const url_Put = "api/ProductCategoryApi/Put";
        return this.put<IResponse<IProductCategoryDataModel>, IProductCategoryDataModel, AxiosResponse<IResponse<IProductCategoryDataModel>>>(url_Put + '/' + this.convertParametersToWebApiRoute(identifier), params)
            .then(this.success);
    }

    public Get = (identifier: IProductCategoryIdentifier): Promise<IResponse<IProductCategoryDataModel>> => {
        const url_Get = "api/ProductCategoryApi/Get";
        return this.get<IResponse<IProductCategoryDataModel>, AxiosResponse<IResponse<IProductCategoryDataModel>>>(url_Get + '/' + this.convertParametersToWebApiRoute(identifier))
            .then(this.success);
    }

    public Post = (params: IProductCategoryDataModel): Promise<IResponse<IProductCategoryDataModel>> => {
        const url_Post = "api/ProductCategoryApi/Post";
        return this.post<IResponse<IProductCategoryDataModel>, IProductCategoryDataModel, AxiosResponse<IResponse<IProductCategoryDataModel>>>(url_Post, params)
            .then(this.success);
    }

}
export const productCategoryApi = new ProductCategoryApi(apiConfig);

