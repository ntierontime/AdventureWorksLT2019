import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { salesOrderHeaderSelectors } from 'src/slices/SalesOrderHeaderSlice';
import { defaultISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderAdvancedQuery, } from 'src/dataModels/ISalesOrderHeaderQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<ISalesOrderHeaderAdvancedQuery>(defaultISalesOrderHeaderAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => salesOrderHeaderSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultISalesOrderHeaderAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.Absolute, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' + t("SalesOrderHeader"),
            hasListViewOptionsSelect: true,
	        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
            hasItemsSelect: true,
            hasBulkDelete: true,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: false,
            hasAdvancedSearchDialog: true,
        }
    } as ListsPartialViewProps<ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

