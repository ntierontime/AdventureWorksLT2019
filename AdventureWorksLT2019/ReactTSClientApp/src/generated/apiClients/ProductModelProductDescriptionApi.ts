import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { IProductModelProductDescriptionIdentifier, IProductModelProductDescriptionAdvancedQuery } from 'src/dataModels/IProductModelProductDescriptionQueries';

export class ProductModelProductDescriptionApi extends AxiosApiBase {
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

    public Search = (params: IProductModelProductDescriptionAdvancedQuery): Promise<IListResponse<IProductModelProductDescriptionDataModel[]>> => {
        const url_Search = "api/ProductModelProductDescriptionApi/Search";
        return this.post<IListResponse<IProductModelProductDescriptionDataModel[]>, IProductModelProductDescriptionAdvancedQuery, AxiosResponse<IListResponse<IProductModelProductDescriptionDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

}
export const productModelProductDescriptionApi = new ProductModelProductDescriptionApi(apiConfig);

