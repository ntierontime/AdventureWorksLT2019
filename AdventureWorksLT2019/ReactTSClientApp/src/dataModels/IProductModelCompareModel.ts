import { IProductModelCompositeModel } from "./IProductModelCompositeModel";


export interface IProductModelCompareModel {
    productModelCompositeModelList: IProductModelCompositeModel[], 
    // 4. ListTable = 4
    compareResult_Products_Via_ProductModelID: {key: string, value: boolean[]}[];
    compareResult_ProductModelProductDescriptions_Via_ProductModelID:  {key: string, value: boolean[]}[];
}
