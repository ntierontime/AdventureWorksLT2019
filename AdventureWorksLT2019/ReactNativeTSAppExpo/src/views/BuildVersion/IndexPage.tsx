import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { View, Text } from "react-native";

import { RootState } from '../../store/CombinedReducers';
// import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
// import { ListsPartialViewProps } from '../../shared/viewModels/ListsPartialViewProps';
// import { ListViewOptions } from '../../shared/views/ListViewOptions';

// import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from '../../slices/BuildVersionSlice';
import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery, } from '../../dataModels/IBuildVersionQueries';

export default function IndexPage() {
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => buildVersionSelectors.selectAll(state)
    );
    console.log("warning");
    return (
        // https://reactnative.dev/docs/view
        <View
            style={{
                flex: 1,
                justifyContent: "center",
                alignItems: "center",
                backgroundColor: "#3498db",
            }}
        >
            {/* https://reactnative.dev/docs/text */}
            <Text
                style={{
                    fontSize: 30,
                    fontWeight: "bold",
                    color: "#fff",
                    textTransform: "uppercase",
                }}
            >
                {t('AdvancedSearch')}
            </Text>
        </View>
    );
}