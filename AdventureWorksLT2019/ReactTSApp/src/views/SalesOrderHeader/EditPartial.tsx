import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderHeaderAvatar, ISalesOrderHeaderDataModel, salesOrderHeaderFormValidationWhenEdit } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { getISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';
import { put } from 'src/slices/SalesOrderHeaderSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.customer_Name, value: item.customerID, selected: false }]);

    const [address_ShipToAddressIDCodeList, setAddress_ShipToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.shipTo_Name, value: item.shipToAddressID, selected: false }]);

    const [address_BillToAddressIDCodeList, setAddress_BillToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.billTo_Name, value: item.billToAddressID, selected: false }]);
    const [orderDate, setOrderDate] = useState<string>();
    const [dueDate, setDueDate] = useState<string>();
    const [shipDate, setShipDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getCustomerCodeList({ ...defaultICustomerAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToAddressIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToAddressIDCodeList(res.responseBody);
            }
        });
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = (data: ISalesOrderHeaderDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: getISalesOrderHeaderIdentifier(data), data: { ...data } }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getSalesOrderHeaderAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
				{(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        disabled={saving}
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        disabled={saving}
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                <IconButton aria-label="Save"
                    color="primary"
                    type='submit'
                    disabled={!isValid || saving || saved || !isDirty}
                >
                    <SaveIcon />
                </IconButton>
                {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
                {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <ArrowBackIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
        return (
            <>
				{(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        disabled={saving}
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        disabled={saving}
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <LoadingButton
                        color="primary"
                        type='submit'
                        variant='contained'
                        disabled={!isValid || saving || saved || !isDirty}
                        startIcon={<SaveIcon color='action' />}>
                        {t('Save')}
                    </LoadingButton>
                    {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                    {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<ArrowBackIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Back')}
                    </Button>}
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderNumber}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.salesOrderNumber}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderNumber}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='revisionNumber'
                                label={t('RevisionNumber')}
                                defaultValue={item.revisionNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("revisionNumber", salesOrderHeaderFormValidationWhenEdit.revisionNumber)}
                                autoComplete='revisionNumber'
                                error={!!errors.revisionNumber}
                                fullWidth
                                helperText={!!errors.revisionNumber ? t(errors.revisionNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={orderDate}
                                label={t('OrderDate')}
                                onChange={(event: string) => { setOrderDate(event); setValue('orderDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='orderDate'
                            			{...register("orderDate", salesOrderHeaderFormValidationWhenEdit.orderDate)}
                                        error={!!errors.orderDate}
                                        helperText={!!errors.orderDate ? t(errors.orderDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={dueDate}
                                label={t('DueDate')}
                                onChange={(event: string) => { setDueDate(event); setValue('dueDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='dueDate'
                            			{...register("dueDate", salesOrderHeaderFormValidationWhenEdit.dueDate)}
                                        error={!!errors.dueDate}
                                        helperText={!!errors.dueDate ? t(errors.dueDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={shipDate}
                                label={t('ShipDate')}
                                onChange={(event: string) => { setShipDate(event); setValue('shipDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='shipDate'
                            			{...register("shipDate", salesOrderHeaderFormValidationWhenEdit.shipDate)}
                                        error={!!errors.shipDate}
                                        helperText={!!errors.shipDate ? t(errors.shipDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='status'
                                label={t('Status')}
                                defaultValue={item.status}
                                variant='outlined'
                                margin='normal'
                                {...register("status", salesOrderHeaderFormValidationWhenEdit.status)}
                                autoComplete='status'
                                error={!!errors.status}
                                fullWidth
                                helperText={!!errors.status ? t(errors.status.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <FormControlLabel
                                control={
                                    <Controller
                                        control={control}
                                        name="onlineOrderFlag"
                                        defaultValue={item.onlineOrderFlag}
                                        {...register("onlineOrderFlag", salesOrderHeaderFormValidationWhenEdit.onlineOrderFlag)}
                                        render={({ field: { onChange } }) => (
                                            <Checkbox
                                                color="primary"
                                                onChange={(e) => onChange(e.target.checked)}
                                            />
                                        )}
                                    />
                                }
                                label={
                                    <Typography>{t('OnlineOrderFlag')}</Typography>
                                }
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='purchaseOrderNumber'
                                label={t('PurchaseOrderNumber')}
                                defaultValue={item.purchaseOrderNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("purchaseOrderNumber", salesOrderHeaderFormValidationWhenEdit.purchaseOrderNumber)}
                                autoComplete='purchaseOrderNumber'
                                error={!!errors.purchaseOrderNumber}
                                fullWidth
                                helperText={!!errors.purchaseOrderNumber ? t(errors.purchaseOrderNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='accountNumber'
                                label={t('AccountNumber')}
                                defaultValue={item.accountNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("accountNumber", salesOrderHeaderFormValidationWhenEdit.accountNumber)}
                                autoComplete='accountNumber'
                                error={!!errors.accountNumber}
                                fullWidth
                                helperText={!!errors.accountNumber ? t(errors.accountNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                label={t("CustomerID")}
                                id="customerIDSelect"
                                select
                                name='customerID'
                                {...register("customerID", salesOrderHeaderFormValidationWhenEdit.customerID)}
                                autoComplete='customerID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.customerID}
                            >
                                {customer_CustomerIDCodeList && customer_CustomerIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                label={t("ShipToAddressID")}
                                id="shipToAddressIDSelect"
                                select
                                name='shipToAddressID'
                                {...register("shipToAddressID", salesOrderHeaderFormValidationWhenEdit.shipToAddressID)}
                                autoComplete='shipToAddressID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.shipToAddressID}
                            >
                                {address_ShipToAddressIDCodeList && address_ShipToAddressIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                label={t("BillToAddressID")}
                                id="billToAddressIDSelect"
                                select
                                name='billToAddressID'
                                {...register("billToAddressID", salesOrderHeaderFormValidationWhenEdit.billToAddressID)}
                                autoComplete='billToAddressID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.billToAddressID}
                            >
                                {address_BillToAddressIDCodeList && address_BillToAddressIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='shipMethod'
                                label={t('ShipMethod')}
                                defaultValue={item.shipMethod}
                                variant='outlined'
                                margin='normal'
                                {...register("shipMethod", salesOrderHeaderFormValidationWhenEdit.shipMethod)}
                                autoComplete='shipMethod'
                                error={!!errors.shipMethod}
                                fullWidth
                                helperText={!!errors.shipMethod ? t(errors.shipMethod.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='creditCardApprovalCode'
                                label={t('CreditCardApprovalCode')}
                                defaultValue={item.creditCardApprovalCode}
                                variant='outlined'
                                margin='normal'
                                {...register("creditCardApprovalCode", salesOrderHeaderFormValidationWhenEdit.creditCardApprovalCode)}
                                autoComplete='creditCardApprovalCode'
                                error={!!errors.creditCardApprovalCode}
                                fullWidth
                                helperText={!!errors.creditCardApprovalCode ? t(errors.creditCardApprovalCode.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='subTotal'
                                label={t('SubTotal')}
                                defaultValue={item.subTotal}
                                variant='outlined'
                                margin='normal'
                                {...register("subTotal", salesOrderHeaderFormValidationWhenEdit.subTotal)}
                                autoComplete='subTotal'
                                error={!!errors.subTotal}
                                fullWidth
                                helperText={!!errors.subTotal ? t(errors.subTotal.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='taxAmt'
                                label={t('TaxAmt')}
                                defaultValue={item.taxAmt}
                                variant='outlined'
                                margin='normal'
                                {...register("taxAmt", salesOrderHeaderFormValidationWhenEdit.taxAmt)}
                                autoComplete='taxAmt'
                                error={!!errors.taxAmt}
                                fullWidth
                                helperText={!!errors.taxAmt ? t(errors.taxAmt.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='freight'
                                label={t('Freight')}
                                defaultValue={item.freight}
                                variant='outlined'
                                margin='normal'
                                {...register("freight", salesOrderHeaderFormValidationWhenEdit.freight)}
                                autoComplete='freight'
                                error={!!errors.freight}
                                fullWidth
                                helperText={!!errors.freight ? t(errors.freight.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='comment'
                                label={t('Comment')}
                                defaultValue={item.comment}
                                variant='outlined'
                                margin='normal'
                                {...register("comment", salesOrderHeaderFormValidationWhenEdit.comment)}
                                autoComplete='comment'
                                error={!!errors.comment}
                                fullWidth
                                helperText={!!errors.comment ? t(errors.comment.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={modifiedDate}
                                label={t('ModifiedDate')}
                                onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='modifiedDate'
                            			{...register("modifiedDate", salesOrderHeaderFormValidationWhenEdit.modifiedDate)}
                                        error={!!errors.modifiedDate}
                                        helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}

