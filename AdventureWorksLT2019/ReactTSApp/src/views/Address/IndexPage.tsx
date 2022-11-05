import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { addressSelectors } from 'src/slices/AddressSlice';
import { defaultIAddressAdvancedQuery, IAddressAdvancedQuery, } from 'src/dataModels/IAddressQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IAddressAdvancedQuery>(defaultIAddressAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => addressSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIAddressAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IAddressAdvancedQuery, IAddressDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

