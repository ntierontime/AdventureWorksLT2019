import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { customerSelectors } from 'src/slices/CustomerSlice';
import { defaultICustomerAdvancedQuery, ICustomerAdvancedQuery, } from 'src/dataModels/ICustomerQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<ICustomerAdvancedQuery>(defaultICustomerAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => customerSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultICustomerAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ToolBar, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' +t("Customer"),
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
    } as ListsPartialViewProps<ICustomerAdvancedQuery, ICustomerDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

