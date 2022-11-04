import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Box,Paper, Dialog, DialogContent, Collapse } from '@mui/material';

import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { AppDispatch } from 'src/store/Store';
import ListToolBar, { ListToolBarProps } from 'src/shared/views/ListToolBar';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { addressSelectors, search, bulkDelete } from 'src/slices/AddressSlice';
import { defaultIAddressAdvancedQuery, getAddressQueryOrderBySettings, IAddressAdvancedQuery, IAddressIdentifier, getIAddressIdentifier, compareIAddressIdentifier } from 'src/dataModels/IAddressQueries';

import AdvancedSearchPartial from './AdvancedSearchPartial'
import CarouselPartial from './CarouselPartial'
import HtmlTablePartial from './HtmlTablePartial'
import TilesPartial from './TilesPartial'

export default function IndexPage() {
    const dispatch = useDispatch<AppDispatch>();
    const { t } = useTranslation();
    const listItems = useSelector(
        (state: RootState) => addressSelectors.selectAll(state)
    );

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(ListViewOptions.Table);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getAddressQueryOrderBySettings();
    const [advancedQuery, setAdvancedQuery] = useState<IAddressAdvancedQuery>(defaultIAddressAdvancedQuery());
    const [selected, setSelected] = useState<readonly IAddressIdentifier[]>([]);
    const [itemsPerRow, setItemsPerRow] = useState<number>(3); // only for ListViewOptions.Tiles, should use MediaQuery(windows size)

    useEffect(() => {
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    // 1.7.1. Top Toolbar - Submit Advanced Search Action
    const submitAdvancedSearch = (query: IAddressAdvancedQuery) => {
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

    // 1. Top Toolbar
    // 1.1. Top Toolbar - Select All Checkbox
    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.checked) {
            const newSelected = listItems.map((n) => getIAddressIdentifier(n));
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };

    // 1.2. Top Toolbar - Delete Selected Rows/Items
    const handleDeleteSelected = () => {
        dispatch(bulkDelete(selected.map(t=>t)));
        // console.log("handleDeleteSelected");
    };

    // 1.7.2. Top Toolbar - Advanced Search Dialog
    const [openAdvancedSearchDialog, setOpenAdvancedSearchDialog] = useState(false);
    const handleAdvancedSearchDialogOpen = () => {
        setOpenAdvancedSearchDialog(true);
    };

    const handleAdvancedSearchDialogClose = () => {
        setOpenAdvancedSearchDialog(false);
    };
    // 1.7.3. Top Toolbar - Advanced Search Collapse Panel
    const [advancedSearchExpanded, setAdvancedSearchExpanded] = useState(false);
    const handleAdvancedSearchExpandClick = () => {
        setAdvancedSearchExpanded(!advancedSearchExpanded);
    };
    const handleAdvancedSearchExpandClose = () => {
        setAdvancedSearchExpanded(false);
    };

    // 2. Selected/De-Select one item
    const handleSelectItemClick = (item: IAddressDataModel) => {
        const selectedIndex = selected.findIndex(t => compareIAddressIdentifier(t, item));
        let newSelected: readonly IAddressIdentifier[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, getIAddressIdentifier(item));
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

    // 3. Bottom Toolbar
    // 3.1. Bottom Toolbar - Pagination - Change Page
    const handlePaginationChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex = value;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 3.2. Bottom Toolbar - Pagination - Load More
    const handlePaginationLoadMore = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex++;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    const numSelected = selected.length;
    const rowCount = listItems.length;

    // Render.1. Top Toolbar
    const renderEnhancedTopToolbar = () => {
        const props = {
            advancedQuery, defaultAdvancedQuery: defaultIAddressAdvancedQuery(), setAdvancedQuery,
            rowCount,
            submitAdvancedSearch,
    
            hasItemsSelect: true, setSelected, numSelected,
            handleSelectAllClick,
    
            hasBulkDelete: true,
            handleDeleteSelected,
            hasBulkUpdate: false,
    
            hasListViewOptionsSelect: true, listViewOption, setListViewOption,
    
            hasPageSizeSelect: true,
    
            hasItemsPerRowSelect: true, itemsPerRow, setItemsPerRow,
    
            hasOrderBySelect: true, serverOrderBys,
    
            hasSearch: true,
            hasAdvancedSearchAccordion: true,
            advancedSearchExpanded,
            handleAdvancedSearchExpandClick,
            hasAdvancedSearchDialog: true,
            handleAdvancedSearchDialogOpen,
        } as ListToolBarProps<IAddressAdvancedQuery, IAddressIdentifier>;

        return <ListToolBar {...props} />
    }

    return (
        <>
            <Box sx={{ width: '100%' }}>
                <Paper sx={{ width: '100%', mb: 2 }}>
                    {renderEnhancedTopToolbar()}
                    <Collapse in={advancedSearchExpanded} timeout="auto" unmountOnExit>
                        <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchExpandClose(); }} />
                    </Collapse>
                    {listViewOption === ListViewOptions.SlideShow && <CarouselPartial
                        listViewOption={ListViewOptions.Tiles}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationLoadMore}
                        handleSelectItemClick={handleSelectItemClick}
                    />}
                    {listViewOption === ListViewOptions.Table && <HtmlTablePartial
                        listViewOption={ListViewOptions.Tiles}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationChangePage}
                        handleSelectItemClick={handleSelectItemClick}
                    />}
                    {listViewOption === ListViewOptions.Tiles && <TilesPartial
                        listViewOption={ListViewOptions.Tiles}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationLoadMore}
                        handleSelectItemClick={handleSelectItemClick}
                    />}
                </Paper>
            </Box>
            <Dialog open={openAdvancedSearchDialog} fullWidth={true} maxWidth={'lg'}>
                <DialogContent>
                    <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchDialogClose(); }} />
                </DialogContent>
            </Dialog>
        </>
    );
}

