import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { IAddressAdvancedQuery, IAddressIdentifier } from 'src/dataModels/IAddressQueries';
import { IAddressCompositeModel } from 'src/dataModels/IAddressCompositeModel';

export class AddressApi extends AxiosApiBaseGeneric<IAddressDataModel, IAddressIdentifier , IAddressAdvancedQuery, IAddressCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/AddressApi/Search";

        this.url_GetCompositeModel = "api/AddressApi/GetCompositeModel";

        this.url_BulkDelete = "api/AddressApi/BulkDelete";

        this.url_MultiItemsCUD = "api/AddressApi/MultiItemsCUD";

        this.url_Put = "api/AddressApi/Put";

        this.url_Get = "api/AddressApi/Get";

        this.url_Post = "api/AddressApi/Post";

        this.url_Delete = "api/AddressApi/Delete";

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
export const addressApi = new AddressApi(apiConfig);

