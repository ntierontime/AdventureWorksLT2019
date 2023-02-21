import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { ICustomerAdvancedQuery, ICustomerIdentifier } from 'src/dataModels/ICustomerQueries';
import { ICustomerCompositeModel } from 'src/dataModels/ICustomerCompositeModel';

export class CustomerApi extends AxiosApiBaseGeneric<ICustomerDataModel, ICustomerIdentifier , ICustomerAdvancedQuery, ICustomerCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/CustomerApi/Search";

        this.url_GetCompositeModel = "api/CustomerApi/GetCompositeModel";

        this.url_BulkDelete = "api/CustomerApi/BulkDelete";

        this.url_BulkUpdate = "api/CustomerApi/BulkUpdate";

        this.url_MultiItemsCUD = "api/CustomerApi/MultiItemsCUD";

        this.url_Put = "api/CustomerApi/Put";

        this.url_Get = "api/CustomerApi/Get";

        this.url_Post = "api/CustomerApi/Post";

        this.url_Delete = "api/CustomerApi/Delete";

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
export const customerApi = new CustomerApi(apiConfig);

