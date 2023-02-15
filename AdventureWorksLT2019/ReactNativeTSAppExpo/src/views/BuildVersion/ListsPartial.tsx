import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, } from 'react-redux';

import {
    Dimensions,
    SafeAreaView,
    View,
} from "react-native";

import { ActivityIndicator, FAB, MD2Colors } from 'react-native-paper';
import { Button, Portal, Dialog } from 'react-native-paper';

import { FontAwesome5 } from "@expo/vector-icons";
//import Modal from "react-native-modal";

import { AppDispatch } from '../../store/Store';
import { ListsPartialViewProps } from '../../shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from '../../shared/views/ListViewOptions';
import { ContainerOptions } from '../../shared/viewModels/ContainerOptions';
import { getCRUDItemPartialViewPropsOnDialog, ItemPartialViewProps } from '../../shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from '../../shared/viewModels/ViewItemTemplates';

import { IBuildVersionDataModel } from '../../dataModels/IBuildVersionDataModel';
import { search, bulkDelete } from '../../slices/BuildVersionSlice';
import { getBuildVersionQueryOrderBySettings, IBuildVersionAdvancedQuery, IBuildVersionIdentifier, getIBuildVersionIdentifier, compareIBuildVersionIdentifier } from '../../dataModels/IBuildVersionQueries';

// import AdvancedSearchPartial from './AdvancedSearchPartial';
// import CarouselPartial from './CarouselPartial';
import HtmlTablePartial from './HtmlTablePartial';
// import TilesPartial from './TilesPartial';
import ItemViewsPartial from './ItemViewsPartial';
import { useExampleTheme } from '../..';
import { useNavigation } from '@react-navigation/native';

