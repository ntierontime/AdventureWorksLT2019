import { IResponseWithoutBody } from "./IResponseWithoutBody";

export interface IResponse<TResponseBody> extends IResponseWithoutBody {
    responseBody: TResponseBody;
}
