import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { Link } from 'react-router-dom';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderHeaderAvatar, ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { delete1 } from 'src/slices/SalesOrderHeaderSlice';

export default function DeletePartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const navigate = useNavigate();
    
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const [deleting, setDeleting] = useState(false);
    const [deleted, setDeleted] = useState(false);

    const [deleteMessage, setDeleteMessage] = useState<string>();

    useEffect(() => {
        setDeleting(false);
        setDeleted(false);
        setDeleteMessage(t('Are_you_sure_you_want_to_delete_this?'));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [item]);

    const onDelete = () => {
        setDeleting(true)
        setDeleteMessage(t('Deleting'));
        dispatch(delete1({ salesOrderID: item.salesOrderID }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setDeleteMessage(item.salesOrderNumber + " " + t('Deleted') + "!");
                    setDeleted(true);
                }
                else { // failed
                    setDeleteMessage(t('DeletionFailed'));
                }
                //console.log(result);
            })
            .catch((error) => { setDeleteMessage(t('DeletionFailed')); /*console.log(error);*/ })
            .finally(() => { setDeleting(true); });
    }

    const theme = useTheme();
    const avatar = getSalesOrderHeaderAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton 
                    color="primary"
                    disabled={deleted}
                    aria-label="delete" 
                    onClick={() => { onDelete() }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
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

                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit); }} disabled={deleting || deleted}>
                    <EditIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}

                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit); }} disabled={deleting || deleted}>
                    <EditIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <LoadingButton
                    color='primary'
                    variant='contained'
                    loading={deleting}
                    disabled={deleted}
                    startIcon={<DeleteIcon color='action' />}
                    onClick={() => { onDelete(); }}>
                    {t('Delete')}
                </LoadingButton>
                <IconButton aria-label="close"
                    onClick={() => {
                        navigate(-1);
                    }} disabled={deleting}
                >
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    return (
        <Card>
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
            <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="subtitle1">
                    {deleteMessage}
                </Typography>
            </CardContent>
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
            {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <CardActions disableSpacing>
                {(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        disabled={deleting}
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        disabled={deleting}
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
                    <LoadingButton
                        color='primary'
                        variant='contained'
                        loading={deleting}
                        disabled={deleted}
                        startIcon={<DeleteIcon color='action' />}
                        onClick={() => { onDelete(); }}>
                        {t('Delete')}
                    </LoadingButton>
                    {!!doneAction && <Button sx={{ marginLeft: 'auto' }}
                        color='secondary'
                        autoFocus
                        disabled={deleting}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                </ButtonGroup>
            </CardActions>}
        </Card >
    );
}

