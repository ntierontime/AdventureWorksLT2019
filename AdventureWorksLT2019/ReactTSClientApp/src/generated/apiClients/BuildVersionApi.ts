import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { apiConfig } from 'src/apiConfig';
import { AxiosApiBase } from 'src/shared/apis/AxiosApiBase';
import { IListResponse } from 'src/shared/apis/IListResponse';
import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { IBuildVersionIdentifier, IBuildVersionAdvancedQuery } from 'src/dataModels/IBuildVersionQueries';

export class BuildVersionApi extends AxiosApiBase {
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

    public Search = (params: IBuildVersionAdvancedQuery): Promise<IListResponse<IBuildVersionDataModel[]>> => {
        const url_Search = "api/BuildVersionApi/Search";
        return this.post<IListResponse<IBuildVersionDataModel[]>, IBuildVersionAdvancedQuery, AxiosResponse<IListResponse<IBuildVersionDataModel[]>>>(url_Search, params)
            .then(this.success);
    }

}
export const buildVersionApi = new BuildVersionApi(apiConfig);

