export interface IBulkUpdateRequest<TIdentifier, TActionData> {
    ids: TIdentifier[];
    actionName: string;
    actionData: TActionData;
}
