import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IProductDataModel } from 'src/dataModels/IProductDataModel';
import { IProductAdvancedQuery, IProductIdentifier } from 'src/dataModels/IProductQueries';
import { IProductCompositeModel } from 'src/dataModels/IProductCompositeModel';

export class ProductApi extends AxiosApiBaseGeneric<IProductDataModel, IProductIdentifier , IProductAdvancedQuery, IProductCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ProductApi/Search";

        this.url_GetCompositeModel = "api/ProductApi/GetCompositeModel";

        this.url_BulkDelete = "api/ProductApi/BulkDelete";

        this.url_MultiItemsCUD = "api/ProductApi/MultiItemsCUD";

        this.url_Put = "api/ProductApi/Put";

        this.url_Get = "api/ProductApi/Get";

        this.url_Post = "api/ProductApi/Post";

        this.url_Delete = "api/ProductApi/Delete";

        this.url_CreateComposite = "api/ProductApi/CreateComposite";

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
export const productApi = new ProductApi(apiConfig);

