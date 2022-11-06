import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { salesOrderDetailSelectors } from 'src/slices/SalesOrderDetailSlice';
import { defaultISalesOrderDetailAdvancedQuery, ISalesOrderDetailAdvancedQuery, } from 'src/dataModels/ISalesOrderDetailQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<ISalesOrderDetailAdvancedQuery>(defaultISalesOrderDetailAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => salesOrderDetailSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultISalesOrderDetailAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        listToolBarSetting: {
            hasListViewOptionsSelect: true,
	        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
            hasItemsSelect: true,
            hasBulkDelete: true,
            hasBulkUpdate: true,
            hasItemsPerRowSelect: true,
            hasPageSizeSelect: true,
            hasOrderBySelect: true,
            hasSearch: true,
            hasAdvancedSearchAccordion: true,
            hasAdvancedSearchDialog: true,
        }
    } as ListsPartialViewProps<ISalesOrderDetailAdvancedQuery, ISalesOrderDetailDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

