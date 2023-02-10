import {
    SafeAreaView,
    Text,
    View,
} from "react-native";

import { useTranslation } from 'react-i18next';
// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { getBuildVersionAvatar, IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { getRouteParamsOfIBuildVersionIdentifier } from 'src/dataModels/IBuildVersionQueries';

export default function DetailsPartial(props: ItemPartialViewProps<IBuildVersionDataModel>) {

    const { t } = useTranslation();

    return (
        <SafeAreaView>
            <View style={{ flex: 1 }}>
                <Text>versionDate</Text>
           </View>
        </SafeAreaView>
    );
}