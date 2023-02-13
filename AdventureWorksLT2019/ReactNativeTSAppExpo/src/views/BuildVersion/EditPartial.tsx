import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Dimensions, ScrollView, StyleSheet, View } from 'react-native';

import { Avatar, Button, Card, Checkbox, Dialog, IconButton, MD2Colors, Paragraph, Text, } from 'react-native-paper';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from '../../i18n';
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
    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    const [versionDate, setVersionDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);


    const onSubmit = (data: IBuildVersionDataModel) => {
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
                    <TextComponent>{t('SystemInformationID')}</TextComponent>
                    <TextComponent>{item.systemInformationID}</TextComponent>
                </View>
                <View style={itemViewsStyles.row}>
                    <TextComponent>{t('ModifiedDate')}</TextComponent>
                    <TextComponent>{t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TextComponent>
                </View>
                <View style={itemViewsStyles.row}>
                    <TextComponent>{t('IsDeleted')}</TextComponent>
                    <Checkbox status={item.isDeleted______ ? "checked" : "unchecked"} disabled />
                </View>
            </>
        );
    }

    return (
        <form noValidate onSubmit={handleSubmit(onSubmit)}>
        <Card style={itemViewsStyles.card} >
            {/* <Card.Title
                title={item.database_Version}
                subtitle={item.versionDate}
                left={(props: any) => <Avatar.Text style={{ ...props.style, ...avatarStyle }} label={avatar} size={40} />}
                right={(props: any) => (
                    hasCloseButton && <IconButton icon="close" onPress={() => { doneAction(); }} />
                )}
            /> */}
            <ScrollView>
                <Card.Content style={{ maxHeight: 0.8 * Dimensions.get('window').height }}>
                    {renderItemView()}
                </Card.Content>
            </ScrollView>
            {crudViewContainer === CrudViewContainers.Dialog && <Card.Actions>
                {!!previousAction && <IconButton icon="arrow-left" onPress={() => { previousAction(); }} />}
                {!!nextAction && <IconButton icon="arrow-right" onPress={() => { nextAction(); }} />}
            </Card.Actions>}
        </Card>
        </form>
    );
}
