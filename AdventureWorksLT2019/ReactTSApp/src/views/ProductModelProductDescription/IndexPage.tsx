import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { productModelProductDescriptionSelectors } from 'src/slices/ProductModelProductDescriptionSlice';
import { defaultIProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionAdvancedQuery, } from 'src/dataModels/IProductModelProductDescriptionQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IProductModelProductDescriptionAdvancedQuery>(defaultIProductModelProductDescriptionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productModelProductDescriptionSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductModelProductDescriptionAdvancedQuery(),
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
    } as ListsPartialViewProps<IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

