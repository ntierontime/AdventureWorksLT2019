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

import { IProductDataModel } from 'src/dataModels/IProductDataModel';
import { IProductIdentifier, getIProductIdentifier, compareIProductIdentifier, getRouteParamsOfIProductIdentifier } from 'src/dataModels/IProductQueries';
import ItemViewsPartial from './ItemViewsPartial';

export default function HtmlTablePartial(props: ListPartialViewProps<IProductDataModel, IProductIdentifier>): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const { listItems, selected, numSelected, handleChangePage, handleSelectItemClick } = props;
    const [order, setOrder] = useState<QueryOrderDirections>('asc');
    const [orderBy, setOrderBy] = useState<keyof IProductDataModel>('sellStartDate');
    const [dense, setDense] = useState(true);
    const { pagination } = useSelector((state: RootState) => state.productList);
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
        property: keyof IProductDataModel,
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
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<IProductDataModel> | null>(null);

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IProductDataModel>(
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

    const orderedListItems = !!listItems ? stableSort(listItems, getComparator(order, orderBy)) as IProductDataModel[] : [];
    const currentItemOnDialog = !!orderedListItems && orderedListItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < orderedListItems.length ? orderedListItems[currentItemIndex] : null;
    const isSelected = (identifier: IProductIdentifier) => selected.findIndex(t=> { return compareIProductIdentifier(identifier, t); }) !== -1;
    const headCells: HeadCell[] = [

        {
            id: 'productID',
            numeric: true,
            disablePadding: true,
            label: t('ProductID'),
        },
        {
            id: 'name',
            numeric: false,
            disablePadding: true,
            label: t('Name'),
        },
        {
            id: 'productNumber',
            numeric: false,
            disablePadding: true,
            label: t('ProductNumber'),
        },
        {
            id: 'color',
            numeric: false,
            disablePadding: true,
            label: t('Color'),
        },
        {
            id: 'standardCost',
            numeric: true,
            disablePadding: true,
            label: t('StandardCost'),
        },
        {
            id: 'listPrice',
            numeric: true,
            disablePadding: true,
            label: t('ListPrice'),
        },
        {
            id: 'size',
            numeric: false,
            disablePadding: true,
            label: t('Size'),
        },
        {
            id: 'weight',
            numeric: true,
            disablePadding: true,
            label: t('Weight'),
        },
        {
            id: 'productCategoryID',
            numeric: true,
            disablePadding: true,
            label: t('ProductCategoryID'),
        },
        {
            id: 'productModelID',
            numeric: true,
            disablePadding: true,
            label: t('ProductModelID'),
        },
        {
            id: 'sellStartDate',
            numeric: false,
            disablePadding: true,
            label: t('SellStartDate'),
        },
        {
            id: 'sellEndDate',
            numeric: false,
            disablePadding: true,
            label: t('SellEndDate'),
        },
        {
            id: 'discontinuedDate',
            numeric: false,
            disablePadding: true,
            label: t('DiscontinuedDate'),
        },
        {
            id: 'thumbNailPhoto',
            numeric: false,
            disablePadding: true,
            label: t('ThumbNailPhoto'),
        },
        {
            id: 'thumbnailPhotoFileName',
            numeric: false,
            disablePadding: true,
            label: t('ThumbnailPhotoFileName'),
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
        {
            id: 'parentID',
            numeric: true,
            disablePadding: true,
            label: t('ParentID'),
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
                                const isItemSelected = isSelected(getIProductIdentifier(row));
                                const labelId = `enhanced-table-checkbox-${index}`;

                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        aria-checked={isItemSelected}
                                        tabIndex={-1}
                                        key={getRouteParamsOfIProductIdentifier(row)}
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
                                            {row.productID}
                                        </TableCell>
                                        <TableCell align='right'>{row.name}</TableCell>
                                        <TableCell align='right'>{row.productNumber}</TableCell>
                                        <TableCell align='right'>{row.color}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.standardCost })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.listPrice })}</TableCell>
                                        <TableCell align='right'>{row.size}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.double.format, { val: row.weight })}</TableCell>
                                        <TableCell align='right'><Link to={"/productCategory/Details/" + row.productCategoryID}>{row.productCategory_Name}</Link></TableCell>
                                        <TableCell align='right'><Link to={"/productModel/Details/" + row.productModelID}>{row.productModel_Name}</Link></TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.sellStartDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.sellEndDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.discontinuedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'>{row.thumbNailPhoto}</TableCell>
                                        <TableCell align='right'>{row.thumbnailPhotoFileName}</TableCell>
                                        <TableCell align='right'>{row._rowguid}</TableCell>
                                        <TableCell align='right'>{t(i18nFormats.dateTime.format, { val: new Date(row.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}</TableCell>
                                        <TableCell align='right'><Link to={"/productCategory/Details/" + row.parentID}>{row.parent_Name}</Link></TableCell>
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
                    <IconButton onClick={() => { navigate("/product/create") }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
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
                <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/product/delete/" + getRouteParamsOfIProductIdentifier(currentItemOnDialog)) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="details" color="primary" onClick={() => { navigate("/product/details/" + getRouteParamsOfIProductIdentifier(currentItemOnDialog)) }}>
                    <BusinessCenterIcon />
                </IconButton>
                <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/product/edit/" + getRouteParamsOfIProductIdentifier(currentItemOnDialog)) }}>
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
                <ItemViewsPartial {...crudItemPartialViewProps} item={currentItemOnDialog} isItemSelected={!!currentItemOnDialog && isSelected(getIProductIdentifier(currentItemOnDialog))} totalCountInList={listItems.length} itemIndex={currentItemIndex} setItemIndex={setCurrentItemIndex} handleSelectItemClick={handleSelectItemClick} />
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

