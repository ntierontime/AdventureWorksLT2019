export enum ListDisplayOptions {
    All,
    Selected,
}

export interface CompareHtmlTableProps<TData, TDataItem> {
    listDisplayOption: ListDisplayOptions;
    selectedIndexes: number[];
    data: TData;
    showChildren?: boolean; // e.g. show SubscriberPlanItems in SubscriberPlan
    renderItemActionButtonGroup?: (data: TDataItem) => JSX.Element;
}