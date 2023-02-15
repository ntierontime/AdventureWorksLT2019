import { useEffect, useState, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import { Dimensions, ScrollView, View } from 'react-native';

import { Avatar, Button, Card, Checkbox, Chip, Dialog, HelperText, IconButton, MD2Colors, MD3Colors, Paragraph, Switch, Text, TextInput, } from 'react-native-paper';

import { useForm, Controller, SubmitHandler } from "react-hook-form"
import { useTranslation } from 'react-i18next';

// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import i18n, { /*getCurrency,*/ i18nFormats } from '../../i18n';
import { AppDispatch } from '../../store/Store';

import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
import { CrudViewContainers } from '../../shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from '../../shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from '../../shared/viewModels/ViewItemTemplates';
import { getAvatarStyle, useExampleTheme } from '../../shared/views/ThemeRelated';

import { defaultBuildVersion, IBuildVersionDataModel, buildVersionFormValidationWhenCreate } from '../../dataModels/IBuildVersionDataModel';
import { getRouteParamsOfIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';
import { itemViewsStyles } from '../itemViewsStyles';
import { post } from '../../slices/BuildVersionSlice';
import { DatePickerInput, DatePickerModal } from 'react-native-paper-dates';

export default function EditPartial(props: ItemPartialViewProps<IBuildVersionDataModel>): JSX.Element {
    const theme = useExampleTheme();
    const { colors, isV3 } = useExampleTheme();
    const TextComponent = isV3 ? Text : Paragraph;

    const { scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, setValue, handleSubmit, reset, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultBuildVersion(),
    });

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };

    useEffect(() => {

        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        reset(item);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const onSubmit = () => {
        setCreating(true);
        dispatch(post({ ...item }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
                        reset(item);
                    }
                    else {
                        setCreateMessage(t('SuccessfullySaved'));
                        setCreated(true);
                    }
                }
                else { // failed
                    setCreateMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }

    const hasCloseButton = !!doneAction && crudViewContainer === CrudViewContainers.Dialog;

    const renderItemView = () => {
        return (
            <>
                <View style={itemViewsStyles.row}>
                    <Controller
                        control={control}
                        defaultValue={item.systemInformationID}
                        name="systemInformationID"
                        render={({ field: { onChange, onBlur, value } }) => (
                            <>
                                <TextInput
                                    label={t('SystemInformationID')}
                                    style={itemViewsStyles.input}
                                    numeric
                                    value={value}
                                    onBlur={onBlur}
                                    onChangeText={(value) => onChange(value)}
                                />
                                <HelperText type="error">{errors.systemInformationID?.message}</HelperText>
                            </>
                        )}
                    />
                </View>
                <View style={itemViewsStyles.row}>
                    <Controller
                        control={control}
                        defaultValue={item.modifiedDate}
                        name="modifiedDate"
                        render={({ field: { onChange, onBlur, value } }) => (
                            <>
                                <TextInput
                                    label={t('ModifiedDate')}
                                    style={itemViewsStyles.input}
                                    value={value}
                                    onBlur={onBlur}
                                    onChangeText={(value) => onChange(value)}
                                />
                                <HelperText type="error">{errors.modifiedDate?.message}</HelperText>
                            </>
                        )}
                    />
                </View>
                <View style={itemViewsStyles.row}>
                    <Controller
                        control={control}
                        defaultValue={t(i18nFormats.dateTime.format, { val: item.versionDate, formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                        name="versionDate"
                        render={({ field: { onChange, onBlur, value } }) => (
                            <DatePickerInput
                                locale={i18n.language}
                                label={t('VersionDate')}
                                value={new Date(value)}
                                onChange={(d) => setValue('versionDate', d.toISOString())}
                                inputMode="start"
                                autoComplete="birthdate-full"
                            // mode="outlined" (see react-native-paper docs)
                            // other react native TextInput props
                            />
                        )}
                    />
                </View>
                <View style={itemViewsStyles.row}>
                    <TextComponent>{t('IsDeleted')}</TextComponent>
                    <Controller
                        control={control}
                        defaultValue={item.isDeleted______}
                        name="isDeleted______"
                        render={({ field: { onChange, onBlur, value } }) => (
                            <Switch
                                value={value}
                                color={isV3 ? MD3Colors.tertiary50 : MD2Colors.blue500}
                                onValueChange={(value) => onChange(value)}
                            />
                        )}
                    />
                </View>
            </>
        );
    }

    return (
        <Card style={itemViewsStyles.card} >
            <Card.Title
                title={t("Create_New")}
                subtitle={t("BuildVersion")}
                right={(props: any) => (
                    hasCloseButton && <IconButton icon="close" onPress={() => { doneAction(); }} />
                )}
            />
            {!!createMessage && <Card.Content>
                    {createMessage + " "}
            </Card.Content>}            
            <ScrollView>
                <Card.Content style={{ maxHeight: 0.8 * Dimensions.get('window').height }}>
                    {renderItemView()}
                </Card.Content>
            </ScrollView>
            <View style={itemViewsStyles.row}>
                <Chip
                    mode="outlined"
                    icon={ createAnother ? "checkbox-marked" : "square-rounded-outline" }
                    onPress={() => { setCreateAnother(!createAnother) }}
                    selected={createAnother}
                    showSelectedOverlay={createAnother}
                >
                    {t('CreateAnotherOne')}
                </Chip>
                <IconButton icon="check" onPress={handleSubmit(onSubmit)} />
            </View>
        </Card>
    );
}
