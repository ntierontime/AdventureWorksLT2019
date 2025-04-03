import { RegularBreakpoints } from "@mui/material";
import { ListToolBarSetting } from "../views/ListToolBar";
import { ContainerOptions } from "./ContainerOptions";
import { ListViewOptions } from "../views/ListViewOptions";

export interface ListsPartialViewProps<TAdvancedQuery, TDataModel> {
    currentListViewOption: ListViewOptions;
    searchMethodName: string;
    advancedQuery: TAdvancedQuery;
    setAdvancedQuery?: React.Dispatch<React.SetStateAction<TAdvancedQuery>>;
    setAdvancedQueryInSlice?: (advancedQuery: TAdvancedQuery) => void;
    defaultAdvancedQuery: TAdvancedQuery;
    listItems: TDataModel[];
    initialLoadFromServer: boolean;
    hasListToolBar: boolean;
    listToolBarSetting: ListToolBarSetting;
    hasAdvancedSearch: boolean;
    addNewButtonContainer: ContainerOptions;
    itemPopupGridColumns?: RegularBreakpoints; //  by default how to display editors/inputs in mui Grid system.
    itemPopupScrollableCardContent?: any;
}
