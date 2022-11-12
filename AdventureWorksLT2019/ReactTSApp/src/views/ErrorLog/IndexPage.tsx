import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import { errorLogSelectors } from 'src/slices/ErrorLogSlice';
import { defaultIErrorLogAdvancedQuery, IErrorLogAdvancedQuery, } from 'src/dataModels/IErrorLogQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IErrorLogAdvancedQuery>(defaultIErrorLogAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => errorLogSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIErrorLogAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ToolBar, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' +t("ErrorLog"),
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
    } as ListsPartialViewProps<IErrorLogAdvancedQuery, IErrorLogDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

