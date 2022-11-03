import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';

import { useNavigate } from "react-router-dom";
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { defaultSalesOrderHeader, getSalesOrderHeaderAvatar, ISalesOrderHeaderDataModel, salesOrderHeaderFormValidationWhenEdit } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { put } from 'src/slices/SalesOrderHeaderSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, handleSubmit, reset, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultSalesOrderHeader(),
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



    const iCustomerAdvancedQuery_CustomerID = defaultICustomerAdvancedQuery();
    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{name: item.customer_Name, value: item.customerID, selected: false}]);

    const iAddressAdvancedQuery_ShipToAddressID = defaultIAddressAdvancedQuery();
    const [address_ShipToAddressIDCodeList, setAddress_ShipToAddressIDCodeList] = useState<readonly INameValuePair[]>([{name: item.shipTo_Name, value: item.shipToAddressID, selected: false}]);

    const iAddressAdvancedQuery_BillToAddressID = defaultIAddressAdvancedQuery();
    const [address_BillToAddressIDCodeList, setAddress_BillToAddressIDCodeList] = useState<readonly INameValuePair[]>([{name: item.billTo_Name, value: item.billToAddressID, selected: false}]);
    useEffect(() => {


        codeListsApi.getCustomerCodeList({ ...iCustomerAdvancedQuery_CustomerID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_ShipToAddressID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToAddressIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_BillToAddressID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToAddressIDCodeList(res.responseBody);
            }
        });
        reset(item);
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const onSubmit = (data: ISalesOrderHeaderDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { salesOrderID: data.salesOrderID }, data: { ...data } }))
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

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderNumber}>
                        {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <>
                            {!!handleSelectItemClick && <Checkbox
                                color="primary"
                                checked={isItemSelected}
                                onChange={() => { handleSelectItemClick(item) }}
                            />}
                            {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                                <DeleteIcon />
                            </IconButton>}
                            {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                                <CloseIcon />
                            </IconButton>}
                        </>}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && <>
                            <LoadingButton
                                color="primary"
                                type='submit'
                                variant='contained'
                                disabled={!isValid || saving || saved}
                                startIcon={<SaveIcon color='action' />}>
                                {t('Save')}
                            </LoadingButton>
                            <IconButton aria-label="close"
                                onClick={() => {
                                    navigate(-1);
                                }} disabled={saving}
                            >
                                <CloseIcon />
                            </IconButton>
                        </>}
                    </>
                }
                title={item.salesOrderNumber}
                subheader={t('{{val, datetime}}', { val: new Date(item.orderDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderNumber}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    name='salesOrderID'
                    label={t('SalesOrderID')}
                	value={item.salesOrderID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
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
                    autoFocus
                    helperText={!!errors.revisionNumber ? t(errors.revisionNumber.message) : ''}
                />
                <Controller
                    name="orderDate"
                    defaultValue={item.orderDate}
                    control={control}
                    {...register("orderDate", salesOrderHeaderFormValidationWhenEdit.orderDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                				ref={null}
                                label={t('OrderDate')}
                                autoFocus
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                						ref={null}
                                        fullWidth
                                        autoComplete='orderDate'
                                        error={!!errors.orderDate}
                						helperText={!!errors.orderDate ? t(errors.orderDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <Controller
                    name="dueDate"
                    defaultValue={item.dueDate}
                    control={control}
                    {...register("dueDate", salesOrderHeaderFormValidationWhenEdit.dueDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                				ref={null}
                                label={t('DueDate')}
                                autoFocus
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                						ref={null}
                                        fullWidth
                                        autoComplete='dueDate'
                                        error={!!errors.dueDate}
                						helperText={!!errors.dueDate ? t(errors.dueDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <Controller
                    name="shipDate"
                    defaultValue={item.shipDate}
                    control={control}
                    {...register("shipDate", salesOrderHeaderFormValidationWhenEdit.shipDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                				ref={null}
                                label={t('ShipDate')}
                                autoFocus
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                						ref={null}
                                        fullWidth
                                        autoComplete='shipDate'
                                        error={!!errors.shipDate}
                						//helperText={!!errors.shipDate ? t(errors.shipDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
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
                    autoFocus
                    helperText={!!errors.status ? t(errors.status.message) : ''}
                />
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
                <TextField
                    name='salesOrderNumber'
                    label={t('SalesOrderNumber')}
                    value={item.salesOrderNumber}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
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
                    {...register("purchaseOrderNumber", salesOrderHeaderFormValidationWhenEdit.purchaseOrderNumber)}
                    autoComplete='purchaseOrderNumber'
                    error={!!errors.purchaseOrderNumber}
                    fullWidth
                    autoFocus
                    helperText={!!errors.purchaseOrderNumber ? t(errors.purchaseOrderNumber.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.accountNumber ? t(errors.accountNumber.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.shipMethod ? t(errors.shipMethod.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.creditCardApprovalCode ? t(errors.creditCardApprovalCode.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.subTotal ? t(errors.subTotal.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.taxAmt ? t(errors.taxAmt.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.freight ? t(errors.freight.message) : ''}
                />
                <TextField
                    name='totalDue'
                    label={t('TotalDue')}
                	value={t(i18nFormats.double.format, { val: item.totalDue })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
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
                    {...register("comment", salesOrderHeaderFormValidationWhenEdit.comment)}
                    autoComplete='comment'
                    error={!!errors.comment}
                    fullWidth
                    autoFocus
                    helperText={!!errors.comment ? t(errors.comment.message) : ''}
                />
                <TextField
                    name='_rowguid'
                    label={t('rowguid')}
                	value={item._rowguid}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", salesOrderHeaderFormValidationWhenEdit.modifiedDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                				ref={null}
                                label={t('ModifiedDate')}
                                autoFocus
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                						ref={null}
                                        fullWidth
                                        autoComplete='modifiedDate'
                                        error={!!errors.modifiedDate}
                						helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <TextField
                    name='billTo_Name'
                    label={t('BillTo_Name')}
                    value={item.billTo_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='customer_Name'
                    label={t('Customer_Name')}
                    value={item.customer_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='shipTo_Name'
                    label={t('ShipTo_Name')}
                    value={item.shipTo_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
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
                        disabled={!isValid || saving || saved}
                        startIcon={<SaveIcon color='action' />}>
                        {t('Save')}
                    </LoadingButton>
                    {!!doneAction && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
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


