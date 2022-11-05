import { useState } from 'react';
import { useSelector } from 'react-redux';

import { RootState } from 'src/store/CombinedReducers';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from 'src/slices/BuildVersionSlice';
import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery, } from 'src/dataModels/IBuildVersionQueries';
import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => buildVersionSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIBuildVersionAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
        hasListToolBar : true,
        hasAdvancedSearch : true,
    } as ListsPartialViewProps<IBuildVersionAdvancedQuery, IBuildVersionDataModel>;

    return (
        <ListsPartial {...listsPartialViewProps} />
    );
}

