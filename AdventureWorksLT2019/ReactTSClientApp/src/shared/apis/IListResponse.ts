import { IPaginationResponse } from "../dataModels/IPaginationResponse";
import { IResponse } from "./IResponse";

export interface IListResponse<TResponseBody> extends IResponse<TResponseBody> {
    pagination:  IPaginationResponse;
}