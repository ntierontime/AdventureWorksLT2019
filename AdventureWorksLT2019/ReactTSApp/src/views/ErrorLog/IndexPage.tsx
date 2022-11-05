import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import { errorLogSelectors } from 'src/slices/ErrorLogSlice';
import { defaultIErrorLogAdvancedQuery, IErrorLogAdvancedQuery, } from 'src/dataModels/IErrorLogQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IErrorLogAdvancedQuery>(defaultIErrorLogAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => errorLogSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIErrorLogAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IErrorLogAdvancedQuery, IErrorLogDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

