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

import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { search } from 'src/slices/ProductDescriptionSlice';
import { getProductDescriptionQueryOrderBySettings, IProductDescriptionAdvancedQuery, IProductDescriptionIdentifier } from 'src/dataModels/IProductDescriptionQueries';

import AdvancedSearchPartial from './AdvancedSearchPartial';
import HtmlTablePartial from './HtmlTablePartial';
import ItemViewsPartial from './ItemViewsPartial';

export default function ListsPartial(props: ListsPartialViewProps<IProductDescriptionAdvancedQuery, IProductDescriptionDataModel>): JSX.Element {
    const { advancedQuery, setAdvancedQuery, defaultAdvancedQuery, listItems, initialLoadFromServer, hasListToolBar, listToolBarSetting, hasAdvancedSearch, addNewButtonContainer } = props;
    const rowCount = listItems.length;

    const dispatch = useDispatch<AppDispatch>();

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(ListViewOptions.Table);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getProductDescriptionQueryOrderBySettings();



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
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<IProductDescriptionDataModel> | null>(null);
    const [currentItemOnDialog, setCurrentItemOnDialog] = useState<IProductDescriptionDataModel>();
    const [currentItemIndex, setCurrentItemIndex] = useState<number>();

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, itemIndex: number | null) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IProductDescriptionDataModel>(
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

    useEffect(() => {
        if (initialLoadFromServer) {
            submitAdvancedSearch(advancedQuery);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const submitAdvancedSearch = (query: IProductDescriptionAdvancedQuery) => {
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

            setSelected: null,
            numSelected: 0,
            handleSelectAllClick: null,

            handleDeleteSelected: null,

            listViewOption,
            setListViewOption,

            itemsPerRow: 1,
            setItemsPerRow: null,

            serverOrderBys,

            advancedSearchExpanded,
            handleAdvancedSearchExpandClick,
            handleAdvancedSearchDialogOpen,

            hasAddNewButton: addNewButtonContainer === ContainerOptions.ListToolBar,
            handleAddNewClick: () => { handleItemDialogOpen(ViewItemTemplates.Create, -1); },
        } as ListToolBarProps<IProductDescriptionAdvancedQuery, IProductDescriptionIdentifier>;

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
                    {listViewOption === ListViewOptions.Table && <HtmlTablePartial
                        listViewOption={ListViewOptions.Table}
                        listItems={listItems}
                        itemsPerRow={1}
						hasItemsSelect={false}
                        numSelected={0}
                        selected={[]}
                        handleChangePage={handlePaginationChangePage}
                        handleSelectItemClick={null}
                        handleItemDialogOpen={handleItemDialogOpen}
                        currentItemOnDialog={currentItemOnDialog}
                        setCurrentItemOnDialog={setCurrentItemOnDialog}
                        currentItemIndex={currentItemIndex}
                        setCurrentItemIndex={setCurrentItemIndex}
                        isSelected={(identifier: IProductDescriptionIdentifier) => { return false; }}
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
                    isItemSelected={false} 
                    totalCountInList={listItems.length} 
                    itemIndex={currentItemIndex} 
                    setItemIndex={setCurrentItemIndex} 
                    handleSelectItemClick={null} />
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

