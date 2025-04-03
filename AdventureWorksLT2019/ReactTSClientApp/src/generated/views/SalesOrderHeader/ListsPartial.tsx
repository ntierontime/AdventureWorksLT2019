import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, } from 'react-redux';
import { Box, Paper, Dialog, DialogContent, Collapse, Snackbar, ButtonGroup, IconButton } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

import { useTranslation } from 'react-i18next';

import { AppDispatch } from 'src/store/Store';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import ListToolBar, { ListToolBarProps } from 'src/shared/views/ListToolBar';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { ButtonTypes } from 'src/shared/views/buttonGroups/ButtonTypes';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { search } from 'src/generated/slices/SalesOrderHeaderSlice';
import { getSalesOrderHeaderQueryOrderBySettings, ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';

import AdvancedSearchPartial from './AdvancedSearchPartial';
import HtmlTablePartial from './HtmlTablePartial';
import ItemViewsPartial from './ItemViewsPartial';

export default function ListsPartial(props: ListsPartialViewProps<ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderDataModel>): JSX.Element {
    const { 
        currentListViewOption
        , advancedQuery, setAdvancedQuery, defaultAdvancedQuery
        , listItems, initialLoadFromServer
        , hasAdvancedSearch, addNewButtonContainer
        , hasListToolBar, listToolBarSetting
        , itemPopupGridColumns //by default how to display editors/inputs in mui Grid system.
        , itemPopupScrollableCardContent
     } = props;
    const rowCount = listItems.length;
    const { t } = useTranslation();

    const dispatch = useDispatch<AppDispatch>();

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(currentListViewOption);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getSalesOrderHeaderQueryOrderBySettings();



    // 3.2. Top Toolbar - Advanced Search Inline - Collapse Panel 
    const [advancedSearchExpanded, setAdvancedSearchExpanded] = useState(false);
    const handleAdvancedSearchExpandClick = () => {
        setAdvancedSearchExpanded(!advancedSearchExpanded);
    };
    const handleAdvancedSearchExpandClose = () => {
        setAdvancedSearchExpanded(false);
    };

	const [openItemDialog, setOpenItemDialog] = useState(false);
    const [currentItemOnDialog, setCurrentItemOnDialog] = useState<ISalesOrderHeaderDataModel>();
    const [currentItemIndex, setCurrentItemIndex] = useState<number>();
    const [currentViewItemTemplate, setCurrentViewItemTemplate] = useState<ViewItemTemplates>();

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, itemIndex: number | null) => {
        setCurrentViewItemTemplate(viewItemTemplate);
        if (itemIndex !== null) {
            setCurrentItemIndex(itemIndex);
        }
        // handleItemActionsPopoverClose();
        setOpenItemDialog(true);
    };

    const handleItemDialogClose = () => {
        setOpenItemDialog(false);
    };


    // 4. Bottom Toolbar - Pagination
    // 4.1. Bottom Toolbar - Pagination - Change Page
    const handlePaginationChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex = value;
        if (!isLoading) {
            submitAdvancedSearch(advancedQuery);
        }
    };

    useEffect(() => {
        if (initialLoadFromServer) {
            submitAdvancedSearch(advancedQuery);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [advancedQuery]);


    const searchMethod = search;
    const submitAdvancedSearch = (query: ISalesOrderHeaderAdvancedQuery) => {
        if (!isLoading) {
            setIsLoading(true);
            dispatch(searchMethod({ ...query }))
                .then((result) => {
                    if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    }
                    else { // failed
                    }
                    // console.log(result);
                })
                .catch((error) => { })
                .finally(() => { setIsLoading(false); setAdvancedQuery(query); });
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
            handleAdvancedSearchDialogOpen: null,

            hasAddNewButton: addNewButtonContainer !== ContainerOptions.None,
            handleAddNewClick: () => { handleItemDialogOpen(ViewItemTemplates.Create, -1); },
        } as ListToolBarProps<ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderIdentifier>;

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
                        isSelected={(identifier: ISalesOrderHeaderIdentifier) => { return false; }}
                    />}
                </Paper>
            </Box>
            <Dialog open={openItemDialog} fullWidth={true} maxWidth={'lg'}>
                <ItemViewsPartial
                    mainButtonContainer={CardButtonGroupPosition.Bottom}
                    mainButtonType={ButtonTypes.IconAndText}
                    crudViewContainer={CrudViewContainers.Dialog}
                    viewItemTemplate={currentViewItemTemplate}
                    doneAction={handleItemDialogClose}
					handleItemDialogOpen={handleItemDialogOpen}

                    item={currentItemOnDialog} 
                    totalCountInList={listItems.length} 
                    itemIndex={currentItemIndex} 
                    setItemIndex={setCurrentItemIndex} 

                    isItemSelected={false} 
                    handleSelectItemClick={null}
                    gridColumns={itemPopupGridColumns}
                    scrollableCardContent={itemPopupScrollableCardContent}
                    />
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

