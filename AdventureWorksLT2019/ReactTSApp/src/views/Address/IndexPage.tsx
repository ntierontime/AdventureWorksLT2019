import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { addressSelectors } from 'src/slices/AddressSlice';
import { defaultIAddressAdvancedQuery, IAddressAdvancedQuery, } from 'src/dataModels/IAddressQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IAddressAdvancedQuery>(defaultIAddressAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => addressSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIAddressAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ToolBar, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' +t("Address"),
            hasListViewOptionsSelect: true,
	        availableListViewOptions: [ListViewOptions.Table],
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
    } as ListsPartialViewProps<IAddressAdvancedQuery, IAddressDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