export default function ListsPartial(props: ListsPartialViewProps<IBuildVersionAdvancedQuery, IBuildVersionDataModel>): JSX.Element {
    const navigation = useNavigation();
    const { isV3 } = useExampleTheme();
    const [visible, setVisible] = React.useState<boolean>(true);
    const [open, setOpen] = React.useState<boolean>(false);
    
    const { advancedQuery, setAdvancedQuery, defaultAdvancedQuery, listItems, initialLoadFromServer, hasListToolBar, listToolBarSetting, hasAdvancedSearch, addNewButtonContainer } = props;
    const rowCount = listItems.length;

    const dispatch = useDispatch<AppDispatch>();

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(ListViewOptions.Table);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getBuildVersionQueryOrderBySettings();
    const [itemsPerRow, setItemsPerRow] = useState<number>(3); // only for ListViewOptions.Tiles, should use MediaQuery(windows size)

    // 2. Bulk actions on Top Toolbar
    // 2.1.1. Top Toolbar - Select All Checkbox
    const [selected, setSelected] = useState<readonly IBuildVersionIdentifier[]>([]);
    const isSelected = (identifier: IBuildVersionIdentifier) => selected.findIndex(t => { return compareIBuildVersionIdentifier(identifier, t); }) !== -1;
    const numSelected = selected.length;

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.checked) {
            const newSelected = listItems.map((n) => getIBuildVersionIdentifier(n));
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };
    // 2.1.2. Selected/De-Select one item
    const handleSelectItemClick = (item: IBuildVersionDataModel) => {
        const selectedIndex = selected.findIndex(t => compareIBuildVersionIdentifier(t, item));
        let newSelected: readonly IBuildVersionIdentifier[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, getIBuildVersionIdentifier(item));
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1),
            );
        }

        setSelected(newSelected);
    };


    // 2.2. Top Toolbar - Delete Selected Rows/Items
    const handleDeleteSelected = () => {
        dispatch(bulkDelete(selected.map(t => t)));
        // console.log("handleDeleteSelected");
    };

    // 3.1. Top Toolbar - Advanced Search Dialog
    const [openAdvancedSearchDialog, setOpenAdvancedSearchDialog] = useState(false);
    const handleAdvancedSearchDialogOpen = () => {
        setOpenAdvancedSearchDialog(true);
    };

    const handleAdvancedSearchDialogClose = () => {
        setOpenAdvancedSearchDialog(false);
    };

    // 3.2. Top Toolbar - Advanced Search Inline - Collapse Panel 
    const [advancedSearchExpanded, setAdvancedSearchExpanded] = useState(false);
    const handleAdvancedSearchExpandClick = () => {
        setAdvancedSearchExpanded(!advancedSearchExpanded);
    };
    const handleAdvancedSearchExpandClose = () => {
        setAdvancedSearchExpanded(false);
    };

    const [openItemDialog, setOpenItemDialog] = useState(false);
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<IBuildVersionDataModel> | null>(null);
    const [currentItemOnDialog, setCurrentItemOnDialog] = useState<IBuildVersionDataModel>();
    const [currentItemIndex, setCurrentItemIndex] = useState<number>();

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, itemIndex: number | null) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IBuildVersionDataModel>(
            viewItemTemplate,
            handleItemDialogClose
        );
        setCRUDItemPartialViewProps(dialogProps);
        if (itemIndex !== null) {
            setCurrentItemIndex(itemIndex);
        }
        // handleItemActionsPopoverClose();
        setOpenItemDialog(true);
    };

    const handleItemDialogClose = () => {
        setOpenItemDialog(false);
        setCRUDItemPartialViewProps(null);
    };


    // 4. Bottom Toolbar - Pagination
    // 4.1. Bottom Toolbar - Pagination - Change Page
    const handlePaginationChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex = value;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 4.2. Bottom Toolbar - Pagination - Load More
    const handlePaginationLoadMore = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex++;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    useEffect(() => {
        if (initialLoadFromServer) {
            submitAdvancedSearch(advancedQuery);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const submitAdvancedSearch = (query: IBuildVersionAdvancedQuery) => {
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search({ ...query }))
                .then((result) => {
                    if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    }
                    else { // failed
                    }
                    //console.log(result);
                })
                .catch((error) => {  /*console.log(error);*/ })
                .finally(() => { setIsLoading(false); setAdvancedQuery(query); /*console.log('finally'); console.log(query);*/ });
        }
    };


    return (
        <>
            <View>
                {/* {hasListToolBar && renderEnhancedTopToolbar()}
                    {hasAdvancedSearch && <Collapse in={advancedSearchExpanded} timeout="auto" unmountOnExit>
                        <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchExpandClose(); }} />
                    </Collapse>} */}
                {/* {listViewOption === ListViewOptions.SlideShow && <CarouselPartial
                        listViewOption={ListViewOptions.SlideShow}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
						hasItemsSelect={hasListToolBar && (listToolBarSetting?.hasItemsSelect ?? false)}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationLoadMore}
                        handleSelectItemClick={handleSelectItemClick}
                        handleItemDialogOpen={handleItemDialogOpen}
                        currentItemOnDialog={currentItemOnDialog}
                        setCurrentItemOnDialog={setCurrentItemOnDialog}
                        currentItemIndex={currentItemIndex}
                        setCurrentItemIndex={setCurrentItemIndex}
                        isSelected={isSelected}
                    />} */}
                {listViewOption === ListViewOptions.Table &&
                    (!isLoading ? (<HtmlTablePartial
                        listViewOption={ListViewOptions.Table}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
                        hasItemsSelect={hasListToolBar && (listToolBarSetting?.hasItemsSelect ?? false)}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationChangePage}
                        handleSelectItemClick={handleSelectItemClick}
                        handleItemDialogOpen={handleItemDialogOpen}
                        currentItemOnDialog={currentItemOnDialog}
                        setCurrentItemOnDialog={setCurrentItemOnDialog}
                        currentItemIndex={currentItemIndex}
                        setCurrentItemIndex={setCurrentItemIndex}
                        isSelected={isSelected}
                    />)
                        : (
                            <ActivityIndicator animating={true} color={MD2Colors.red800} />
                        ))}
                {/* {listViewOption === ListViewOptions.Tiles && <TilesPartial
                        listViewOption={ListViewOptions.Tiles}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
						hasItemsSelect={hasListToolBar && (listToolBarSetting?.hasItemsSelect ?? false)}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationLoadMore}
                        handleSelectItemClick={handleSelectItemClick}
                        handleItemDialogOpen={handleItemDialogOpen}
                        currentItemOnDialog={currentItemOnDialog}
                        setCurrentItemOnDialog={setCurrentItemOnDialog}
                        currentItemIndex={currentItemIndex}
                        setCurrentItemIndex={setCurrentItemIndex}
                        isSelected={isSelected}
                    />} */}
            </View>
            {openItemDialog && currentItemOnDialog && <Portal>
                <Dialog visible={openItemDialog} dismissable={false} onDismiss={() => { handleItemDialogClose(); }} style={{ padding: 0, margin: 0, backgroundColor: 'rgba(0,0,0,0.5)' }}>
                    <ItemViewsPartial {...crudItemPartialViewProps}
                        item={currentItemOnDialog}
                        isItemSelected={!!currentItemOnDialog && isSelected(getIBuildVersionIdentifier(currentItemOnDialog))}
                        totalCountInList={listItems.length}
                        itemIndex={currentItemIndex}
                        setItemIndex={setCurrentItemIndex}
                        handleSelectItemClick={handleSelectItemClick} />
                </Dialog>
            </Portal>}
            {addNewButtonContainer === ContainerOptions.Absolute && <Portal>
                <FAB.Group
                    open={open}
                    icon={open ? 'dots-horizontal' : 'dots-vertical'}
                    actions={[
                        { icon: 'plus', label: 'Add Page', onPress: () => { navigation.navigate("BuildVersionCreatePage") } },
                        { icon: 'plus', label: 'Add Popup', onPress: () => { handleItemDialogOpen(ViewItemTemplates.Create, 0) } },
                    ]}
                    onStateChange={({ open }: { open: boolean }) => setOpen(open)}
                    onPress={() => {
                        if (open) {
                            // do something if the speed dial is open
                        }
                    }}
                    visible={visible}
                />
            </Portal>}
            {/* { {hasAdvancedSearch && <Dialog open={openAdvancedSearchDialog} fullWidth={true} maxWidth={'lg'}>
                <DialogContent>
                    <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchDialogClose(); }} />
                </DialogContent>
            </Dialog>} */}
        </>
    );
}
