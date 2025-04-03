import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { ICustomerAddressIdentifier, ICustomerAddressAdvancedQuery } from 'src/dataModels/ICustomerAddressQueries';

export class CustomerAddressApi extends AxiosApiBase {
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

    public Search = (params: ICustomerAddressAdvancedQuery): Promise<IListResponse<ICustomerAddressDataModel[]>> => {
        const url_Search = "api/CustomerAddressApi/Search";
        return this.post<IListResponse<ICustomerAddressDataModel[]>, ICustomerAddressAdvancedQuery, AxiosResponse<IListResponse<ICustomerAddressDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

}
export const customerAddressApi = new CustomerAddressApi(apiConfig);

