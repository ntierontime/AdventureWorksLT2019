import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ListsPartialViewProps } from 'src/shared/viewModels/ListsPartialViewProps';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { buildVersionSelectors } from 'src/slices/BuildVersionSlice';
import { compareIBuildVersionIdentifier, defaultIBuildVersionAdvancedQuery, getIBuildVersionIdentifier, getRouteParamsOfIBuildVersionIdentifier, IBuildVersionAdvancedQuery, IBuildVersionIdentifier, } from 'src/dataModels/IBuildVersionQueries';
import ListsPartial from './ListsPartial';
import InfiniteScroll from 'react-infinite-scroll-component';
import { Box, CircularProgress, Collapse, Grid, Paper } from '@mui/material';
import { setLoading } from 'src/slices/appSlice';
import { search, bulkDelete } from 'src/slices/BuildVersionSlice';
import { AppDispatch } from 'src/store/Store';
import { IListResponse } from 'src/shared/apis/IListResponse';
import AdvancedSearchPartial from './AdvancedSearchPartial';

const gridItemSpacing = 0.5;

export default function IndexPage1() {
    const { loading } = useSelector((state: RootState) => state.app);
    const dispatch = useDispatch<AppDispatch>();
    const { t } = useTranslation();
    const [advancedQuery, setAdvancedQuery] = useState<IBuildVersionAdvancedQuery>(defaultIBuildVersionAdvancedQuery());
    const listItems = useSelector(
        (state: RootState) => buildVersionSelectors.selectAll(state)
    );

    const { pagination } = useSelector((state: RootState) => state.buildVersionList);
    const hasMoreItems = pagination.pageIndex !== pagination.lastPageIndex;


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

    // 3.2. Top Toolbar - Advanced Search Inline - Collapse Panel 
    const [advancedSearchExpanded, setAdvancedSearchExpanded] = useState(false);
    const handleAdvancedSearchExpandClick = () => {
        setAdvancedSearchExpanded(!advancedSearchExpanded);
    };
    const handleAdvancedSearchExpandClose = () => {
        setAdvancedSearchExpanded(false);
    };


    // 4. Bottom Toolbar - Pagination
    // 4.1. Bottom Toolbar - Pagination - Change Page
    const handlePaginationChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
        advancedQuery.pageIndex += value;
        if (!loading) {
            setLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setLoading(false); });
        }
    };

    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("BuildVersion") + ":" + t("_APPLICATION_TITLE_");

        submitAdvancedSearch(advancedQuery);
    }, []);


    const submitAdvancedSearch = (query: IBuildVersionAdvancedQuery) => {
        if (!loading) {
            setLoading(true);
            dispatch(search({ ...query }))
                .then((result) => {
                    if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    }
                    else { // failed
                    }
                    //console.log(result);
                })
                .catch((error) => {  /*console.log(error);*/ })
                .finally(() => { setLoading(false); setAdvancedQuery(query); /*console.log('finally'); console.log(query);*/ });
        }
    };

    const rowCount = listItems.length;

    // // Render.1. Top Toolbar
    // const renderEnhancedTopToolbar = () => {
    //     const topToolbarProps = {

    //         listToolBarSetting: {
    //             textSearchPlaceHolder: t("Search") + ' ' + t("BuildVersion"),
    //             hasListViewOptionsSelect: false,
    //             availableListViewOptions: [],
    //             hasItemsSelect: true,
    //             hasBulkDelete: true,
    //             hasBulkUpdate: true,
    //             hasItemsPerRowSelect: true, // When "Tiles"
    //             hasPageSizeSelect: true,    // When "Table"
    //             hasOrderBySelect: true,
    //             hasSearch: true,			// Text Search
    //             hasAdvancedSearchAccordion: true,
    //             hasAdvancedSearchDialog: false,
    //         },
            
    //         advancedQuery, defaultAdvancedQuery: defaultIBuildVersionAdvancedQuery(), setAdvancedQuery,
    //         rowCount,
    //         submitAdvancedSearch,

    //         setSelected,
    //         numSelected,
    //         handleSelectAllClick,

    //         handleDeleteSelected,

    //         listViewOption,
    //         setListViewOption,

    //         itemsPerRow,
    //         setItemsPerRow,

    //         serverOrderBys,

    //         advancedSearchExpanded,
    //         handleAdvancedSearchExpandClick,
    //         handleAdvancedSearchDialogOpen,

    //         hasAddNewButton: addNewButtonContainer === ContainerOptions.ListToolBar,
    //         handleAddNewClick: () => { handleItemDialogOpen(ViewItemTemplates.Create, -1); },
    //     } as ListToolBarProps<IBuildVersionAdvancedQuery, IBuildVersionIdentifier>;

    //     return <ListToolBar {...topToolbarProps} />
    // }

    return (
        <Box sx={{ width: '100%' }}>
            <Paper sx={{ width: '100%', mb: 2 }}>
                {/* {renderEnhancedTopToolbar()} */}
                <Collapse in={advancedSearchExpanded} timeout="auto" unmountOnExit>
                    <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchExpandClose(); }} />
                </Collapse>

                <Grid container spacing={gridItemSpacing}>
                    {listItems && listItems.map((row, index) => {
                        const isItemSelected = isSelected(getIBuildVersionIdentifier(row));

                        return (
                            <Grid item xs={12} key={getRouteParamsOfIBuildVersionIdentifier(row)}>
                                <Paper elevation={3} sx={{ p: 1, height: "100%" }}>
                                    {row.database_Version}
                                </Paper>
                            </Grid>
                        );
                    })}
                </Grid>
            </Paper>
        </Box>
    );
}

