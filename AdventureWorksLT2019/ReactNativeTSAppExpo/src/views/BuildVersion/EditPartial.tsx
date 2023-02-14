import { useEffect, useState, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import { Dimensions, ScrollView, View } from 'react-native';

import { Avatar, Button, Card, Checkbox, Dialog, HelperText, IconButton, MD2Colors, MD3Colors, Paragraph, Switch, Text, TextInput, } from 'react-native-paper';

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

import { getBuildVersionAvatar, IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { getRouteParamsOfIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';
import { itemViewsStyles } from '../itemViewsStyles';
import { put } from '../../slices/BuildVersionSlice';
import { DatePickerInput, DatePickerModal } from 'react-native-paper-dates';

export default function EditPartial(props: ItemPartialViewProps<IBuildVersionDataModel>): JSX.Element {
    const theme = useExampleTheme();
    const { colors, isV3 } = useExampleTheme();
    const TextComponent = isV3 ? Text : Paragraph;

    const { scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, getValues, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm<IBuildVersionDataModel>({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    // const [versionDate, setVersionDate] = useState<string>();
    const [openVersionDateModal, setOpenVersionDateModal] = useState(false)
    const onDismissVersionDate = useCallback(() => {
        setOpenVersionDateModal(false)
    }, [setOpenVersionDateModal])

    const onConfirmVersionDate = useCallback(
        (params: any) => {
            setOpenVersionDateModal(false)
            setValue('versionDate', params.date)
        },
        [setOpenVersionDateModal, setValue]
    )

    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);


    const onSubmit = (data: IBuildVersionDataModel) => {
        console.log(data);

        setSaving(true);
        dispatch(put({ identifier: { systemInformationID: data.systemInformationID, versionDate: data.versionDate, modifiedDate: data.modifiedDate }, data: { ...data } }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const avatar = getBuildVersionAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

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
                title={item.database_Version}
                subtitle={item.versionDate}
                left={(props: any) => <Avatar.Text style={{ ...props.style, ...avatarStyle }} label={avatar} size={40} />}
                right={(props: any) => (
                    hasCloseButton && <IconButton icon="close" onPress={() => { doneAction(); }} />
                )}
            />
            <ScrollView>
                <Card.Content style={{ maxHeight: 0.8 * Dimensions.get('window').height }}>
                    {renderItemView()}
                </Card.Content>
            </ScrollView>
            {crudViewContainer === CrudViewContainers.Dialog && <Card.Actions>
                {!!previousAction && <IconButton icon="arrow-left" onPress={() => { previousAction(); }} />}
                {!!nextAction && <IconButton icon="arrow-right" onPress={() => { nextAction(); }} />}
                <IconButton icon="check" onPress={handleSubmit(onSubmit)} />
            </Card.Actions>}
            {crudViewContainer === CrudViewContainers.StandaloneView && <Card.Actions>
                <IconButton icon="check" onPress={handleSubmit(onSubmit)} />
            </Card.Actions>}
        </Card>
    );
}
