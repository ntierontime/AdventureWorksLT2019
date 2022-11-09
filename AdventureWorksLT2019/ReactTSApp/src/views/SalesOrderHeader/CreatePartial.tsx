import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { ICustomerAdvancedQuery, defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { IAddressAdvancedQuery, defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';

import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultSalesOrderHeader, ISalesOrderHeaderDataModel, salesOrderHeaderFormValidationWhenCreate } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { post } from 'src/slices/SalesOrderHeaderSlice';

export default function CreatePartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<ISalesOrderHeaderDataModel>(defaultSalesOrderHeader());
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, setValue, handleSubmit, reset, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    });

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };



    const [iCustomerAdvancedQuery_CustomerID, setICustomerAdvancedQuery_CustomerID] = useState<ICustomerAdvancedQuery>();
    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.customer_Name, value: item.customerID, selected: false }]);

    const [iAddressAdvancedQuery_ShipToAddressID, setIAddressAdvancedQuery_ShipToAddressID] = useState<IAddressAdvancedQuery>();
    const [address_ShipToAddressIDCodeList, setAddress_ShipToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.shipTo_Name, value: item.shipToAddressID, selected: false }]);

    const [iAddressAdvancedQuery_BillToAddressID, setIAddressAdvancedQuery_BillToAddressID] = useState<IAddressAdvancedQuery>();
    const [address_BillToAddressIDCodeList, setAddress_BillToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.billTo_Name, value: item.billToAddressID, selected: false }]);
    useEffect(() => {


		setICustomerAdvancedQuery_CustomerID({ ...defaultICustomerAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getCustomerCodeList({ ...iCustomerAdvancedQuery_CustomerID }).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
            }
        });

		setIAddressAdvancedQuery_ShipToAddressID({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_ShipToAddressID }).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToAddressIDCodeList(res.responseBody);
            }
        });

		setIAddressAdvancedQuery_BillToAddressID({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_BillToAddressID }).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToAddressIDCodeList(res.responseBody);
            }
        });
        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = () => {
        setCreating(true);
        dispatch(post({ ...item }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
                        setItem(defaultSalesOrderHeader());
                        reset(item);
                    }
                    else {
                        setCreateMessage(t('SuccessfullySaved'));
                        setCreated(true);
                    }
                }
                else { // failed
                    setCreateMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }

    const renderButtonGroupWhenDialog = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        disabled={!isValid || creating || created}
                        startIcon={<SaveIcon />}>
                        {t('Create')}
                    </Button>
                    <Button
                        autoFocus
                        disabled={creating || created}
                        fullWidth
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>
                </ButtonGroup>
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                <IconButton type='submit' aria-label="create" disabled={!isValid || creating || created}>
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        disabled={!isValid || creating || created}
                        startIcon={<SaveIcon />}>
                        {t('Create')}
                    </Button>
                    <IconButton aria-label="close" onClick={() => { doneAction() }}>
                        <ChevronLeftIcon />
                    </IconButton>
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={
                    <>
                        {crudViewContainer === CrudViewContainers.Inline && (renderButtonGroupWhenInline())}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && (renderButtonGroupWhenStandaloneView())}
                    </>
                }
                title={t("Create_New")}
                subheader={t("SalesOrderHeader")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    name='revisionNumber'
                    label={t('RevisionNumber')}
                    defaultValue={item.revisionNumber}
                    variant='outlined'
                    margin='normal'
                    {...register("revisionNumber", salesOrderHeaderFormValidationWhenCreate.revisionNumber)}
                    autoComplete='revisionNumber'
                    error={!!errors.revisionNumber}
                    fullWidth
                    helperText={!!errors.revisionNumber ? t(errors.revisionNumber.message) : ''}
                />
                <Controller
                    name="orderDate"
                    defaultValue={item.orderDate}
                    control={control}
                    {...register("orderDate", salesOrderHeaderFormValidationWhenCreate.orderDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('OrderDate')}
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
                    {...register("dueDate", salesOrderHeaderFormValidationWhenCreate.dueDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('DueDate')}
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
                    {...register("shipDate", salesOrderHeaderFormValidationWhenCreate.shipDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('ShipDate')}
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
                    {...register("status", salesOrderHeaderFormValidationWhenCreate.status)}
                    autoComplete='status'
                    error={!!errors.status}
                    fullWidth
                    helperText={!!errors.status ? t(errors.status.message) : ''}
                />
                <FormControlLabel
                    control={
                        <Controller
                            control={control}
                            name="onlineOrderFlag"
                            defaultValue={item.onlineOrderFlag}
                            {...register("onlineOrderFlag", salesOrderHeaderFormValidationWhenCreate.onlineOrderFlag)}
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
                    name='purchaseOrderNumber'
                    label={t('PurchaseOrderNumber')}
                    defaultValue={item.purchaseOrderNumber}
                    variant='outlined'
                    margin='normal'
                    {...register("purchaseOrderNumber", salesOrderHeaderFormValidationWhenCreate.purchaseOrderNumber)}
                    autoComplete='purchaseOrderNumber'
                    error={!!errors.purchaseOrderNumber}
                    fullWidth
                    helperText={!!errors.purchaseOrderNumber ? t(errors.purchaseOrderNumber.message) : ''}
                />
                <TextField
                    name='accountNumber'
                    label={t('AccountNumber')}
                    defaultValue={item.accountNumber}
                    variant='outlined'
                    margin='normal'
                    {...register("accountNumber", salesOrderHeaderFormValidationWhenCreate.accountNumber)}
                    autoComplete='accountNumber'
                    error={!!errors.accountNumber}
                    fullWidth
                    helperText={!!errors.accountNumber ? t(errors.accountNumber.message) : ''}
                />
                <TextField
                    label={t("CustomerID")}
                    id="customerIDSelect"
                    select
                    name='customerID'
                    {...register("customerID", salesOrderHeaderFormValidationWhenCreate.customerID)}
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
                    {...register("shipToAddressID", salesOrderHeaderFormValidationWhenCreate.shipToAddressID)}
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
                    {...register("billToAddressID", salesOrderHeaderFormValidationWhenCreate.billToAddressID)}
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
                    {...register("shipMethod", salesOrderHeaderFormValidationWhenCreate.shipMethod)}
                    autoComplete='shipMethod'
                    error={!!errors.shipMethod}
                    fullWidth
                    helperText={!!errors.shipMethod ? t(errors.shipMethod.message) : ''}
                />
                <TextField
                    name='creditCardApprovalCode'
                    label={t('CreditCardApprovalCode')}
                    defaultValue={item.creditCardApprovalCode}
                    variant='outlined'
                    margin='normal'
                    {...register("creditCardApprovalCode", salesOrderHeaderFormValidationWhenCreate.creditCardApprovalCode)}
                    autoComplete='creditCardApprovalCode'
                    error={!!errors.creditCardApprovalCode}
                    fullWidth
                    helperText={!!errors.creditCardApprovalCode ? t(errors.creditCardApprovalCode.message) : ''}
                />
                <TextField
                    name='subTotal'
                    label={t('SubTotal')}
                    defaultValue={item.subTotal}
                    variant='outlined'
                    margin='normal'
                    {...register("subTotal", salesOrderHeaderFormValidationWhenCreate.subTotal)}
                    autoComplete='subTotal'
                    error={!!errors.subTotal}
                    fullWidth
                    helperText={!!errors.subTotal ? t(errors.subTotal.message) : ''}
                />
                <TextField
                    name='taxAmt'
                    label={t('TaxAmt')}
                    defaultValue={item.taxAmt}
                    variant='outlined'
                    margin='normal'
                    {...register("taxAmt", salesOrderHeaderFormValidationWhenCreate.taxAmt)}
                    autoComplete='taxAmt'
                    error={!!errors.taxAmt}
                    fullWidth
                    helperText={!!errors.taxAmt ? t(errors.taxAmt.message) : ''}
                />
                <TextField
                    name='freight'
                    label={t('Freight')}
                    defaultValue={item.freight}
                    variant='outlined'
                    margin='normal'
                    {...register("freight", salesOrderHeaderFormValidationWhenCreate.freight)}
                    autoComplete='freight'
                    error={!!errors.freight}
                    fullWidth
                    helperText={!!errors.freight ? t(errors.freight.message) : ''}
                />
                <TextField
                    name='comment'
                    label={t('Comment')}
                    defaultValue={item.comment}
                    variant='outlined'
                    margin='normal'
                    {...register("comment", salesOrderHeaderFormValidationWhenCreate.comment)}
                    autoComplete='comment'
                    error={!!errors.comment}
                    fullWidth
                    helperText={!!errors.comment ? t(errors.comment.message) : ''}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", salesOrderHeaderFormValidationWhenCreate.modifiedDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('ModifiedDate')}
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
            </CardContent>
            {(crudViewContainer === CrudViewContainers.Dialog) && <CardActions disableSpacing>
                {renderButtonGroupWhenDialog()}
            </CardActions>}
        </Card >
    );
}


