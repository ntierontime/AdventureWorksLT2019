import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import {
    ActivityIndicator,
    FlatList,
    Text,
    View,
} from "react-native";
import { ListItem, SearchBar } from "react-native-elements";

import { AppDispatch } from 'src/store/Store';
import { RootState } from '../../store/CombinedReducers';
// import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
// import { ListsPartialViewProps } from '../../shared/viewModels/ListsPartialViewProps';
// import { ListViewOptions } from '../../shared/views/ListViewOptions';

import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from '../../slices/BuildVersionSlice';
import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery, } from '../../dataModels/IBuildVersionQueries';
import { search } from '../../slices/BuildVersionSlice';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => buildVersionSelectors.selectAll(state)
    );
    const dispatch = useDispatch<AppDispatch>();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    useEffect(() => {
        submitAdvancedSearch(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const submitAdvancedSearch = (query: IBuildVersionAdvancedQuery) => {
        console.log(query);
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search({ ...query }))
                .then((result) => {
                    if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    }
                    else { // failed
                    }
                    // console.log(result);
                })
                .catch((error) => {  /*console.log(error);*/ })
                .finally(() => { setIsLoading(false); setAdvancedQuery(query); /* console.log('finally'); console.log(query); */ });
        }
    };

    const searchBar_onChangeText = (text: string) => {
        const query = {...advancedQuery, textSearch: text};
        submitAdvancedSearch(query)
      };

    return (
        <View style={{ flex: 1 }}>
            <SearchBar
                placeholder="Find Move by name ..."
                inputContainerStyle={{ backgroundColor: "#e9e9e9" }}
                containerStyle={{ backgroundColor: "transparent" }}
                lightTheme={true}
                round={true}
                value={advancedQuery.textSearch}
                onChangeText={searchBar_onChangeText}
            />

            {!isLoading ? (
                <FlatList
                    data={listItems}
                    renderItem={({ item, index }) => {
                        const pokemonTypes = item.database_Version;
                        return (
                            <View key={index}>
                                <Text>{pokemonTypes}</Text>
                            </View>
                        );
                    }}
                    keyExtractor={(item) => item.systemInformationID.toString()}
                    initialNumToRender={10}
                />
            ) : (
                <ActivityIndicator animating size="large" />
            )}
        </View>
    );
}