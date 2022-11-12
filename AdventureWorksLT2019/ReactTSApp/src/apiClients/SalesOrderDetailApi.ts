import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { ISalesOrderDetailAdvancedQuery, ISalesOrderDetailIdentifier } from 'src/dataModels/ISalesOrderDetailQueries';

export class SalesOrderDetailApi extends AxiosApiBaseGeneric<ISalesOrderDetailDataModel, ISalesOrderDetailIdentifier , ISalesOrderDetailAdvancedQuery, any> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/SalesOrderDetailApi/Search";

        this.url_Put = "api/SalesOrderDetailApi/Put";

        this.url_Get = "api/SalesOrderDetailApi/Get";

        this.url_Post = "api/SalesOrderDetailApi/Post";

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
export const salesOrderDetailApi = new SalesOrderDetailApi(apiConfig);

