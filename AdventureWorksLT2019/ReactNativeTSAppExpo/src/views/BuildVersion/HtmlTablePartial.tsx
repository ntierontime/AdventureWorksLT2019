import { useEffect, useMemo, useState } from 'react';
import { useSelector } from 'react-redux';

import {
    ActivityIndicator,
    FlatList,
    SafeAreaView,
    Text,
    TouchableOpacity,
    View,
} from "react-native";

import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { ListPartialViewProps } from '../../shared/viewModels/ListPartialViewProps';
import { QueryOrderDirections } from '../../shared/dataModels/QueryOrderDirections';
import { ViewItemTemplates } from '../../shared/viewModels/ViewItemTemplates';

import { getComparator, HeadCell, stableSort } from '../../shared/views/TableFeatures';

import { RootState } from '../../store/CombinedReducers';

import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { IBuildVersionIdentifier, getIBuildVersionIdentifier, getRouteParamsOfIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';

export default function HtmlTablePartial(props: ListPartialViewProps<IBuildVersionDataModel, IBuildVersionIdentifier>): JSX.Element {
    const { t } = useTranslation();

    // currentItemOnDialog is only used in page navigation, you can remove it if not-in-use.
    const { listItems, hasItemsSelect, numSelected, isSelected, handleChangePage, handleSelectItemClick, handleItemDialogOpen, currentItemOnDialog, setCurrentItemOnDialog, currentItemIndex, setCurrentItemIndex } = props;
    // const [order, setOrder] = useState<QueryOrderDirections>('asc');
    // const [orderBy, setOrderBy] = useState<keyof IBuildVersionDataModel>('versionDate');
    // const [dense, setDense] = useState(true);
    // const { pagination } = useSelector((state: RootState) => state.buildVersionList);
    // const [anchorElItemActions, setAnchorElItemActions] = useState<HTMLElement | null>(null);
    // const openPopoverItemActions = Boolean(anchorElItemActions);

    // const orderedListItems = useMemo(() => !!listItems ? stableSort(listItems, getComparator(order, orderBy)) as IBuildVersionDataModel[] : [], [listItems, order, orderBy]);

    // useEffect(() => {
    //     setCurrentItemOnDialog(!!orderedListItems && orderedListItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < orderedListItems.length ? orderedListItems[currentItemIndex] : null);
    // }, [currentItemIndex, orderedListItems, setCurrentItemOnDialog]);

    // const handleItemActionsPopoverOpen = (event: React.MouseEvent<HTMLElement>, thisIndex: number) => {
    //     event.stopPropagation();
    //     setAnchorElItemActions(event.currentTarget);
    //     setCurrentItemIndex(thisIndex);
    // };

    // const handleItemActionsPopoverClose = () => {
    //     setAnchorElItemActions(null);
    // };

    // // 2.1. Table Specific
    // // 2.1.2 Table Specific - Table Head Column Sort
    // const handleClientSideRequestSort = (
    //     event: React.MouseEvent<unknown>,
    //     property: keyof IBuildVersionDataModel,
    // ) => {
    //     const isAsc = orderBy === property && order === 'asc';
    //     setOrder(isAsc ? 'desc' : 'asc');
    //     setOrderBy(property);
    // };

    // // 3.2. Bottom Toolbar - Change Table Dense
    // const handleChangeTableDense = (event: React.ChangeEvent<HTMLInputElement>) => {
    //     setDense(event.target.checked);
    // };

    // const headCells: HeadCell[] = [

    //     {
    //         id: 'systemInformationID',
    //         numeric: true,
    //         disablePadding: true,
    //         label: t('SystemInformationID'),
    //     },
    //     {
    //         id: 'database_Version',
    //         numeric: false,
    //         disablePadding: true,
    //         label: t('Database_Version'),
    //     },
    //     {
    //         id: 'versionDate',
    //         numeric: false,
    //         disablePadding: true,
    //         label: t('VersionDate'),
    //     },
    //     {
    //         id: 'modifiedDate',
    //         numeric: false,
    //         disablePadding: true,
    //         label: t('ModifiedDate'),
    //     },
    // ];

    const [isModalVisible, setModalVisible] = useState(false);
    const [currentItem, setCurrentItem] = useState<IBuildVersionDataModel>(null);
    const onItemTouched = (item: IBuildVersionDataModel, index: number) => {
        handleItemDialogOpen(ViewItemTemplates.Details, index)
        console.log(item);
    }
    const closeModal = () => {
        setModalVisible(false);
    };

    return (
        <SafeAreaView>
            <FlatList
                data={listItems}
                renderItem={({ item, index }) => {
                    const pokemonTypes = item.database_Version;
                    return (
                        <TouchableOpacity
                            onPress={() => onItemTouched(item, index)}
                        >
                            <View key={index}>
                                <Text>{pokemonTypes}</Text>
                            </View>
                        </TouchableOpacity>
                    );
                }}
                keyExtractor={(item) => item.systemInformationID.toString()}
                initialNumToRender={10}
            />
        </SafeAreaView>
    );
}

