import { IPaginationResponse } from "./IPaginationResponse";
import { IResponse } from "../apis/IResponse";

export interface ICompositeModel<TMaster, TPropertyEnum> {
    __Master__: TMaster;
    responses: Record<string, IResponse<IPaginationResponse>>;
}
