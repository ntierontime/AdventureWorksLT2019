import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, } from 'react-redux';
import { Box, Paper, Dialog, DialogContent, Collapse, Snackbar, ButtonGroup, IconButton } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

import { AppDispatch } from 'src/store/Store';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import ListToolBar, { ListToolBarProps } from 'src/shared/views/ListToolBar';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { getCRUDItemPartialViewPropsOnDialog, ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { search, bulkDelete } from 'src/slices/ProductModelProductDescriptionSlice';
import { getProductModelProductDescriptionQueryOrderBySettings, IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionIdentifier, getIProductModelProductDescriptionIdentifier, compareIProductModelProductDescriptionIdentifier } from 'src/dataModels/IProductModelProductDescriptionQueries';

import AdvancedSearchPartial from './AdvancedSearchPartial';
import CarouselPartial from './CarouselPartial';
import HtmlTablePartial from './HtmlTablePartial';
import TilesPartial from './TilesPartial';
import ItemViewsPartial from './ItemViewsPartial';

export default function ListsPartial(props: ListsPartialViewProps<IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionDataModel>): JSX.Element {
    const { advancedQuery, setAdvancedQuery, defaultAdvancedQuery, listItems, initialLoadFromServer, hasListToolBar, listToolBarSetting, hasAdvancedSearch, addNewButtonContainer } = props;
    const rowCount = listItems.length;

    const dispatch = useDispatch<AppDispatch>();

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(ListViewOptions.Table);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getProductModelProductDescriptionQueryOrderBySettings();
    const [itemsPerRow, setItemsPerRow] = useState<number>(3); // only for ListViewOptions.Tiles, should use MediaQuery(windows size)



    // 2. Bulk actions on Top Toolbar
    // 2.1.1. Top Toolbar - Select All Checkbox
    const [selected, setSelected] = useState<readonly IProductModelProductDescriptionIdentifier[]>([]);
    const isSelected = (identifier: IProductModelProductDescriptionIdentifier) => selected.findIndex(t => { return compareIProductModelProductDescriptionIdentifier(identifier, t); }) !== -1;
    const numSelected = selected.length;

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.checked) {
            const newSelected = listItems.map((n) => getIProductModelProductDescriptionIdentifier(n));
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };
    // 2.1.2. Selected/De-Select one item
    const handleSelectItemClick = (item: IProductModelProductDescriptionDataModel) => {
        const selectedIndex = selected.findIndex(t => compareIProductModelProductDescriptionIdentifier(t, item));
        let newSelected: readonly IProductModelProductDescriptionIdentifier[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, getIProductModelProductDescriptionIdentifier(item));
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
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<IProductModelProductDescriptionDataModel> | null>(null);
    const [currentItemOnDialog, setCurrentItemOnDialog] = useState<IProductModelProductDescriptionDataModel>();
    const [currentItemIndex, setCurrentItemIndex] = useState<number>();

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, itemIndex: number | null) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IProductModelProductDescriptionDataModel>(
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

    const submitAdvancedSearch = (query: IProductModelProductDescriptionAdvancedQuery) => {
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


    // Render.1. Top Toolbar
    const renderEnhancedTopToolbar = () => {
        const topToolbarProps = {

            ...listToolBarSetting,
            advancedQuery, defaultAdvancedQuery: { ...defaultAdvancedQuery }, setAdvancedQuery,
            rowCount,
            submitAdvancedSearch,

            setSelected,
            numSelected,
            handleSelectAllClick,

            handleDeleteSelected,

            listViewOption,
            setListViewOption,

            itemsPerRow,
            setItemsPerRow,

            serverOrderBys,

            advancedSearchExpanded,
            handleAdvancedSearchExpandClick,
            handleAdvancedSearchDialogOpen,

            hasAddNewButton: addNewButtonContainer === ContainerOptions.ListToolBar,
            handleAddNewClick: () => { handleItemDialogOpen(ViewItemTemplates.Create, -1); },
        } as ListToolBarProps<IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionIdentifier>;

        return <ListToolBar {...topToolbarProps} />
    }

    return (
        <>
            <Box sx={{ width: '100%' }}>
                <Paper sx={{ width: '100%', mb: 2 }}>
                    {hasListToolBar && renderEnhancedTopToolbar()}
                    {hasAdvancedSearch && <Collapse in={advancedSearchExpanded} timeout="auto" unmountOnExit>
                        <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchExpandClose(); }} />
                    </Collapse>}
                    {listViewOption === ListViewOptions.SlideShow && <CarouselPartial
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
                    />}
                    {listViewOption === ListViewOptions.Table && <HtmlTablePartial
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
                    />}
                    {listViewOption === ListViewOptions.Tiles && <TilesPartial
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
                    />}
                </Paper>
            </Box>
            {hasAdvancedSearch && <Dialog open={openAdvancedSearchDialog} fullWidth={true} maxWidth={'lg'}>
                <DialogContent>
                    <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchDialogClose(); }} />
                </DialogContent>
            </Dialog>}
            <Dialog open={openItemDialog} fullWidth={true} maxWidth={'lg'}>
                <ItemViewsPartial {...crudItemPartialViewProps} 
                    item={currentItemOnDialog} 
                    isItemSelected={!!currentItemOnDialog && isSelected(getIProductModelProductDescriptionIdentifier(currentItemOnDialog))} 
                    totalCountInList={listItems.length} 
                    itemIndex={currentItemIndex} 
                    setItemIndex={setCurrentItemIndex} 
                    handleSelectItemClick={handleSelectItemClick} />
            </Dialog>
            {addNewButtonContainer === ContainerOptions.Absolute && <Snackbar
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                open={true}
            >
                <ButtonGroup orientation='horizontal'>
                    <IconButton onClick={() => { handleItemDialogOpen(ViewItemTemplates.Create, -1); }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
                        <AddIcon />
                    </IconButton>
                </ButtonGroup>
            </Snackbar>}
		</>
    );
}

