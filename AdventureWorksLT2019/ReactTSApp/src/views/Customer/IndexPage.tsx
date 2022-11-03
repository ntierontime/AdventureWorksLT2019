import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { alpha } from '@mui/material/styles';

import {
    Box, Checkbox, Divider, FormControl, IconButton, InputAdornment, InputLabel, MenuItem, Paper,
    Select, SelectChangeEvent, OutlinedInput, ToggleButton, ToggleButtonGroup, Toolbar, Tooltip, Typography, Dialog, DialogContent, Collapse
} from '@mui/material';

import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import DeleteIcon from '@mui/icons-material/Delete';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import FormatListBulletedIcon from '@mui/icons-material/FormatListBulleted';
import PlayCircleOutlinedIcon from '@mui/icons-material/PlayCircleOutlined';
import RefreshIcon from '@mui/icons-material/Refresh';
import SearchIcon from '@mui/icons-material/Search';
import ViewModuleIcon from '@mui/icons-material/ViewModule';

import { useTranslation } from 'react-i18next';

import { RootState } from 'src/store/CombinedReducers';
import { AppDispatch } from 'src/store/Store';

import { PaginationOptions } from 'src/shared/dataModels/PaginationOptions';
import { ExpandMoreIconButton } from 'src/shared/views/ExpandMoreIconButton';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';

import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { customerSelectors, search, bulkDelete } from 'src/slices/CustomerSlice';
import { defaultICustomerAdvancedQuery, getCustomerQueryOrderBySettings, ICustomerAdvancedQuery, ICustomerIdentifier, getICustomerIdentifier, compareICustomerIdentifier } from 'src/dataModels/ICustomerQueries';

import AdvancedSearchPartial from './AdvancedSearchPartial'
import HtmlTablePartial from './HtmlTablePartial'

