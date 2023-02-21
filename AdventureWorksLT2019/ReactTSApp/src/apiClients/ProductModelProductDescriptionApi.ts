import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionIdentifier } from 'src/dataModels/IProductModelProductDescriptionQueries';

export class ProductModelProductDescriptionApi extends AxiosApiBaseGeneric<IProductModelProductDescriptionDataModel, IProductModelProductDescriptionIdentifier , IProductModelProductDescriptionAdvancedQuery, any> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ProductModelProductDescriptionApi/Search";

        this.url_Put = "api/ProductModelProductDescriptionApi/Put";

        this.url_Get = "api/ProductModelProductDescriptionApi/Get";

        this.url_Post = "api/ProductModelProductDescriptionApi/Post";

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
export const productModelProductDescriptionApi = new ProductModelProductDescriptionApi(apiConfig);

