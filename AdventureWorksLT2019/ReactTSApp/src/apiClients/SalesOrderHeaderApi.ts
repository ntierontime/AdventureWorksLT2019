import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';
import { ISalesOrderHeaderCompositeModel } from 'src/dataModels/ISalesOrderHeaderCompositeModel';

export class SalesOrderHeaderApi extends AxiosApiBaseGeneric<ISalesOrderHeaderDataModel, ISalesOrderHeaderIdentifier , ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/SalesOrderHeaderApi/Search";

        this.url_GetCompositeModel = "api/SalesOrderHeaderApi/GetCompositeModel";

        this.url_BulkDelete = "api/SalesOrderHeaderApi/BulkDelete";

        this.url_BulkUpdate = "api/SalesOrderHeaderApi/BulkUpdate";

        this.url_MultiItemsCUD = "api/SalesOrderHeaderApi/MultiItemsCUD";

        this.url_Put = "api/SalesOrderHeaderApi/Put";

        this.url_Get = "api/SalesOrderHeaderApi/Get";

        this.url_Post = "api/SalesOrderHeaderApi/Post";

        this.url_Delete = "api/SalesOrderHeaderApi/Delete";

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
export const salesOrderHeaderApi = new SalesOrderHeaderApi(apiConfig);

