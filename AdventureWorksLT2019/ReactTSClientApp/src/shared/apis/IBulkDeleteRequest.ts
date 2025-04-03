import { IPaginationResponse } from "../dataModels/IPaginationResponse";
import { IResponse } from "./IResponse";

export interface IBulkDeleteRequest<TIdentifier> {
    ids: TIdentifier[];
    actionName: string;
}
