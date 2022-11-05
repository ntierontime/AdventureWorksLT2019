import { alpha } from '@mui/material/styles';

import {
    Box, Checkbox, Divider, FormControl, IconButton, InputAdornment, InputLabel, MenuItem, Paper,
    Select, SelectChangeEvent, OutlinedInput, ToggleButton, ToggleButtonGroup, Toolbar, Tooltip, Typography
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

import { ExpandMoreIconButton } from 'src/shared/views/ExpandMoreIconButton';
import { ListViewOptions } from 'src/shared/views/ListViewOptions';
import { IBaseQuery } from '../dataModels/IBaseQuery';
import { IQueryOrderBySetting } from '../viewModels/IQueryOrderBySetting';
import { PaginationOptions } from '../dataModels/PaginationOptions';
import { INameValuePair } from '../dataModels/INameValuePair';

export interface ListToolBarProps<TAdvancedQuery, TIdentifier> {
    advancedQuery: TAdvancedQuery;
    defaultAdvancedQuery: TAdvancedQuery
    setAdvancedQuery: React.Dispatch<React.SetStateAction<TAdvancedQuery>>;
    rowCount: number;
    submitAdvancedSearch: (query: TAdvancedQuery) => void;

    hasListViewOptionsSelect: boolean;
    availableListViewOptions: ListViewOptions[];
    listViewOption: ListViewOptions;
    setListViewOption: React.Dispatch<React.SetStateAction<ListViewOptions>>;

    hasItemsSelect: boolean;
    setSelected: React.Dispatch<React.SetStateAction<TIdentifier[]>>;
    numSelected: number;
    handleSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;

    hasBulkDelete: boolean;
    handleDeleteSelected: () => void;
    hasBulkUpdate: boolean;

    hasItemsPerRowSelect: boolean;
    itemsPerRow: number;
    setItemsPerRow: React.Dispatch<React.SetStateAction<number>>;

    hasPageSizeSelect: boolean;
    availablePageSizes: INameValuePair[];

    hasOrderBySelect: boolean;
    serverOrderBys: IQueryOrderBySetting[];

    hasSearch: boolean;
    hasAdvancedSearchAccordion: boolean;
    advancedSearchExpanded: boolean;
    handleAdvancedSearchExpandClick: () => void;
    hasAdvancedSearchDialog: boolean;
    handleAdvancedSearchDialogOpen: () => void;
}

export default function ListToolBar<TAdvancedQuery extends IBaseQuery, TIdentifier>(props: ListToolBarProps<TAdvancedQuery, TIdentifier>): JSX.Element {
    const {
        advancedQuery, defaultAdvancedQuery, setAdvancedQuery,
        rowCount,
        submitAdvancedSearch,

        hasItemsSelect, setSelected, numSelected,
        handleSelectAllClick,

        hasBulkDelete,
        handleDeleteSelected,
        hasBulkUpdate,

        hasListViewOptionsSelect, availableListViewOptions, listViewOption, setListViewOption,

        hasPageSizeSelect, availablePageSizes,

        hasItemsPerRowSelect, itemsPerRow, setItemsPerRow,

        hasOrderBySelect, serverOrderBys,

        hasSearch,
        hasAdvancedSearchAccordion,
        advancedSearchExpanded,
        handleAdvancedSearchExpandClick,
        hasAdvancedSearchDialog,
        handleAdvancedSearchDialogOpen,
    } = props;
    const { t } = useTranslation();

    // 1.2. Top Toolbar - Refresh
    const handleRefresh = () => {
        setAdvancedQuery(defaultAdvancedQuery);
        setSelected([]);
        submitAdvancedSearch(advancedQuery);
    };

    // 1.3. Top Toolbar - Change ListViewOptions, MOVED
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

        submitAdvancedSearch(advancedQuery);
    };

    // 1.4.1. Top Toolbar only when ListViewOptions.Table - Change PageSize
    const handleChangePageSize = (event: SelectChangeEvent<number>) => {
        advancedQuery.pageSize = event.target.value as number;
        advancedQuery.pageIndex = 1;
        submitAdvancedSearch(advancedQuery);
    };

    // 1.4.2. Top Toolbar only when ListViewOptions.Tile - Change ItemsPerRow
    const handleChangeItemsPerRow = (event: SelectChangeEvent<number>) => {
        const newItemsPerRow = event.target.value as number;
        setItemsPerRow(newItemsPerRow);
        advancedQuery.pageSize = 12 * Math.floor(Math.sqrt(newItemsPerRow));
        advancedQuery.pageIndex = 1;
        submitAdvancedSearch(advancedQuery);
    };

    // 1.5. Top Toolbar - Change Sort/Order by
    const handleChangeSort = (event: SelectChangeEvent) => {
        advancedQuery.orderBys = event.target.value;
        advancedQuery.pageIndex = 1;
        submitAdvancedSearch(advancedQuery);
    };

    // 1.6.1. Top Toolbar - Text Search
    const handleTextSearchClicked = () => {
        advancedQuery.pageIndex = 1;
        submitAdvancedSearch(advancedQuery);
    }

    // 1.6.2. Top Toolbar - Text to Search Changed
    const handleChangedTextToSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAdvancedQuery({ ...advancedQuery, textSearch: event.target.value });
    }

    return (
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
                    {hasItemsSelect && <Checkbox
                        color="primary"
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={handleSelectAllClick}
                        inputProps={{
                            'aria-label': 'select All Product',
                        }} />}
                    <Tooltip title="Refresh">
                        <IconButton onClick={() => handleRefresh()}>
                            <RefreshIcon />
                        </IconButton>
                    </Tooltip>
                    {hasItemsSelect && hasBulkDelete && numSelected > 0 && (
                        <Tooltip title="Delete">
                            <IconButton onClick={() => handleDeleteSelected()}>
                                <DeleteIcon />
                            </IconButton>
                        </Tooltip>
                    )}
                </Box>

                {hasItemsSelect && numSelected > 0 ? (
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
                            {hasSearch && <OutlinedInput
                                sx={{ pr: 0.5, }}
                                defaultValue={advancedQuery.textSearch}
                                onChange={handleChangedTextToSearch}
                                placeholder={t("Search") + ' ' + t("Product")}
                                fullWidth margin='none'
                                id="text-search-field"
                                size="small"
                                endAdornment={<InputAdornment position="end">
                                    <IconButton type="button" size='small' saria-label="text search" onClick={() => handleTextSearchClicked()}>
                                        <SearchIcon />
                                    </IconButton>
                                    {(hasAdvancedSearchAccordion || hasAdvancedSearchAccordion) && <Divider sx={{ height: 38, p: 0, m: 0 }} orientation="vertical" />}
                                    {hasAdvancedSearchDialog && <IconButton color="primary" size='small' aria-label="advanced search" onClick={() => handleAdvancedSearchDialogOpen()}>
                                        <FilterAltIcon />
                                    </IconButton>}
                                    {hasAdvancedSearchAccordion && <ExpandMoreIconButton
                                        size='small'
                                        expand={advancedSearchExpanded}
                                        onClick={handleAdvancedSearchExpandClick}
                                        aria-expanded={advancedSearchExpanded}
                                        aria-label="show more"
                                    >
                                        <FilterAltIcon />
                                    </ExpandMoreIconButton>}
                                </InputAdornment>}
                            />}
                        </FormControl>
                    </Box>
                )}
                <Box sx={{ alignItems: 'center', pt: 0.5 }}>
                    {numSelected === 0 && (
                        <>
                            {hasListViewOptionsSelect && !!availableListViewOptions && availableListViewOptions.length > 1 && <ToggleButtonGroup
                                size="small"
                                sx={{ mt: 1 }}
                                value={listViewOption}
                                exclusive
                                onChange={handleChangeListViewOptions}
                                aria-label="list options"
                            >
                                {availableListViewOptions.indexOf(ListViewOptions.Table) !== -1 &&
                                    <ToggleButton value={ListViewOptions.Table} aria-label="htmltable">
                                        <FormatListBulletedIcon />
                                    </ToggleButton>}
                                {availableListViewOptions.indexOf(ListViewOptions.Tiles) !== -1 && <ToggleButton value={ListViewOptions.Tiles} aria-label="tiles">
                                    <ViewModuleIcon />
                                </ToggleButton>}
                                {availableListViewOptions.indexOf(ListViewOptions.SlideShow) !== -1 && <ToggleButton value={ListViewOptions.SlideShow} aria-label="slideshow">
                                    <PlayCircleOutlinedIcon />
                                </ToggleButton>}
                            </ToggleButtonGroup>}
                            {hasPageSizeSelect && listViewOption === ListViewOptions.Table && <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
                                <InputLabel id="page-size-select">{t("PageSize")}</InputLabel>
                                {(!!!availablePageSizes || availablePageSizes.length == 0) && <Select
                                    labelId="page-size-select"
                                    id="page-size-select"
                                    value={advancedQuery.pageSize}
                                    label={t("PageSize")}
                                    onChange={handleChangePageSize}
                                >
                                    <MenuItem value={10}>10</MenuItem>
                                    <MenuItem value={25}>25</MenuItem>
                                    <MenuItem value={100}>100</MenuItem>
                                </Select>}
                                {(!!availablePageSizes && availablePageSizes.length > 0) && <Select
                                    labelId="page-size-select"
                                    id="page-size-select"
                                    value={advancedQuery.pageSize}
                                    label={t("PageSize")}
                                    onChange={handleChangePageSize}
                                >
                                    {availablePageSizes.map((availablePageSize) => {
                                        return (
                                            <MenuItem key={availablePageSize.value} value={availablePageSize.value}>{availablePageSize.name}</MenuItem>)
                                    })}
                                </Select>}
                            </FormControl>}
                            {hasItemsPerRowSelect && listViewOption === ListViewOptions.Tiles && <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
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
                            {hasOrderBySelect && <FormControl size="small" sx={{ m: 1, minWidth: 120 }}>
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
                            </FormControl>}
                        </>
                    )}
                </Box>
            </Box>
        </Toolbar>);
}