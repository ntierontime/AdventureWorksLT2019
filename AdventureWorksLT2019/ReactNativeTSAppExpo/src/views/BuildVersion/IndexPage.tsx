import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import {
    ActivityIndicator,
    FlatList,
    SafeAreaView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";
import { ListItem, SearchBar } from "react-native-elements";
import Modal from "react-native-modal";

import { AppDispatch } from 'src/store/Store';
import { RootState } from '../../store/CombinedReducers';
import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from '../../shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from '../../shared/views/ListViewOptions';

import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from '../../slices/BuildVersionSlice';
import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery, } from '../../dataModels/IBuildVersionQueries';
import { search } from '../../slices/BuildVersionSlice';

import ListsPartial from './ListsPartial';

export default function IndexPage() {
    // const { t } = useTranslation();
    // const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    // const listItems = useSelector(
    //     (state: RootState) => buildVersionSelectors.selectAll(state)
    // );
    // const dispatch = useDispatch<AppDispatch>();
    // const [isLoading, setIsLoading] = useState<boolean>(false);

    // useEffect(() => {
    //     submitAdvancedSearch(advancedQuery);
    //     // eslint-disable-next-line react-hooks/exhaustive-deps
    // }, []);

    // const submitAdvancedSearch = (query: IBuildVersionAdvancedQuery) => {
    //     console.log(query);
    //     if (!isLoading) {
    //         setIsLoading(true);
    //         dispatch(search({ ...query }))
    //             .then((result) => {
    //                 if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
    //                 }
    //                 else { // failed
    //                 }
    //                 // console.log(result);
    //             })
    //             .catch((error) => {  /*console.log(error);*/ })
    //             .finally(() => { setIsLoading(false); setAdvancedQuery(query); /* console.log('finally'); console.log(query); */ });
    //     }
    // };

    // const [isModalVisible, setModalVisible] = useState(false);
    // const [currentItem, setCurrentItem] = useState<IBuildVersionDataModel>(null);
    // const onItemTouched = (item: IBuildVersionDataModel, index: number) => {
    //     setModalVisible(true);
    //     setCurrentItem(item);
    //     console.log(item);
    // }
    // const closeModal = () => {
    //     setModalVisible(false);
    // };
    
    // const searchBar_onChangeText = (text?: string) => {
    //     const query = { ...advancedQuery, textSearch: text };
    //     submitAdvancedSearch(query)
    // }

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
        <ListsPartial {...listsPartialViewProps} />
        // <SafeAreaView>
        //     <View style={{ flex: 1 }}>
        //         <SearchBar
        //             placeholder="Find Move by name ..."
        //             inputContainerStyle={{ backgroundColor: "#e9e9e9" }}
        //             containerStyle={{ backgroundColor: "transparent" }}
        //             lightTheme={true}
        //             round={true}
        //             value={advancedQuery.textSearch}
        //             onChangeText={searchBar_onChangeText} onBlur={undefined} onFocus={undefined} platform={'default'} clearIcon={undefined} searchIcon={undefined} loadingProps={undefined} showLoading={false} onClear={undefined} onCancel={undefined} cancelButtonTitle={''} cancelButtonProps={undefined} showCancel={false}                />
        //         {!isLoading ? (
        //             <FlatList
        //                 data={listItems}
        //                 renderItem={({ item, index }) => {
        //                     const pokemonTypes = item.database_Version;
        //                     return (
        //                         <TouchableOpacity
        //                             onPress={() => onItemTouched(item, index)}
        //                         >
        //                             <View key={index}>
        //                                 <Text>{pokemonTypes}</Text>
        //                             </View>
        //                         </TouchableOpacity>
        //                     );
        //                 }}
        //                 keyExtractor={(item) => item.systemInformationID.toString()}
        //                 initialNumToRender={10}
        //             />
        //         ) : (
        //             <ActivityIndicator animating size="large" />
        //         )}
        //         <Modal
        //             isVisible={isModalVisible}
        //             hasBackdrop={false}
        //             onBackButtonPress={closeModal}
        //             style={{ margin: 0 }}
        //         >
        //             {currentItem !== null
        //                 ? (<SafeAreaView>
        //                     <View>
        //                         <Text>versionDate</Text>
        //                         <Text>{currentItem.modifiedDate}</Text>
        //                     </View>
        //                 </SafeAreaView>)
        //                 : (
        //                     <ActivityIndicator animating size="large" />
        //                 )}
        //         </Modal>
        //     </View>
        // </SafeAreaView>
    );
}