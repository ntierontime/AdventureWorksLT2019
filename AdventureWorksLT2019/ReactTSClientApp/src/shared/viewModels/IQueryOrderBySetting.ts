import { QueryOrderDirections } from "src/shared/dataModels/QueryOrderDirections";

export interface IQueryOrderBySetting {
    propertyName: string;
    displayName: string;
    direction: QueryOrderDirections;
    expression: string;
}
