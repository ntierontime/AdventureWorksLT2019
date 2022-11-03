import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { ICustomerAddressAdvancedQuery, ICustomerAddressIdentifier } from 'src/dataModels/ICustomerAddressQueries';
import { ICustomerAddressCompositeModel } from 'src/dataModels/ICustomerAddressCompositeModel';

export class CustomerAddressApi extends AxiosApiBaseGeneric<ICustomerAddressDataModel, ICustomerAddressIdentifier , ICustomerAddressAdvancedQuery, ICustomerAddressCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/CustomerAddressApi/Search";

        this.url_GetCompositeModel = "api/CustomerAddressApi/GetCompositeModel";

        this.url_BulkDelete = "api/CustomerAddressApi/BulkDelete";

        this.url_MultiItemsCUD = "api/CustomerAddressApi/MultiItemsCUD";

        this.url_Put = "api/CustomerAddressApi/Put";

        this.url_Get = "api/CustomerAddressApi/Get";

        this.url_Post = "api/CustomerAddressApi/Post";

        this.url_Delete = "api/CustomerAddressApi/Delete";

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
export const customerAddressApi = new CustomerAddressApi(apiConfig);

