import { ListToolBarSetting } from "../views/ListToolBar";
import { ListViewOptions } from "../views/ListViewOptions";

export interface ListsPartialViewProps<TAdvancedQuery, TDataModel> {
    advancedQuery: TAdvancedQuery;
    setAdvancedQuery: React.Dispatch<React.SetStateAction<TAdvancedQuery>>;
    defaultAdvancedQuery: TAdvancedQuery;
    listItems: TDataModel[];
    initialLoadFromServer: boolean;
    hasListToolBar: boolean;
    hasAdvancedSearch: boolean;
    listToolBarSetting: ListToolBarSetting;
}
