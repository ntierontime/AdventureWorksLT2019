import { useEffect, useMemo, useState } from 'react';
import { useSelector } from 'react-redux';

import { List, Divider, Checkbox, Avatar, Switch } from 'react-native-paper';

import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { ListPartialViewProps } from '../../shared/viewModels/ListPartialViewProps';
import { QueryOrderDirections } from '../../shared/dataModels/QueryOrderDirections';
import { ViewItemTemplates } from '../../shared/viewModels/ViewItemTemplates';


import { getBuildVersionAvatar, IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { IBuildVersionIdentifier, getIBuildVersionIdentifier, getRouteParamsOfIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';

export default function HtmlTablePartial(props: ListPartialViewProps<IBuildVersionDataModel, IBuildVersionIdentifier>): JSX.Element {
    const { t } = useTranslation();

    // currentItemOnDialog is only used in page navigation, you can remove it if not-in-use.
    const { listItems, hasItemsSelect, numSelected, isSelected, handleChangePage, handleSelectItemClick, handleItemDialogOpen, currentItemOnDialog, setCurrentItemOnDialog, currentItemIndex, setCurrentItemIndex } = props;

    useEffect(() => {
        setCurrentItemOnDialog(!!listItems && listItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < listItems.length ? listItems[currentItemIndex] : null);
    }, [currentItemIndex, listItems, setCurrentItemOnDialog]);

    const onItemTouched = (item: IBuildVersionDataModel, index: number) => {
        handleItemDialogOpen(ViewItemTemplates.Details, index)
        console.log(item);
    }

    return (
        <List.Section>
            {listItems
                .map((row, index) => {
                    return (
                        <List.Item
                            key={row.database_Version}
                            title={row.database_Version}
                            left={(props) => (
                                <Avatar.Text style={props.style} label={getBuildVersionAvatar(row)} size={40} />
                            )}
                            onPress={() => onItemTouched(row, index)}
                        />
                    )
                })}
        </List.Section>
    );
}
