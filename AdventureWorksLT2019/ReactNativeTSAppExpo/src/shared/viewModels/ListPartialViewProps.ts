import { ListViewOptions } from "../views/ListViewOptions";
import { ViewItemTemplates } from "./ViewItemTemplates";

export interface ListPartialViewProps<TDataModel, TIdentifier> {
    listViewOption: ListViewOptions,
    listItems: TDataModel[],
    itemsPerRow: number,

    hasItemsSelect: boolean;
    isSelected?: (identifier: TIdentifier) => boolean,
    numSelected: number,
    selected: readonly TIdentifier[],
    handleChangePage: (event: React.ChangeEvent<unknown>, value: number) => void,
    handleSelectItemClick: (item: TDataModel) => void,

    handleItemDialogOpen?: (viewItemTemplate: ViewItemTemplates, itemIndex: number) => void,
    currentItemOnDialog? : TDataModel,
    setCurrentItemOnDialog? : React.Dispatch<React.SetStateAction<TDataModel>>
    currentItemIndex?: number,
    setCurrentItemIndex?: React.Dispatch<React.SetStateAction<number>>
}