export default function IndexPage() {
    const dispatch = useDispatch<AppDispatch>();
    const { t } = useTranslation();
    const listItems = useSelector(
        (state: RootState) => customerSelectors.selectAll(state)
    );

    const [listViewOption, setListViewOption] = useState<ListViewOptions>(ListViewOptions.Table);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const serverOrderBys = getCustomerQueryOrderBySettings();
    const [advancedQuery, setAdvancedQuery] = useState<ICustomerAdvancedQuery>(defaultICustomerAdvancedQuery());
    const [selected, setSelected] = useState<readonly ICustomerIdentifier[]>([]);
    const [itemsPerRow, setItemsPerRow] = useState<number>(3); // only for ListViewOptions.Tiles, should use MediaQuery(windows size)

    useEffect(() => {
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    // 1. Top Toolbar
    // 1.1. Top Toolbar - Select All Checkbox
    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.checked) {
            const newSelected = listItems.map((n) => getICustomerIdentifier(n));
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };

    // 1.2. Top Toolbar - Refresh
    const handleRefresh = () => {
        if (!isLoading) {
            setAdvancedQuery(defaultICustomerAdvancedQuery());
            setSelected([]);
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.2. Top Toolbar - Delete Selected Rows/Items
    const handleDeleteSelected = () => {
        dispatch(bulkDelete(selected.map(t=>t)));
        // console.log("handleDeleteSelected");
    };

    // 1.3. Top Toolbar - Change ListViewOptions
    const handleChangeListViewOptions = (
        event: React.MouseEvent<HTMLElement>,
        newSetListViewOption: ListViewOptions,
    ) => {
        if (!!!newSetListViewOption) {
            return;
        }
        advancedQuery.pageIndex = 1;
        if (newSetListViewOption === ListViewOptions.Table) {
            advancedQuery.pageSize = 10;
            advancedQuery.paginationOption = PaginationOptions.PageIndexesAndAllButtons;
        }
        else {
            advancedQuery.pageSize = advancedQuery.pageSize = 12 * Math.floor(Math.sqrt(itemsPerRow));
            advancedQuery.paginationOption = PaginationOptions.LoadMore;
        }
        setListViewOption(newSetListViewOption);

        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.4.1. Top Toolbar only when ListViewOptions.Table - Change PageSize
    const handleChangePageSize = (event: SelectChangeEvent<number>) => {
        advancedQuery.pageSize = event.target.value as number;
        advancedQuery.pageIndex = 1;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.4.2. Top Toolbar only when ListViewOptions.Tile - Change ItemsPerRow
    const handleChangeItemsPerRow = (event: SelectChangeEvent<number>) => {
        const newItemsPerRow = event.target.value as number;
        setItemsPerRow(newItemsPerRow);
        advancedQuery.pageSize = 12 * Math.floor(Math.sqrt(newItemsPerRow));
        advancedQuery.pageIndex = 1;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.5. Top Toolbar - Change Sort/Order by
    const handleChangeSort = (event: SelectChangeEvent) => {
        advancedQuery.orderBys = event.target.value;
        advancedQuery.pageIndex = 1;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.6.1. Top Toolbar - Text Search
    const handleTextSearchClicked = () => {
        advancedQuery.pageIndex = 1;
        if (!isLoading) {
            setIsLoading(true);
            dispatch(search(advancedQuery)).finally(() => { setIsLoading(false); });
        }
    };

    // 1.6.2. Top Toolbar - Text to Search Changed
    const handleChangedTextToSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAdvancedQuery({ ...advancedQuery, textSearch: event.target.value });
    };

    // 1.7.1. Top Toolbar - Submit Advanced Search Action
    const submitAdvancedSearch = (query: ICustomerAdvancedQuery) => {
        dispatch(search({ ...query }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                }
                else { // failed
                }
                //console.log(result);
            })
            .catch((error) => {  /*console.log(error);*/ })
            .finally(() => { setAdvancedQuery(query); console.log('finally'); console.log(query); });
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
    const handleSelectItemClick = (item: ICustomerDataModel) => {
        const selectedIndex = selected.findIndex(t=>compareICustomerIdentifier(t, item));
        let newSelected: readonly ICustomerIdentifier[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, getICustomerIdentifier(item));
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
    const renderEnhancedTopToolbar = () => (
        <Toolbar
            sx={{
                pl: { sm: 0.5 },
                pr: { sm: 0.5 },
                ...(numSelected > 0 && {
                    bgcolor: (theme) => alpha(theme.palette.primary.main, theme.palette.action.activatedOpacity),
                }),
                width: '100%',
            }}
        >
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    bgcolor: 'background.paper',
                    borderRadius: 1,
                    width: '100%',
                }}
            >
                <Box>
                    <Checkbox
                        color="primary"
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={handleSelectAllClick}
                        inputProps={{
                            'aria-label': 'select All Customer',
                        }} />
                    <Tooltip title="Refresh">
                        <IconButton onClick={() => handleRefresh()}>
                            <RefreshIcon />
                        </IconButton>
                    </Tooltip>
                    {numSelected > 0 && (
                        <Tooltip title="Delete">
                            <IconButton onClick={() => handleDeleteSelected()}>
                                <DeleteIcon />
                            </IconButton>
                        </Tooltip>
                    )}
                </Box>

                {numSelected > 0 ? (
                    <Typography
                        color="inherit"
                        variant="subtitle1"
                    >
                        {numSelected} selected
                    </Typography>
                ) : (
                    <Box sx={{ p: 0, display: 'flex', alignItems: 'center' }}>
                        <FormControl variant="outlined"

                            sx={{
                                pr: 0,
                                width: { sm: 100, md: 150, lg: 400, xl: 600 },
                                "& .MuiOutlinedInput-root.Mui-focused": {
                                    "& > fieldset": {
                                        borderColor: "orange",
                                    }
                                },
                            }}
                        >
                            <OutlinedInput
                                sx={{ pr: 0.5, }}
                                value={advancedQuery.textSearch}
                                onChange={handleChangedTextToSearch}
                                placeholder={t("Search") + ' ' + t("Customer")}
                                fullWidth margin='none'
                                id="text-search-field"
                                size="small"
                                endAdornment={<InputAdornment position="end">
                                    <IconButton type="button" size='small' saria-label="text search" onClick={() => handleTextSearchClicked()}>
                                        <SearchIcon />
                                    </IconButton>
                                    <Divider sx={{ height: 38, p: 0, m: 0 }} orientation="vertical" />
                                    <IconButton color="primary" size='small' aria-label="advanced search" onClick={() => handleAdvancedSearchDialogOpen()}>
                                        <FilterAltIcon />
                                    </IconButton>
                                    <ExpandMoreIconButton
                                        size='small'
                                        expand={advancedSearchExpanded}
                                        onClick={handleAdvancedSearchExpandClick}
                                        aria-expanded={advancedSearchExpanded}
                                        aria-label="show more"
                                    >
                                        <FilterAltIcon />
                                    </ExpandMoreIconButton>
                                </InputAdornment>}
                            />
                        </FormControl>
                    </Box>
                )}
                <Box sx={{ alignItems: 'center', pt: 0.5 }}>
                    {numSelected === 0 && (
                        <>
                            <ToggleButtonGroup
                                size="small"
                                sx={{ mt: 1 }}
                                value={listViewOption}
                                exclusive
                                onChange={handleChangeListViewOptions}
                                aria-label="list options"
                            >
                                <ToggleButton value={ListViewOptions.Table} aria-label="htmltable">
                                    <FormatListBulletedIcon />
                                </ToggleButton>
                                <ToggleButton value={ListViewOptions.Tiles} aria-label="tiles">
                                    <ViewModuleIcon />
                                </ToggleButton>
                                <ToggleButton value={ListViewOptions.SlideShow} aria-label="slideshow">
                                    <PlayCircleOutlinedIcon />
                                </ToggleButton>
                            </ToggleButtonGroup>
                            {listViewOption === ListViewOptions.Table && <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
                                <InputLabel id="page-size-select">{t("PageSize")}</InputLabel>
                                <Select
                                    labelId="page-size-select"
                                    id="page-size-select"
                                    value={advancedQuery.pageSize}
                                    label={t("PageSize")}
                                    onChange={handleChangePageSize}
                                >
                                    <MenuItem value={10}>10</MenuItem>
                                    <MenuItem value={25}>25</MenuItem>
                                    <MenuItem value={100}>100</MenuItem>
                                </Select>
                            </FormControl>}
                            {listViewOption === ListViewOptions.Tiles && <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
                                <InputLabel id="tile-size-select">{t("ItemsPerRow")}</InputLabel>
                                <Select
                                    labelId="tile-tile-select"
                                    id="tile-tile-select"
                                    value={itemsPerRow}
                                    label={t("ItemsPerRow")}
                                    onChange={handleChangeItemsPerRow}
                                >
                                    <MenuItem value={1}>1</MenuItem>
                                    <MenuItem value={3}>3</MenuItem>
                                    <MenuItem value={4}>4</MenuItem>
                                    <MenuItem value={6}>6</MenuItem>
                                    <MenuItem value={12}>12</MenuItem>
                                </Select>
                            </FormControl>}
                            <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
                                <InputLabel id="orderby-select">{t("Sort")}</InputLabel>
                                <Select
                                    labelId="orderby-select"
                                    id="orderby-select"
                                    value={advancedQuery.orderBys}
                                    label={t("Sort")}
                                    onChange={handleChangeSort}
                                >
                                    {serverOrderBys.map((serverOrderBy) => {
                                        return (<MenuItem key={serverOrderBy.expression} value={serverOrderBy.expression}>
                                            {serverOrderBy.direction === 'asc' ? <ArrowDownwardIcon fontSize="inherit" /> : <ArrowUpwardIcon fontSize="inherit" />}
                                            {serverOrderBy.displayName}
                                        </MenuItem>);
                                    })}
                                </Select>
                            </FormControl>
                        </>
                    )}
                </Box>
            </Box>
        </Toolbar>
    );

    return (
        <>
            <Box sx={{ width: '100%' }}>
                <Paper sx={{ width: '100%', mb: 2 }}>
                    {renderEnhancedTopToolbar()}
                    <Collapse in={advancedSearchExpanded} timeout="auto" unmountOnExit>
                        <AdvancedSearchPartial advancedQuery={advancedQuery} submitAction={submitAdvancedSearch} doneAction={() => { handleAdvancedSearchExpandClose(); }} />
                    </Collapse>
                    {listViewOption === ListViewOptions.Table && <HtmlTablePartial
                        listViewOption={ListViewOptions.Tiles}
                        listItems={listItems}
                        itemsPerRow={itemsPerRow}
                        numSelected={numSelected}
                        selected={selected}
                        handleChangePage={handlePaginationChangePage}
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

