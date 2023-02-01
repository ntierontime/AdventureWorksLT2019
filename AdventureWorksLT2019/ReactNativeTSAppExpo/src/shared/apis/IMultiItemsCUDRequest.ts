export interface IMultiItemsCUDRequest<TIdentifier, TItem> {
    deleteItems: TIdentifier[];
    mergeItems: TItem[];
    newItems: TItem[];
    updateItems: TItem[];
}
