import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { apiConfig } from 'src/apiConfig';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { IListResponse } from 'src/shared/apis/IListResponse';

import { IBuildVersionAdvancedQuery } from 'src/dataModels/IBuildVersionQueries';
import { IErrorLogAdvancedQuery } from 'src/dataModels/IErrorLogQueries';
import { IAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { ICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { ICustomerAddressAdvancedQuery } from 'src/dataModels/ICustomerAddressQueries';
import { IProductAdvancedQuery } from 'src/dataModels/IProductQueries';
import { IProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { IProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';
import { IProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { IProductModelProductDescriptionAdvancedQuery } from 'src/dataModels/IProductModelProductDescriptionQueries';
import { ISalesOrderDetailAdvancedQuery } from 'src/dataModels/ISalesOrderDetailQueries';
import { ISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';

export class CodeListsApi extends AxiosApiBase {
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

        this.getBuildVersionCodeList = this.getBuildVersionCodeList.bind(this);
        this.getErrorLogCodeList = this.getErrorLogCodeList.bind(this);
        this.getAddressCodeList = this.getAddressCodeList.bind(this);
        this.getCustomerCodeList = this.getCustomerCodeList.bind(this);
        this.getCustomerAddressCodeList = this.getCustomerAddressCodeList.bind(this);
        this.getProductCodeList = this.getProductCodeList.bind(this);
        this.getProductCategoryCodeList = this.getProductCategoryCodeList.bind(this);
        this.getProductDescriptionCodeList = this.getProductDescriptionCodeList.bind(this);
        this.getProductModelCodeList = this.getProductModelCodeList.bind(this);
        this.getProductModelProductDescriptionCodeList = this.getProductModelProductDescriptionCodeList.bind(this);
        this.getSalesOrderDetailCodeList = this.getSalesOrderDetailCodeList.bind(this);
        this.getSalesOrderHeaderCodeList = this.getSalesOrderHeaderCodeList.bind(this);
    }


    public getBuildVersionCodeList = (query: IBuildVersionAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetBuildVersionCodeList";
        return this.post<IListResponse<INameValuePair[]>, IBuildVersionAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getErrorLogCodeList = (query: IErrorLogAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetErrorLogCodeList";
        return this.post<IListResponse<INameValuePair[]>, IErrorLogAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getAddressCodeList = (query: IAddressAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetAddressCodeList";
        return this.post<IListResponse<INameValuePair[]>, IAddressAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getCustomerCodeList = (query: ICustomerAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetCustomerCodeList";
        return this.post<IListResponse<INameValuePair[]>, ICustomerAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getCustomerAddressCodeList = (query: ICustomerAddressAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetCustomerAddressCodeList";
        return this.post<IListResponse<INameValuePair[]>, ICustomerAddressAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getProductCodeList = (query: IProductAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetProductCodeList";
        return this.post<IListResponse<INameValuePair[]>, IProductAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getProductCategoryCodeList = (query: IProductCategoryAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetProductCategoryCodeList";
        return this.post<IListResponse<INameValuePair[]>, IProductCategoryAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getProductDescriptionCodeList = (query: IProductDescriptionAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetProductDescriptionCodeList";
        return this.post<IListResponse<INameValuePair[]>, IProductDescriptionAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getProductModelCodeList = (query: IProductModelAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetProductModelCodeList";
        return this.post<IListResponse<INameValuePair[]>, IProductModelAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getProductModelProductDescriptionCodeList = (query: IProductModelProductDescriptionAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetProductModelProductDescriptionCodeList";
        return this.post<IListResponse<INameValuePair[]>, IProductModelProductDescriptionAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getSalesOrderDetailCodeList = (query: ISalesOrderDetailAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetSalesOrderDetailCodeList";
        return this.post<IListResponse<INameValuePair[]>, ISalesOrderDetailAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }

    public getSalesOrderHeaderCodeList = (query: ISalesOrderHeaderAdvancedQuery): Promise<IListResponse<INameValuePair[]>> => {
        const url = "api/CodeListsApi/GetSalesOrderHeaderCodeList";
        return this.post<IListResponse<INameValuePair[]>, ISalesOrderHeaderAdvancedQuery, AxiosResponse<IListResponse<INameValuePair[]>>>(url, query)
            .then(res => {
                return this.success(res);
            });
    }
}

export const codeListsApi = new CodeListsApi(apiConfig);

