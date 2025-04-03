import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { ISalesOrderDetailIdentifier, ISalesOrderDetailAdvancedQuery } from 'src/dataModels/ISalesOrderDetailQueries';

export class SalesOrderDetailApi extends AxiosApiBase {
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

    public Search = (params: ISalesOrderDetailAdvancedQuery): Promise<IListResponse<ISalesOrderDetailDataModel[]>> => {
        const url_Search = "api/SalesOrderDetailApi/Search";
        return this.post<IListResponse<ISalesOrderDetailDataModel[]>, ISalesOrderDetailAdvancedQuery, AxiosResponse<IListResponse<ISalesOrderDetailDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

}
export const salesOrderDetailApi = new SalesOrderDetailApi(apiConfig);

