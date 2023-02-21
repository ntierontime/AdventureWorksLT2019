import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { ICustomerAddressAdvancedQuery, ICustomerAddressIdentifier } from 'src/dataModels/ICustomerAddressQueries';

export class CustomerAddressApi extends AxiosApiBaseGeneric<ICustomerAddressDataModel, ICustomerAddressIdentifier , ICustomerAddressAdvancedQuery, any> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/CustomerAddressApi/Search";

        this.url_Put = "api/CustomerAddressApi/Put";

        this.url_Get = "api/CustomerAddressApi/Get";

        this.url_Post = "api/CustomerAddressApi/Post";

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

