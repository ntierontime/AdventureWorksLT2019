import {
    ActivityIndicator,
    FlatList,
    SafeAreaView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";

import { useTranslation } from 'react-i18next';

import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { IBaseQuery } from '../dataModels/IBaseQuery';
import { IQueryOrderBySetting } from '../viewModels/IQueryOrderBySetting';
import { PaginationOptions } from '../dataModels/PaginationOptions';
import { INameValuePair } from '../dataModels/INameValuePair';

export interface ListToolBarSetting {
    textSearchPlaceHolder: string;
    hasListViewOptionsSelect: boolean;
    availableListViewOptions: ListViewOptions[];
    hasItemsSelect: boolean;
    hasBulkDelete: boolean;
    hasBulkUpdate: boolean;
    hasItemsPerRowSelect: boolean;
    hasPageSizeSelect: boolean;
    hasOrderBySelect: boolean;
    hasSearch: boolean;
    hasAdvancedSearchAccordion: boolean;
    hasAdvancedSearchDialog: boolean;
}

export interface ListToolBarProps<TAdvancedQuery, TIdentifier> extends ListToolBarSetting {
    advancedQuery: TAdvancedQuery;
    defaultAdvancedQuery: TAdvancedQuery
    setAdvancedQuery: React.Dispatch<React.SetStateAction<TAdvancedQuery>>;
    rowCount: number;
    submitAdvancedSearch: (query: TAdvancedQuery) => void;

    listViewOption: ListViewOptions;
    setListViewOption: React.Dispatch<React.SetStateAction<ListViewOptions>>;

    setSelected: React.Dispatch<React.SetStateAction<TIdentifier[]>>;
    numSelected: number;
    handleSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;

    handleDeleteSelected: () => void;

    itemsPerRow: number;
    setItemsPerRow: React.Dispatch<React.SetStateAction<number>>;

    availablePageSizes: INameValuePair[];

    serverOrderBys: IQueryOrderBySetting[];

    advancedSearchExpanded: boolean;
    handleAdvancedSearchExpandClick: () => void;
    handleAdvancedSearchDialogOpen: () => void;

    hasAddNewButton: boolean; // this is a calculated value based on ListsPartialViewProps.addNewButtonContainer
    handleAddNewClick: () => void;
}

export default function ListToolBar<TAdvancedQuery extends IBaseQuery, TIdentifier>(props: ListToolBarProps<TAdvancedQuery, TIdentifier>): JSX.Element {
    const {
        textSearchPlaceHolder,
        advancedQuery, defaultAdvancedQuery, setAdvancedQuery,
        rowCount,
        submitAdvancedSearch,

        hasItemsSelect, setSelected, numSelected,
        handleSelectAllClick,

        hasBulkDelete,
        handleDeleteSelected,
        hasBulkUpdate,

        hasListViewOptionsSelect, availableListViewOptions, listViewOption, setListViewOption,

        hasPageSizeSelect, availablePageSizes,

        hasItemsPerRowSelect, itemsPerRow, setItemsPerRow,

        hasOrderBySelect, serverOrderBys,

        hasSearch,
        hasAdvancedSearchAccordion,
        advancedSearchExpanded,
        handleAdvancedSearchExpandClick,
        hasAdvancedSearchDialog,
        handleAdvancedSearchDialogOpen,

        hasAddNewButton,
        handleAddNewClick,
    } = props;
    const { t } = useTranslation();

    // 1.2. Top Toolbar - Refresh
    const handleRefresh = () => {
        setAdvancedQuery(defaultAdvancedQuery);
        setSelected([]);
        submitAdvancedSearch(advancedQuery);
    };

    // 1.3. Top Toolbar - Change ListViewOptions, MOVED
    const handleChangeListViewOptions = (
        event: React.MouseEvent<HTMLElement>,
        newSetListViewOption: ListViewOptions,
    ) => {
        if (!!!newSetListViewOption) {
            return;
        }
        advancedQuery.pageIndex = 1;
        if (newSetListViewOption === ListViewOptions.Table) {
            advancedQuery.pageSize = 10;
            advancedQuery.paginationOption = PaginationOptions.PageIndexesAndAllButtons;
        }
        else {
            advancedQuery.pageSize = advancedQuery.pageSize = 12 * Math.floor(Math.sqrt(itemsPerRow));
            advancedQuery.paginationOption = PaginationOptions.LoadMore;
        }
        setListViewOption(newSetListViewOption);

        submitAdvancedSearch(advancedQuery);
    };

    // // 1.4.1. Top Toolbar only when ListViewOptions.Table - Change PageSize
    // const handleChangePageSize = (event: SelectChangeEvent<number>) => {
    //     advancedQuery.pageSize = event.target.value as number;
    //     advancedQuery.pageIndex = 1;
    //     submitAdvancedSearch(advancedQuery);
    // };

    // // 1.4.2. Top Toolbar only when ListViewOptions.Tile - Change ItemsPerRow
    // const handleChangeItemsPerRow = (event: SelectChangeEvent<number>) => {
    //     const newItemsPerRow = event.target.value as number;
    //     setItemsPerRow(newItemsPerRow);
    //     advancedQuery.pageSize = 12 * Math.floor(Math.sqrt(newItemsPerRow));
    //     advancedQuery.pageIndex = 1;
    //     submitAdvancedSearch(advancedQuery);
    // };

    // // 1.5. Top Toolbar - Change Sort/Order by
    // const handleChangeSort = (event: SelectChangeEvent) => {
    //     advancedQuery.orderBys = event.target.value;
    //     advancedQuery.pageIndex = 1;
    //     submitAdvancedSearch(advancedQuery);
    // };

    // 1.6.1. Top Toolbar - Text Search
    const handleTextSearchClicked = () => {
        advancedQuery.pageIndex = 1;
        submitAdvancedSearch(advancedQuery);
    }

    // 1.6.2. Top Toolbar - Text to Search Changed
    const handleChangedTextToSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAdvancedQuery({ ...advancedQuery, textSearch: event.target.value });
    }

    return (
        <SafeAreaView>
            <View style={{ flex: 1 }}>
                <Text>versionDate</Text>
           </View>
        </SafeAreaView>
    );
}