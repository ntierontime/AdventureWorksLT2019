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

import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { ISalesOrderDetailIdentifier, getISalesOrderDetailIdentifier, compareISalesOrderDetailIdentifier, getRouteParamsOfISalesOrderDetailIdentifier } from 'src/dataModels/ISalesOrderDetailQueries';
import ItemViewsPartial from './ItemViewsPartial';

export default function HtmlTablePartial(props: ListPartialViewProps<ISalesOrderDetailDataModel, ISalesOrderDetailIdentifier>): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const { listItems, selected, numSelected, handleChangePage, handleSelectItemClick } = props;
    const [order, setOrder] = useState<QueryOrderDirections>('asc');
    const [orderBy, setOrderBy] = useState<keyof ISalesOrderDetailDataModel>('modifiedDate');
    const [dense, setDense] = useState(true);
    const { pagination } = useSelector((state: RootState) => state.salesOrderDetailList);
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
        property: keyof ISalesOrderDetailDataModel,
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
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<ISalesOrderDetailDataModel> | null>(null);

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<ISalesOrderDetailDataModel>(
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

    const orderedListItems = !!listItems ? stableSort(listItems, getComparator(order, orderBy)) as ISalesOrderDetailDataModel[] : [];
    const currentItemOnDialog = !!orderedListItems && orderedListItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < orderedListItems.length ? orderedListItems[currentItemIndex] : null;
    const isSelected = (identifier: ISalesOrderDetailIdentifier) => selected.findIndex(t=> { return compareISalesOrderDetailIdentifier(identifier, t); }) !== -1;
    const headCells: HeadCell[] = [

        {
            id: 'salesOrderID',
            numeric: true,
            disablePadding: true,
            label: t('SalesOrderID'),
        },
        {
            id: 'salesOrderDetailID',
            numeric: true,
            disablePadding: true,
            label: t('SalesOrderDetailID'),
        },
        {
            id: 'orderQty',
            numeric: true,
            disablePadding: true,
            label: t('OrderQty'),
        },
        {
            id: 'productID',
            numeric: true,
            disablePadding: true,
            label: t('ProductID'),
        },
        {
            id: 'unitPrice',
            numeric: true,
            disablePadding: true,
            label: t('UnitPrice'),
        },
        {
            id: 'unitPriceDiscount',
            numeric: true,
            disablePadding: true,
            label: t('UnitPriceDiscount'),
        },
        {
            id: 'lineTotal',
            numeric: true,
            disablePadding: true,
            label: t('LineTotal'),
        },
        {
            id: 'rowguid',
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
        {
            id: 'productCategoryID',
            numeric: true,
            disablePadding: true,
            label: t('ProductCategoryID'),
        },
        {
            id: 'productCategory_ParentID',
            numeric: true,
            disablePadding: true,
            label: t('ProductCategory_ParentID'),
        },
        {
            id: 'productModelID',
            numeric: true,
            disablePadding: true,
            label: t('ProductModelID'),
        },
        {
            id: 'billToID',
            numeric: true,
            disablePadding: true,
            label: t('BillToID'),
        },
        {
            id: 'customerID',
            numeric: true,
            disablePadding: true,
            label: t('CustomerID'),
        },
        {
            id: 'shipToID',
            numeric: true,
            disablePadding: true,
            label: t('ShipToID'),
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
								const key = getRouteParamsOfISalesOrderDetailIdentifier(row);
                                const isItemSelected = isSelected(getISalesOrderDetailIdentifier(row));
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
                                        {!!handleSelectItemClick && <TableCell padding="checkbox">
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
                                            <Link to={"/salesOrderHeader/Details/" + row.salesOrderID}>{row.salesOrderHeader_Name}</Link>
                                        </TableCell>
                                        <TableCell align='right'>{row.salesOrderDetailID}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.number.format, { val: row.orderQty })}</TableCell>
                                        <TableCell align='right'><Link to={"/product/Details/" + row.productID}>{row.product_Name}</Link></TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.unitPrice })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.unitPriceDiscount })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.lineTotal })}</TableCell>
                                        <TableCell align='right'>{row.rowguid}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'><Link to={"/productCategory/Details/" + row.productCategoryID}>{row.productCategory_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/productCategory/Details/" + row.productCategory_ParentID}>{row.productCategory_Parent_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/productModel/Details/" + row.productModelID}>{row.productModel_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/address/Details/" + row.billToID}>{row.billTo_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/customer/Details/" + row.customerID}>{row.customer_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/address/Details/" + row.shipToID}>{row.shipTo_Name}</Link></TableCell>
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
                    <IconButton onClick={() => { navigate("/salesOrderDetail/create") }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
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
                <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/salesOrderDetail/delete/" + getRouteParamsOfISalesOrderDetailIdentifier(currentItemOnDialog)) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { navigate("/salesOrderDetail/details/" + getRouteParamsOfISalesOrderDetailIdentifier(currentItemOnDialog)) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/salesOrderDetail/edit/" + getRouteParamsOfISalesOrderDetailIdentifier(currentItemOnDialog)) }}>
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
                <ItemViewsPartial {...crudItemPartialViewProps} item={currentItemOnDialog} isItemSelected={!!currentItemOnDialog && isSelected(getISalesOrderDetailIdentifier(currentItemOnDialog))} totalCountInList={listItems.length} itemIndex={currentItemIndex} setItemIndex={setCurrentItemIndex} handleSelectItemClick={handleSelectItemClick} />
            </Dialog>
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

