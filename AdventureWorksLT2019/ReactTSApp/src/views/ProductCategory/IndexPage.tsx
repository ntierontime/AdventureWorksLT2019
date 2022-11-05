import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { productCategorySelectors } from 'src/slices/ProductCategorySlice';
import { defaultIProductCategoryAdvancedQuery, IProductCategoryAdvancedQuery, } from 'src/dataModels/IProductCategoryQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IProductCategoryAdvancedQuery>(defaultIProductCategoryAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => productCategorySelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIProductCategoryAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IProductCategoryAdvancedQuery, IProductCategoryDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

