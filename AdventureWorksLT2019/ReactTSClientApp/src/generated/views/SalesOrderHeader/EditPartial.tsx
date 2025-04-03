import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, Dialog, FormControlLabel, FormLabel, Grid, IconButton, MenuItem, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';
import dayjs from 'dayjs';
import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { card100PercentHeighFlex, ItemCardProps } from 'src/shared/views/ItemCardProps';
import { getAvatar, getAvatarStyle } from 'src/shared/avatarUtility';
import { getTitle } from 'src/shared/captionTextUtility';
import { EditItemActionIconButtonGroup, EditItemActionIconAndTextButtonGroup } from 'src/shared/views/buttonGroups/EditItemActionButtonGroups';
import { ButtonTypes } from 'src/shared/views/buttonGroups/ButtonTypes';
import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { RatingSummary } from 'src/shared/views/rating/RatingSummary';

import { ISalesOrderHeaderDataModel, salesOrderHeaderFormValidation } from 'src/dataModels/ISalesOrderHeaderDataModel';


export default function EditPartial(props: ItemCardProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const { t } = useTranslation();

    const {
        mainButtonContainer, mainButtonType,
        gridColumns, scrollableCardContent,
        item, itemIndex,
        submitAction, submitting, submitted, submitMessage,
        doneAction, previousAction, nextAction,
        showCloseIconOnTopRight, showCardHeader,
        isItemSelected, handleSelectItemClick,
    } = props;

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(salesOrderHeaderFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;




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
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: ISalesOrderHeaderDataModel) => {
		const dataWithProperDateTimeFormat = {...data, orderDate: dayjs(orderDate).toString(), dueDate: dayjs(dueDate).toString(), shipDate: dayjs(shipDate).toString(), modifiedDate: dayjs(modifiedDate).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const salesOrderNumber_Watched = watch("salesOrderNumber");
    const orderDate_Watched = watch("orderDate");
    const avatar = getAvatar([salesOrderNumber_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([salesOrderNumber_Watched]);
    const subheader = !!orderDate_Watched
        ? t(i18nFormats.dateTime.format, { val: new Date(orderDate_Watched), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;


    const renderMainButtonGroup = () => {
        const extraButtonGroups = (
            <>

            </>
        );

        if (mainButtonType === ButtonTypes.IconAndText) {
            return (
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="SalesOrderHeaderEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="SalesOrderHeaderEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="SalesOrderHeaderEditForm" sx={{ ...card100PercentHeighFlex }} >
            {showCardHeader && <CardHeader
                avatar={
                    <Avatar
                        sx={avatarStyle}>
                            {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {mainButtonContainer === CardButtonGroupPosition.CardHeader && <>
                            {renderMainButtonGroup()}
                        </>}
                        {(showCloseIconOnTopRight && !!doneAction) && <IconButton aria-label="edit" color="primary" onClick={() => { doneAction() }}>
                            <CloseIcon />
                        </IconButton>}
                    </>}
                title={title}
                subheader={subheader}
            />}
            {mainButtonContainer === CardButtonGroupPosition.BelowCardHeader && <CardActions disableSpacing>
                {renderMainButtonGroup()}
            </CardActions>}
            {!!submitMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {submitMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderNumber}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
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
                        defaultValue={item.revisionNumber}
                        variant='outlined'
                        margin='normal'
                        {...register("revisionNumber")}
                        autoComplete='revisionNumber'
                        error={!!errors.revisionNumber}
                        fullWidth
                        helperText={!!errors.revisionNumber ? t(errors.revisionNumber.message) : ''}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={orderDate}
                        // value={orderDate}
                        label={t('OrderDate')}
                        onChange={(event: string) => { setOrderDate(event); setValue('orderDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={dueDate}
                        // value={dueDate}
                        label={t('DueDate')}
                        onChange={(event: string) => { setDueDate(event); setValue('dueDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={shipDate}
                        // value={shipDate}
                        label={t('ShipDate')}
                        onChange={(event: string) => { setShipDate(event); setValue('shipDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
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
                    <Controller
                        name='onlineOrderFlag'
                        control={control}
                        defaultValue={item.onlineOrderFlag}
                        render={({ field }) => (
                            <FormControlLabel
                                control={<Checkbox {...field} />}
                                label={
                                    <Typography>{t('OnlineOrderFlag')}</Typography>
                                }
                            />
                        )}
                    />
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
                        {...register("purchaseOrderNumber")}
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
                        {...register("accountNumber")}
                        autoComplete='accountNumber'
                        error={!!errors.accountNumber}
                        fullWidth
                        helperText={!!errors.accountNumber ? t(errors.accountNumber.message) : ''}
                    />
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
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
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
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
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
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
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
                        {...register("comment")}
                        autoComplete='comment'
                        error={!!errors.comment}
                        fullWidth
                        helperText={!!errors.comment ? t(errors.comment.message) : ''}
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
                        sx={{ minWidth: "100%" }}
                        defaultValue={modifiedDate}
                        // value={modifiedDate}
                        label={t('ModifiedDate')}
                        onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                </Box>
            </CardContent>
            {mainButtonContainer === CardButtonGroupPosition.Bottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderMainButtonGroup()}
            </CardActions>}
        </Card >
    );
}

