import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { customerAddressSelectors } from 'src/slices/CustomerAddressSlice';
import { defaultICustomerAddressAdvancedQuery, ICustomerAddressAdvancedQuery, } from 'src/dataModels/ICustomerAddressQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<ICustomerAddressAdvancedQuery>(defaultICustomerAddressAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => customerAddressSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultICustomerAddressAdvancedQuery(),
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
    } as ListsPartialViewProps<ICustomerAddressAdvancedQuery, ICustomerAddressDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

