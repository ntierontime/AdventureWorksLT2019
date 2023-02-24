import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { IProductModelAdvancedQuery, IProductModelIdentifier } from 'src/dataModels/IProductModelQueries';
import { IProductModelCompositeModel } from 'src/dataModels/IProductModelCompositeModel';

export class ProductModelApi extends AxiosApiBaseGeneric<IProductModelDataModel, IProductModelIdentifier , IProductModelAdvancedQuery, IProductModelCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/ProductModelApi/Search";

        this.url_GetCompositeModel = "api/ProductModelApi/GetCompositeModel";

        this.url_BulkDelete = "api/ProductModelApi/BulkDelete";

        this.url_MultiItemsCUD = "api/ProductModelApi/MultiItemsCUD";

        this.url_Put = "api/ProductModelApi/Put";

        this.url_Get = "api/ProductModelApi/Get";

        this.url_Post = "api/ProductModelApi/Post";

        this.url_Delete = "api/ProductModelApi/Delete";

        this.url_CreateComposite = "api/ProductModelApi/CreateComposite";

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
export const productModelApi = new ProductModelApi(apiConfig);

