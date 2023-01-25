import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { productDescriptionSelectors } from 'src/slices/ProductDescriptionSlice';
import { defaultIProductDescriptionAdvancedQuery, IProductDescriptionAdvancedQuery, } from 'src/dataModels/IProductDescriptionQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
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
        addNewButtonContainer: ContainerOptions.Absolute, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' + t("ProductDescription"),
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
    } as ListsPartialViewProps<IProductDescriptionAdvancedQuery, IProductDescriptionDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

