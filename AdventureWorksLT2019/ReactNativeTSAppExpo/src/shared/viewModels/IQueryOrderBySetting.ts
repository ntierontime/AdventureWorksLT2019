import { QueryOrderDirections } from "../dataModels/QueryOrderDirections";

export interface IQueryOrderBySetting {
    propertyName: string;
    displayName: string;
    direction: QueryOrderDirections;
    expression: string;
}
