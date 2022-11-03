import { useState } from 'react';
import { useSelector } from 'react-redux';
import { ButtonGroup, Checkbox, Dialog, FormControlLabel, IconButton, Pagination, Popover, Snackbar, Stack, Switch, Table, TableBody, TableCell, TableContainer, TableRow } from '@mui/material';
import { Link } from 'react-router-dom';
import AddIcon from '@mui/icons-material/Add';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { getCRUDItemPartialViewPropsOnDialog, ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { QueryOrderDirections } from 'src/shared/dataModels/QueryOrderDirections';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { EnhancedTableHead } from 'src/shared/views/EnhancedTableHead';
import { Item } from 'src/shared/views/Item';
import { getComparator, HeadCell, stableSort } from 'src/shared/views/TableFeatures';

import { RootState } from 'src/store/CombinedReducers';

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import ItemViewsPartial from './ItemViewsPartial';

export default function HtmlTablePartial(props: ListPartialViewProps<ISalesOrderHeaderDataModel, number>): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const { listItems, selected, numSelected, handleChangePage, handleSelectItemClick } = props;
    const [order, setOrder] = useState<QueryOrderDirections>('asc');
    const [orderBy, setOrderBy] = useState<keyof ISalesOrderHeaderDataModel>('orderDate');
    const [dense, setDense] = useState(true);
    const { pagination } = useSelector((state: RootState) => state.salesOrderHeaderList);
    const [currentItemIndex, setCurrentItemIndex] = useState<number>();
    const [anchorElItemActions, setAnchorElItemActions] = useState<HTMLElement | null>(null);
    const openPopoverItemActions = Boolean(anchorElItemActions);

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
        property: keyof ISalesOrderHeaderDataModel,
    ) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    // 3.2. Bottom Toolbar - Change Table Dense
    const handleChangeTableDense = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDense(event.target.checked);
    };

    const [openItemDialog, setOpenItemDialog] = useState(false);
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<ISalesOrderHeaderDataModel> | null>(null);

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<ISalesOrderHeaderDataModel>(
            viewItemTemplate,
            handleItemDialogClose
        );
        setCRUDItemPartialViewProps(dialogProps);
        handleItemActionsPopoverClose();
        setOpenItemDialog(true);
    };

    const handleItemDialogClose = () => {
        setOpenItemDialog(false);
        setCRUDItemPartialViewProps(null);
    };

    const orderedListItems = !!listItems ? stableSort(listItems, getComparator(order, orderBy)) as ISalesOrderHeaderDataModel[] : [];
    const currentItemOnDialog = !!orderedListItems && orderedListItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < orderedListItems.length ? orderedListItems[currentItemIndex] : null;
    const isSelected = (salesOrderID: number) => selected.indexOf(salesOrderID) !== -1;
    const headCells: HeadCell[] = [

        {
            id: 'salesOrderID',
            numeric: true,
            disablePadding: true,
            label: t('SalesOrderID'),
        },
        {
            id: 'revisionNumber',
            numeric: true,
            disablePadding: true,
            label: t('RevisionNumber'),
        },
        {
            id: 'orderDate',
            numeric: false,
            disablePadding: true,
            label: t('OrderDate'),
        },
        {
            id: 'dueDate',
            numeric: false,
            disablePadding: true,
            label: t('DueDate'),
        },
        {
            id: 'shipDate',
            numeric: false,
            disablePadding: true,
            label: t('ShipDate'),
        },
        {
            id: 'status',
            numeric: true,
            disablePadding: true,
            label: t('Status'),
        },
        {
            id: 'onlineOrderFlag',
            numeric: false,
            disablePadding: true,
            label: t('OnlineOrderFlag'),
        },
        {
            id: 'salesOrderNumber',
            numeric: false,
            disablePadding: true,
            label: t('SalesOrderNumber'),
        },
        {
            id: 'purchaseOrderNumber',
            numeric: false,
            disablePadding: true,
            label: t('PurchaseOrderNumber'),
        },
        {
            id: 'accountNumber',
            numeric: false,
            disablePadding: true,
            label: t('AccountNumber'),
        },
        {
            id: 'customerID',
            numeric: true,
            disablePadding: true,
            label: t('CustomerID'),
        },
        {
            id: 'shipToAddressID',
            numeric: true,
            disablePadding: true,
            label: t('ShipToAddressID'),
        },
        {
            id: 'billToAddressID',
            numeric: true,
            disablePadding: true,
            label: t('BillToAddressID'),
        },
        {
            id: 'shipMethod',
            numeric: false,
            disablePadding: true,
            label: t('ShipMethod'),
        },
        {
            id: 'creditCardApprovalCode',
            numeric: false,
            disablePadding: true,
            label: t('CreditCardApprovalCode'),
        },
        {
            id: 'subTotal',
            numeric: true,
            disablePadding: true,
            label: t('SubTotal'),
        },
        {
            id: 'taxAmt',
            numeric: true,
            disablePadding: true,
            label: t('TaxAmt'),
        },
        {
            id: 'freight',
            numeric: true,
            disablePadding: true,
            label: t('Freight'),
        },
        {
            id: 'totalDue',
            numeric: true,
            disablePadding: true,
            label: t('TotalDue'),
        },
        {
            id: 'comment',
            numeric: false,
            disablePadding: true,
            label: t('Comment'),
        },
        {
            id: '_rowguid',
            numeric: false,
            disablePadding: true,
            label: t('rowguid'),
        },
        {
            id: 'modifiedDate',
            numeric: false,
            disablePadding: true,
            label: t('ModifiedDate'),
        },
    ];

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
                                const isItemSelected = isSelected(row.salesOrderID);
                                const labelId = `enhanced-table-checkbox-${index}`;

                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        aria-checked={isItemSelected}
                                        tabIndex={-1}
                                        key={row.salesOrderID}
                                        selected={isItemSelected}
                                    >
                                        <TableCell padding="checkbox">
                                            <Checkbox
                                                color="primary"
                                                checked={isItemSelected}
                                                onChange={() => { handleSelectItemClick(row) }}
                                                inputProps={{
                                                    'aria-labelledby': labelId,
                                                }}
                                            />
                                        </TableCell>
                                        <TableCell
                                            component="th"
                                            id={labelId}
                                            scope="row"
                                            padding="none"
                                        >
                                            {row.salesOrderID}
                                        </TableCell>
                                        <TableCell align='right'>{t(i18nFormats.number.format, { val: row.revisionNumber })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.dueDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.shipDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.number.format, { val: row.status })}</TableCell>
                                        <TableCell align='right'><Checkbox checked={row.onlineOrderFlag} readOnly /></TableCell>
                                        <TableCell align='right'>{row.salesOrderNumber}</TableCell>
                                        <TableCell align='right'>{row.purchaseOrderNumber}</TableCell>
                                        <TableCell align='right'>{row.accountNumber}</TableCell>
                                        <TableCell align='right'><Link to={"/customer/Details/" + row.customerID}>{row.customer_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/address/Details/" + row.shipToAddressID}>{row.shipTo_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/address/Details/" + row.billToAddressID}>{row.billTo_Name}</Link></TableCell>
                                        <TableCell align='right'>{row.shipMethod}</TableCell>
                                        <TableCell align='right'>{row.creditCardApprovalCode}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.subTotal })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.taxAmt })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.freight })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.totalDue })}</TableCell>
                                        <TableCell align='right'>{row.comment}</TableCell>
                                        <TableCell align='right'>{row._rowguid}</TableCell>
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
            <Snackbar
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                open={true}
            >
				<ButtonGroup orientation='horizontal'>
                    <IconButton onClick={() => { handleItemDialogOpen(ViewItemTemplates.Create); }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
                        <AddIcon />
                    </IconButton>
                    <IconButton onClick={() => { navigate("/salesOrderHeader/create") }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
                        <AddIcon />
                    </IconButton>
                </ButtonGroup>
            </Snackbar>
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
                <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/salesOrderHeader/delete/" + currentItemOnDialog.salesOrderID) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { navigate("/salesOrderHeader/details/" + currentItemOnDialog.salesOrderID) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/salesOrderHeader/edit/" + currentItemOnDialog.salesOrderID) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
            </Popover>
            <Dialog open={openItemDialog} fullWidth={true} maxWidth={'sm'}>
                <ItemViewsPartial {...crudItemPartialViewProps} item={currentItemOnDialog} isItemSelected={!!currentItemOnDialog && isSelected(currentItemOnDialog.salesOrderID)} totalCountInList={listItems.length} itemIndex={currentItemIndex} setItemIndex={setCurrentItemIndex} handleSelectItemClick={handleSelectItemClick} />
            </Dialog>
            {!numSelected && <Stack direction="row" onMouseEnter={() => { handleItemActionsPopoverClose(); }}>
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

