import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBaseGeneric } from 'src/shared/apis/AxiosApiBaseGeneric';
import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { ISalesOrderDetailAdvancedQuery, ISalesOrderDetailIdentifier } from 'src/dataModels/ISalesOrderDetailQueries';
import { ISalesOrderDetailCompositeModel } from 'src/dataModels/ISalesOrderDetailCompositeModel';

export class SalesOrderDetailApi extends AxiosApiBaseGeneric<ISalesOrderDetailDataModel, ISalesOrderDetailIdentifier , ISalesOrderDetailAdvancedQuery, ISalesOrderDetailCompositeModel> {
    public constructor(conf?: AxiosRequestConfig) {
        super(conf);


        this.url_Search = "api/SalesOrderDetailApi/Search";

        this.url_GetCompositeModel = "api/SalesOrderDetailApi/GetCompositeModel";

        this.url_BulkDelete = "api/SalesOrderDetailApi/BulkDelete";

        this.url_MultiItemsCUD = "api/SalesOrderDetailApi/MultiItemsCUD";

        this.url_Put = "api/SalesOrderDetailApi/Put";

        this.url_Get = "api/SalesOrderDetailApi/Get";

        this.url_Post = "api/SalesOrderDetailApi/Post";

        this.url_Delete = "api/SalesOrderDetailApi/Delete";

        this.url_CreateComposite = "api/SalesOrderDetailApi/CreateComposite";

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

