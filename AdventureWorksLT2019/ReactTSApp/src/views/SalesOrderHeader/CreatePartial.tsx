import { useEffect, useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';

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
import { ISalesOrderHeaderDataModel, salesOrderHeaderFormValidationWhenCreate } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { post } from 'src/slices/SalesOrderHeaderSlice';

export default function CreatePartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const { t } = useTranslation();
    // #region 1.start redux-hook-form related
    const { item } = props;
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props;
    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(salesOrderHeaderFormValidationWhenCreate)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;
    // #endregion 1. redux-hook-form related

    // #region 2. CodeLists if any
	

    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.customer_Name, value: item.customerID, selected: false }]);

    const [address_ShipToAddressIDCodeList, setAddress_ShipToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.shipTo_Name, value: item.shipToAddressID, selected: false }]);

    const [address_BillToAddressIDCodeList, setAddress_BillToAddressIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.billTo_Name, value: item.billToAddressID, selected: false }]);
    const [orderDate, setOrderDate] = useState<string>();
    const [dueDate, setDueDate] = useState<string>();
    const [shipDate, setShipDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();






    // #endregion 2. CodeLists if any

    // #region 3. crudViewContainer !== CrudViewContainers.Wizard
    const { doneAction } = props; // dialog
    const dispatch = useDispatch<AppDispatch>();

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };

    const onSubmit = (data: ISalesOrderHeaderDataModel) => {
        if(crudViewContainer === CrudViewContainers.Wizard) {
            onWizardStepSubmit(data);
            return;
        }

        setCreating(true);
        dispatch(post({ ...data }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
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
            .catch((error: any) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || creating || created}>
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" disabled={creating || created}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
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
	// #endregion 3. crudViewContainer !== CrudViewContainers.Wizard
	
    // #region 4. crudViewContainer === CrudViewContainers.Wizard
    const { wizardOrientation, onWizardStepSubmit, renderWizardButtonGroup, isFirstStep, isLastStep, isStepOptional } = props;
	const submitRef = useRef(); // used for external trigger submit event.
    // #endregion 4. crudViewContainer === CrudViewContainers.Wizard

    useEffect(() => {


        codeListsApi.getCustomerCodeList({ ...defaultICustomerAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
                setValue('customerID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getAddressCodeList({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToAddressIDCodeList(res.responseBody);
                setValue('shipToAddressID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getAddressCodeList({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToAddressIDCodeList(res.responseBody);
                setValue('billToAddressID', res.responseBody[0].value);
				
            }
        });
        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [item]);
	
	useEffect(() => {
        // console.log("trigger validation");
        trigger();
    }, [trigger]);

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)} sx={{height: '100%', display: "flex", flexDirection: "column",}}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("SalesOrderHeader")}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && !!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='revisionNumber'
                                label={t('RevisionNumber')}
                                defaultValue={item.revisionNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("revisionNumber")}
                                autoComplete='revisionNumber'
                                error={!!errors.revisionNumber}
                                fullWidth
                                helperText={!!errors.revisionNumber ? t(errors.revisionNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="orderDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={orderDate}
                            				label={t('OrderDate')}
                            				onChange={(event: string) => { setOrderDate(event); setValue('orderDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.orderDate}
                            						helperText={!!errors.orderDate ? t(errors.orderDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="dueDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={dueDate}
                            				label={t('DueDate')}
                            				onChange={(event: string) => { setDueDate(event); setValue('dueDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.dueDate}
                            						helperText={!!errors.dueDate ? t(errors.dueDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="shipDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={shipDate}
                            				label={t('ShipDate')}
                            				onChange={(event: string) => { setShipDate(event); setValue('shipDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.shipDate}
                            						helperText={!!errors.shipDate ? t(errors.shipDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='status'
                                label={t('Status')}
                                defaultValue={item.status}
                                variant='outlined'
                                margin='normal'
                                {...register("status")}
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
                            			{...register("onlineOrderFlag")}
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
                                name='purchaseOrderNumber'
                                label={t('PurchaseOrderNumber')}
                                defaultValue={item.purchaseOrderNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("purchaseOrderNumber")}
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
                                {...register("accountNumber")}
                                autoComplete='accountNumber'
                                error={!!errors.accountNumber}
                                fullWidth
                                helperText={!!errors.accountNumber ? t(errors.accountNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("CustomerID")}
                                id="customerIDSelect"
                                select
                                name='customerID'
                                {...register("customerID")}
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
                            	sx={{marginTop: 2}}
                                label={t("ShipToAddressID")}
                                id="shipToAddressIDSelect"
                                select
                                name='shipToAddressID'
                                {...register("shipToAddressID")}
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
                            	sx={{marginTop: 2}}
                                label={t("BillToAddressID")}
                                id="billToAddressIDSelect"
                                select
                                name='billToAddressID'
                                {...register("billToAddressID")}
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
                                {...register("shipMethod")}
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
                                {...register("creditCardApprovalCode")}
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
                                {...register("subTotal")}
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
                                {...register("taxAmt")}
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
                                {...register("freight")}
                                autoComplete='freight'
                                error={!!errors.freight}
                                fullWidth
                                helperText={!!errors.freight ? t(errors.freight.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='comment'
                                label={t('Comment')}
                                defaultValue={item.comment}
                                variant='outlined'
                                margin='normal'
                                {...register("comment")}
                                autoComplete='comment'
                                error={!!errors.comment}
                                fullWidth
                                helperText={!!errors.comment ? t(errors.comment.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="modifiedDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={modifiedDate}
                            				label={t('ModifiedDate')}
                            				onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.modifiedDate}
                            						helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                    </Grid>
                </Box>
            </CardContent>
            {crudViewContainer != CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
            {crudViewContainer === CrudViewContainers.Wizard && <CardActions disableSpacing sx={{ mt: "auto" }}>
                <button ref={submitRef} type="submit" style={{ display: 'none' }} />{renderWizardButtonGroup(isFirstStep, isLastStep, isStepOptional, ()=>!isValid || creating || created, submitRef)}
            </CardActions>}
        </Card >
    );
}

