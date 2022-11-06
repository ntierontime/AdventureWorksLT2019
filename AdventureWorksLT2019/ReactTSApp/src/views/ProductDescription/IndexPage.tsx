import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { productDescriptionSelectors } from 'src/slices/ProductDescriptionSlice';
import { defaultIProductDescriptionAdvancedQuery, IProductDescriptionAdvancedQuery, } from 'src/dataModels/IProductDescriptionQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IProductDescriptionAdvancedQuery>(defaultIProductDescriptionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productDescriptionSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductDescriptionAdvancedQuery(),
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
    } as ListsPartialViewProps<IProductDescriptionAdvancedQuery, IProductDescriptionDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

