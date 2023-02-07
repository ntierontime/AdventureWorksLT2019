import { useEffect,  } from 'react';

import {
    ActivityIndicator,
    FlatList,
    SafeAreaView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { AdvancedSearchPartialViewProps } from 'src/shared/viewModels/AdvancedSearchPartialViewProps';

// import { SearchDateTimeRangeField } from 'src/shared/views/SearchDateTimeRangeField';
// import { SearchStringTextField } from 'src/shared/views/SearchStringTextField';
import { getDateRange } from 'src/shared/dataModels/PreDefinedDateTimeRanges';


import { defaultIBuildVersionAdvancedQuery, IBuildVersionAdvancedQuery } from 'src/dataModels/IBuildVersionQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<IBuildVersionAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();



    useEffect(() => {

        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultIBuildVersionAdvancedQuery(),
    },);


    const handleVersionDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('versionDateRangeLower', newRange.lowerBound);
        setValue('versionDateRangeUpper', newRange.upperBound);
    }
    const handleModifiedDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('modifiedDateRangeLower', newRange.lowerBound);
        setValue('modifiedDateRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: IBuildVersionAdvancedQuery) => {
        submitAction({ ...query });
        doneAction();
    }

    return (
        <SafeAreaView>
            <View style={{ flex: 1 }}>
                <Text>versionDate</Text>
           </View>
        </SafeAreaView>
    );
}
