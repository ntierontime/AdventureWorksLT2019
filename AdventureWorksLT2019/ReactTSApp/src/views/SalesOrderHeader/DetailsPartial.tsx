import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from "react-router-dom";
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { Link } from 'react-router-dom';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderHeaderAvatar, ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { getRouteParamsOfISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';

export default function DetailsPartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getSalesOrderHeaderAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/SalesOrderHeader/Dashboard/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
				{!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <ChevronLeftIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenDialog = () => {
        return (
            <>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/SalesOrderHeader/Dashboard/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
                {!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
                {crudViewContainer === CrudViewContainers.Dialog && <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/SalesOrderHeader/Dashboard/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                {!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <IconButton aria-label="close"
                    onClick={() => {
                        navigate(-1);
                    }}
                >
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    return (
        <Card sx={{ minHeight: '100%' }}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderNumber}>
                        {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {crudViewContainer === CrudViewContainers.Card && (renderButtonGroupWhenCard())}
                        {crudViewContainer === CrudViewContainers.Dialog && (renderButtonGroupWhenDialog())}
                        {crudViewContainer === CrudViewContainers.Inline && (renderButtonGroupWhenInline())}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && (renderButtonGroupWhenStandaloneView())}
                    </>
                }
                title={item.salesOrderNumber}
                subheader={t('{{val, datetime}}', { val: new Date(item.orderDate) })}
            />
            <CardContent>
                <TextField
                    name='salesOrderID'
                    label={t('SalesOrderID')}
                    value={item.salesOrderID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='revisionNumber'
                    label={t('RevisionNumber')}
                    value={t(i18nFormats.number.format, { val: item.revisionNumber })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <DatePicker
                    label={t('OrderDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => { }}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='orderDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <DatePicker
                    label={t('DueDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.dueDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => { }}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='dueDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <DatePicker
                    label={t('ShipDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.shipDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => { }}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='shipDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <TextField
                    name='status'
                    label={t('Status')}
                    value={t(i18nFormats.number.format, { val: item.status })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <FormControlLabel control={<Checkbox checked={item.onlineOrderFlag} readOnly />} label={t('OnlineOrderFlag')} />
                <TextField
                    name='salesOrderNumber'
                    label={t('SalesOrderNumber')}
                    defaultValue={item.salesOrderNumber}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='purchaseOrderNumber'
                    label={t('PurchaseOrderNumber')}
                    defaultValue={item.purchaseOrderNumber}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='accountNumber'
                    label={t('AccountNumber')}
                    defaultValue={item.accountNumber}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("CustomerID")}</Typography>
                    <Link to={"/customer/Details/" + item.customerID}>{item.customer_Name}</Link>
                </Stack>
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("ShipToAddressID")}</Typography>
                    <Link to={"/address/Details/" + item.shipToAddressID}>{item.shipTo_Name}</Link>
                </Stack>
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("BillToAddressID")}</Typography>
                    <Link to={"/address/Details/" + item.billToAddressID}>{item.billTo_Name}</Link>
                </Stack>
                <TextField
                    name='shipMethod'
                    label={t('ShipMethod')}
                    defaultValue={item.shipMethod}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='creditCardApprovalCode'
                    label={t('CreditCardApprovalCode')}
                    defaultValue={item.creditCardApprovalCode}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='subTotal'
                    label={t('SubTotal')}
                    value={t(i18nFormats.double.format, { val: item.subTotal })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='taxAmt'
                    label={t('TaxAmt')}
                    value={t(i18nFormats.double.format, { val: item.taxAmt })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='freight'
                    label={t('Freight')}
                    value={t(i18nFormats.double.format, { val: item.freight })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='totalDue'
                    label={t('TotalDue')}
                    value={t(i18nFormats.double.format, { val: item.totalDue })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='comment'
                    label={t('Comment')}
                    defaultValue={item.comment}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='rowguid'
                    label={t('rowguid')}
                    value={item.rowguid}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <DatePicker
                    label={t('ModifiedDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => { }}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='modifiedDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
            </CardContent>
            {crudViewContainer === CrudViewContainers.Dialog && <CardActions disableSpacing>
                {(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                <ButtonGroup sx={{ marginLeft: 'auto' }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button sx={{ marginLeft: 'auto' }}
                        color="primary"
                        autoFocus
                        variant='contained'
                        startIcon={<CloseIcon color='action' />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>
                </ButtonGroup>
            </CardActions>}
        </Card >
    );
}

