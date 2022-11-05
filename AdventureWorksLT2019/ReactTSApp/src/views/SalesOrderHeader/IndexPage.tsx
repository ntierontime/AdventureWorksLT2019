import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { salesOrderHeaderSelectors } from 'src/slices/SalesOrderHeaderSlice';
import { defaultISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderAdvancedQuery, } from 'src/dataModels/ISalesOrderHeaderQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<ISalesOrderHeaderAdvancedQuery>(defaultISalesOrderHeaderAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => salesOrderHeaderSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultISalesOrderHeaderAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

