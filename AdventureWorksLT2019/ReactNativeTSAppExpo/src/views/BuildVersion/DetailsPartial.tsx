import { Dimensions, ScrollView, StyleSheet, View } from 'react-native';

import { Avatar, Button, Card, Checkbox, Dialog, IconButton, MD2Colors, Paragraph, Text, } from 'react-native-paper';

import { useTranslation } from 'react-i18next';

// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from '../../i18n';
import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
import { CrudViewContainers } from '../../shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from '../../shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from '../../shared/viewModels/ViewItemTemplates';
import { getAvatarStyle, useExampleTheme } from '../../shared/views/ThemeRelated';

import { getBuildVersionAvatar, IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { getRouteParamsOfIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';
import { itemViewsStyles } from '../itemViewsStyles';

export default function DetailsPartial(props: ItemPartialViewProps<IBuildVersionDataModel>): JSX.Element {
    const theme = useExampleTheme();
    const { colors, isV3 } = useExampleTheme();
    const TextComponent = isV3 ? Text : Paragraph;

    // const navigate = useNavigate();
    const { scrollableCardContent, crudViewContainer, buttonContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const avatar = getBuildVersionAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    return (
        <Card style={itemViewsStyles.card}>
            <Card.Title
                title={item.database_Version}
                subtitle={item.versionDate}
                left={(props: any) => <Avatar.Text style={{ ...props.style, ...avatarStyle }} label={avatar} size={40} />}
                right={(props: any) => (
                    !!doneAction && crudViewContainer === CrudViewContainers.Dialog && <IconButton icon="close" onPress={() => { doneAction(); }} />
                )}
            />
            <ScrollView>
                <Card.Content style={{ maxHeight: 0.8 * Dimensions.get('window').height }}>
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
                </Card.Content>
            </ScrollView>
            {crudViewContainer === CrudViewContainers.Dialog && <Card.Actions>
                {!!previousAction && <IconButton icon="arrow-left" onPress={() => { previousAction(); }} />}
                {!!nextAction && <IconButton icon="arrow-right" onPress={() => { nextAction(); }} />}
            </Card.Actions>}
        </Card>
    );
}
