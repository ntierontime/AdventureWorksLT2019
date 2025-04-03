import { ListViewOptions } from "./ListViewOptions";

export interface IndexPageProps {
    searchMethod?: string;
    currentListViewOption?: ListViewOptions;
    hasListToolBar?: boolean;
    hasAdvancedSearch?: boolean;
}
