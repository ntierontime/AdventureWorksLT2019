import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { IProductCategoryAdvancedQuery, IProductCategoryIdentifier } from 'src/dataModels/IProductCategoryQueries';
import { IProductCategoryCompositeModel } from 'src/dataModels/IProductCategoryCompositeModel';

export class ProductCategoryApi extends AxiosApiBaseGeneric<IProductCategoryDataModel, IProductCategoryIdentifier , IProductCategoryAdvancedQuery, IProductCategoryCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ProductCategoryApi/Search";

        this.url_GetCompositeModel = "api/ProductCategoryApi/GetCompositeModel";

        this.url_BulkDelete = "api/ProductCategoryApi/BulkDelete";

        this.url_MultiItemsCUD = "api/ProductCategoryApi/MultiItemsCUD";

        this.url_Put = "api/ProductCategoryApi/Put";

        this.url_Get = "api/ProductCategoryApi/Get";

        this.url_Post = "api/ProductCategoryApi/Post";

        this.url_Delete = "api/ProductCategoryApi/Delete";

        this.url_CreateComposite = "api/ProductCategoryApi/CreateComposite";

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
export const productCategoryApi = new ProductCategoryApi(apiConfig);

