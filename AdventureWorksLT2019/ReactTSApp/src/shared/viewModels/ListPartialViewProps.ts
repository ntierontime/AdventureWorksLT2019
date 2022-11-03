import { ListViewOptions } from "../views/ListViewOptions";

export interface ListPartialViewProps<TDataModel, TIdentifier> {
    listViewOption: ListViewOptions,
    listItems: TDataModel[],
    itemsPerRow: number,
    numSelected: number,
    selected: readonly TIdentifier[],
    handleChangePage: (event: React.ChangeEvent<unknown>, value: number) => void,
    handleSelectItemClick: (item: TDataModel) => void,
}
