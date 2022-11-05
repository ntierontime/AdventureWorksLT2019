import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductDataModel } from 'src/dataModels/IProductDataModel';
import { productSelectors } from 'src/slices/ProductSlice';
import { defaultIProductAdvancedQuery, IProductAdvancedQuery, } from 'src/dataModels/IProductQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IProductAdvancedQuery>(defaultIProductAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IProductAdvancedQuery, IProductDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

