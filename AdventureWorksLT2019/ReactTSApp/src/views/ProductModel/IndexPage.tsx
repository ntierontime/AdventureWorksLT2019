import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { productModelSelectors } from 'src/slices/ProductModelSlice';
import { defaultIProductModelAdvancedQuery, IProductModelAdvancedQuery, } from 'src/dataModels/IProductModelQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IProductModelAdvancedQuery>(defaultIProductModelAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productModelSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductModelAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IProductModelAdvancedQuery, IProductModelDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

