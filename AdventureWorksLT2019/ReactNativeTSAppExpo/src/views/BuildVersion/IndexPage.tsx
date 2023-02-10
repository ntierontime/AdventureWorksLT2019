import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import {
    SafeAreaView,
    Text,
    View,
} from "react-native";

import { RootState } from '../../store/CombinedReducers';
import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from '../../shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from '../../shared/views/ListViewOptions';

import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from '../../slices/BuildVersionSlice';
import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery, } from '../../dataModels/IBuildVersionQueries';

import ListsPartial from './ListsPartial';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => buildVersionSelectors.selectAll(state)
    );

    const listsPartialViewProps = {
        advancedQuery, setAdvancedQuery,
		defaultAdvancedQuery: defaultIBuildVersionAdvancedQuery(),
        listItems,
        initialLoadFromServer: true,
        hasListToolBar : true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.Absolute, // at bottom-right using SnackBar
        listToolBarSetting: {
            textSearchPlaceHolder: t("Search") + ' ' + t("BuildVersion"),
            hasListViewOptionsSelect: true,
	        availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
            hasItemsSelect: true,
            hasBulkDelete: true,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: false,
            hasAdvancedSearchDialog: true,
        }
    } as ListsPartialViewProps<IBuildVersionAdvancedQuery, IBuildVersionDataModel>;

    return (
        <SafeAreaView>
            <View style={{ flex: 1 }}>
                <Text>versionDate</Text>
           </View>
        </SafeAreaView>
   );

    // return (
    //      <ListsPartial {...listsPartialViewProps} />
    // );
}