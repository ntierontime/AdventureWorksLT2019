import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { IProductDescriptionAdvancedQuery, IProductDescriptionIdentifier } from 'src/dataModels/IProductDescriptionQueries';
import { IProductDescriptionCompositeModel } from 'src/dataModels/IProductDescriptionCompositeModel';

export class ProductDescriptionApi extends AxiosApiBaseGeneric<IProductDescriptionDataModel, IProductDescriptionIdentifier , IProductDescriptionAdvancedQuery, IProductDescriptionCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ProductDescriptionApi/Search";

        this.url_GetCompositeModel = "api/ProductDescriptionApi/GetCompositeModel";

        this.url_BulkDelete = "api/ProductDescriptionApi/BulkDelete";

        this.url_MultiItemsCUD = "api/ProductDescriptionApi/MultiItemsCUD";

        this.url_Put = "api/ProductDescriptionApi/Put";

        this.url_Get = "api/ProductDescriptionApi/Get";

        this.url_Post = "api/ProductDescriptionApi/Post";

        this.url_Delete = "api/ProductDescriptionApi/Delete";

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
export const productDescriptionApi = new ProductDescriptionApi(apiConfig);

