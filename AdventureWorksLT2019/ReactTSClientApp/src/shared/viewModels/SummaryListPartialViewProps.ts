import { RegularBreakpoints } from "@mui/material";
import { ListViewOptions } from "../views/ListViewOptions";
import { CrudViewContainers } from "./CrudViewContainers";

export interface SummaryListPartialViewProps<TQuery> {
    refresh: boolean;
    toLoadOwnerConsumerOnButtons?: boolean;
    hasListToolBar: boolean;
    itemsPerRow?: RegularBreakpoints
    listViewOption: ListViewOptions;
    listItemOnclickTarget?: CrudViewContainers;
    itemUrl?: string;
    currentItemIndex?: number;
    searchMethodName?: string;
    query?: TQuery;

    // the following 3 properties are used for top list,
    // will display refresh button, title, and a "see all" link use listUrl
    hasOutstandingListToolBar?: boolean;
    title?: string;
    listUrl?: string;
}
