import { useEffect, useMemo, useState } from 'react';
import { useSelector } from 'react-redux';
import { Checkbox, FormControlLabel, IconButton, Link, Pagination, Popover, Stack, Switch, Table, TableBody, TableCell, TableContainer, TableRow } from '@mui/material';

import AccountTreeIcon from '@mui/icons-material/AccountTree';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { QueryOrderDirections } from 'src/shared/dataModels/QueryOrderDirections';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { EnhancedTableHead } from 'src/shared/views/EnhancedTableHead';
import { Item } from 'src/shared/views/Item';
import { getComparator, HeadCell, stableSort } from 'src/shared/views/TableFeatures';

import { RootState } from 'src/store/CombinedReducers';

import { IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { IAddressIdentifier, getIAddressIdentifier, getRouteParamsOfIAddressIdentifier } from 'src/dataModels/IAddressQueries';

export default function HtmlTablePartial(props: ListPartialViewProps<IAddressDataModel, IAddressIdentifier>): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    // currentItemOnDialog is only used in page navigation, you can remove it if not-in-use.
    const { listItems, hasItemsSelect, numSelected, isSelected, handleChangePage, handleSelectItemClick, handleItemDialogOpen, currentItemOnDialog, setCurrentItemOnDialog, currentItemIndex, setCurrentItemIndex } = props;
    const [order, setOrder] = useState<QueryOrderDirections>('asc');
    const [orderBy, setOrderBy] = useState<keyof IAddressDataModel>('modifiedDate');
    const [dense, setDense] = useState(true);
    const { pagination } = useSelector((state: RootState) => state.addressList);
    const [anchorElItemActions, setAnchorElItemActions] = useState<HTMLElement | null>(null);
    const openPopoverItemActions = Boolean(anchorElItemActions);

    const orderedListItems = useMemo(() => !!listItems ? stableSort(listItems, getComparator(order, orderBy)) as IAddressDataModel[] : [], [listItems, order, orderBy]);

    useEffect(() => {
        setCurrentItemOnDialog(!!orderedListItems && orderedListItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < orderedListItems.length ? orderedListItems[currentItemIndex] : null);
    }, [currentItemIndex, orderedListItems, setCurrentItemOnDialog]);

    const handleItemActionsPopoverOpen = (event: React.MouseEvent<HTMLElement>, thisIndex: number) => {
        event.stopPropagation();
        setAnchorElItemActions(event.currentTarget);
        setCurrentItemIndex(thisIndex);
    };

    const handleItemActionsPopoverClose = () => {
        setAnchorElItemActions(null);
    };

    // 2.1. Table Specific
    // 2.1.2 Table Specific - Table Head Column Sort
    const handleClientSideRequestSort = (
        event: React.MouseEvent<unknown>,
        property: keyof IAddressDataModel,
    ) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    // 3.2. Bottom Toolbar - Change Table Dense
    const handleChangeTableDense = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDense(event.target.checked);
    };

    return (
        <>
            <TableContainer onMouseEnter={(event) => { handleItemActionsPopoverClose() }}>
                <Table
                    sx={{ minWidth: 750 }}
                    aria-labelledby="tableTitle"
                    size={dense ? 'small' : 'medium'}
                    stickyHeader
                >
                    <EnhancedTableHead
					    hasItemsSelect={hasItemsSelect}
                        order={order}
                        orderBy={orderBy}
                        onRequestSort={handleClientSideRequestSort}
                        headCells={headCells}
                        onMouseOver={() => { handleItemActionsPopoverClose(); }}
                    />
                    <TableBody>
                        {/* if you don't need to support IE11, you can replace the `stableSort` call with:
                    rows.slice().sort(getComparator(order, orderBy)) */}
                        {orderedListItems
                            //.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            .map((row, index) => {
								const key = getRouteParamsOfIAddressIdentifier(row);
                                const isItemSelected = isSelected(getIAddressIdentifier(row));
                                const labelId = `enhanced-table-checkbox-${key}`;

                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        aria-checked={isItemSelected}
                                        tabIndex={-1}
                                        key={key}
                                        selected={isItemSelected}
                                    >
                                        {hasItemsSelect && !!handleSelectItemClick && <TableCell padding="checkbox">
                                            <Checkbox
                                                color="primary"
                                                checked={isItemSelected}
                                                onChange={() => { handleSelectItemClick(row) }}
                                                inputProps={{
                                                    'aria-labelledby': labelId,
                                                }}
                                            />
                                        </TableCell>}
                                        <TableCell
                                            component="th"
                                            id={labelId}
                                            scope="row"
                                            padding="none"
                                        >
                                            {row.addressID}
                                        </TableCell>
                                        <TableCell align='right'>{row.addressLine1}</TableCell>
                                        <TableCell align='right'>{row.addressLine2}</TableCell>
                                        <TableCell align='right'>{row.city}</TableCell>
                                        <TableCell align='right'>{row.stateProvince}</TableCell>
                                        <TableCell align='right'>{row.countryRegion}</TableCell>
                                        <TableCell align='right'>{row.postalCode}</TableCell>
                                        <TableCell align='right'>{row.rowguid}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align="right" onMouseOver={(event) => { handleItemActionsPopoverOpen(event, index) }}>
                                            <IconButton aria-label="more" size="small" >
                                                <MoreVertIcon />
                                            </IconButton>
                                        </TableCell>
                                    </TableRow>
                                );
                            })}
                        {/* {emptyRows > 0 && (
                    <TableRow
                        style={{
                            height: (dense ? 33 : 53) * emptyRows,
                        }}
                    >
                        <TableCell colSpan={6} />
                    </TableRow>
                )} */}
                    </TableBody>
                </Table>
            </TableContainer>
            <Popover id="item-action-popover"
                sx={{ pointerEvents: 'none', }}
                open={openPopoverItemActions}
                anchorEl={anchorElItemActions}
                anchorOrigin={{ vertical: 'center', horizontal: 'right', }}
                transformOrigin={{ vertical: 'center', horizontal: 'right', }}
                PaperProps={{
                    onMouseLeave: () => { handleItemActionsPopoverClose() },
                    sx: {
                        pointerEvents: 'auto'
                    }
                }}
                disableRestoreFocus
            >
                <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, null) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, null) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, null) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/address/delete/" + getRouteParamsOfIAddressIdentifier(currentItemOnDialog)) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { navigate("/address/details/" + getRouteParamsOfIAddressIdentifier(currentItemOnDialog)) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/address/edit/" + getRouteParamsOfIAddressIdentifier(currentItemOnDialog)) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/address/dashboard/" + getRouteParamsOfIAddressIdentifier(currentItemOnDialog)) }}>
                    <AccountTreeIcon />
                </IconButton>
            </Popover>
            {!!handleChangePage && !numSelected && <Stack direction="row" onMouseEnter={() => { handleItemActionsPopoverClose(); }}>
                <Item sx={{ width: 1 }}>
                    <Pagination count={Math.ceil(pagination.totalCount / ((1.0) * pagination.pageSize))} page={pagination.pageIndex} showFirstButton showLastButton variant="outlined" shape="rounded" onChange={handleChangePage} />
                </Item>
                <Item>
                    <FormControlLabel control={<Switch checked={dense} onChange={handleChangeTableDense} />} label="Dense" />
                </Item>
            </Stack>}
        </>
    );
}

const headCells: HeadCell[] = [

    {
        id: 'addressID',
        numeric: true,
        disablePadding: true,
        label: 'AddressID',
    },
    {
        id: 'addressLine1',
        numeric: false,
        disablePadding: true,
        label: 'AddressLine1',
    },
    {
        id: 'addressLine2',
        numeric: false,
        disablePadding: true,
        label: 'AddressLine2',
    },
    {
        id: 'city',
        numeric: false,
        disablePadding: true,
        label: 'City',
    },
    {
        id: 'stateProvince',
        numeric: false,
        disablePadding: true,
        label: 'StateProvince',
    },
    {
        id: 'countryRegion',
        numeric: false,
        disablePadding: true,
        label: 'CountryRegion',
    },
    {
        id: 'postalCode',
        numeric: false,
        disablePadding: true,
        label: 'PostalCode',
    },
    {
        id: 'rowguid',
        numeric: false,
        disablePadding: true,
        label: 'rowguid',
    },
    {
        id: 'modifiedDate',
        numeric: false,
        disablePadding: true,
        label: 'ModifiedDate',
    },
];

