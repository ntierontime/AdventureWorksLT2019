import { IPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { IResponse } from "src/shared/apis/IResponse";

export interface ICompositeModel<TMaster, TPropertyEnum> {
    __Master__: TMaster;
    responses: Record<string, IResponse<IPaginationResponse>> | null;
}
