import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
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
import { ISalesOrderHeaderAdvancedQuery, defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { IProductAdvancedQuery, defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderDetailAvatar, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenEdit } from 'src/dataModels/ISalesOrderDetailDataModel';
import { put } from 'src/slices/SalesOrderDetailSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



    const [iSalesOrderHeaderAdvancedQuery_SalesOrderID, setISalesOrderHeaderAdvancedQuery_SalesOrderID] = useState<ISalesOrderHeaderAdvancedQuery>({ ...defaultISalesOrderHeaderAdvancedQuery(), pageSize: 10000 });
    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false }]);

    const [iProductAdvancedQuery_ProductID, setIProductAdvancedQuery_ProductID] = useState<IProductAdvancedQuery>({ ...defaultIProductAdvancedQuery(), pageSize: 10000 });
    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.product_Name, value: item.productID, selected: false }]);
    useEffect(() => {


        getSalesOrderHeader_SalesOrderIDCodeList(iSalesOrderHeaderAdvancedQuery_SalesOrderID, false, false);

        getProduct_ProductIDCodeList(iProductAdvancedQuery_ProductID, false, false);
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const getSalesOrderHeader_SalesOrderIDCodeList = (query: ISalesOrderHeaderAdvancedQuery, toSetSelectedValue: boolean, setCodeListToEmpty: boolean) => {
        if (!setCodeListToEmpty) {
            codeListsApi.getSalesOrderHeaderCodeList({ ...query, pageSize: 10000 }).then((res) => {
                if (res.status === "OK") {
                    if (toSetSelectedValue) {
                        if (res.responseBody.findIndex(t => t.value === item.salesOrderID) === -1) {
                            if (res.responseBody.length > 0) {
                                setValue('salesOrderID', res.responseBody[0].value);
                            }
                            else {
                                setValue('salesOrderID', -1);
                            }
                        }
                        else {
                            setValue('salesOrderID', item.salesOrderID);
                        }
                    }
                    setSalesOrderHeader_SalesOrderIDCodeList(res.responseBody);
                }
            });
        }
        else {
            setSalesOrderHeader_SalesOrderIDCodeList([]);
            setValue('salesOrderID', -1);
        }
    }

    const getProduct_ProductIDCodeList = (query: IProductAdvancedQuery, toSetSelectedValue: boolean, setCodeListToEmpty: boolean) => {
        if (!setCodeListToEmpty) {
            codeListsApi.getProductCodeList({ ...query, pageSize: 10000 }).then((res) => {
                if (res.status === "OK") {
                    if (toSetSelectedValue) {
                        if (res.responseBody.findIndex(t => t.value === item.productID) === -1) {
                            if (res.responseBody.length > 0) {
                                setValue('productID', res.responseBody[0].value);
                            }
                            else {
                                setValue('productID', -1);
                            }
                        }
                        else {
                            setValue('productID', item.productID);
                        }
                    }
                    setProduct_ProductIDCodeList(res.responseBody);
                }
            });
        }
        else {
            setProduct_ProductIDCodeList([]);
            setValue('productID', -1);
        }
    }

    const onSubmit = (data: ISalesOrderDetailDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { salesOrderID: data.salesOrderID, salesOrderDetailID: data.salesOrderDetailID }, data: { ...data } }))
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
    const avatar = getSalesOrderDetailAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton
                    color="primary"
                    type='submit'
                    disabled={(!isValid || saving || saved) && !isDirty}
                    aria-label="save">
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
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
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
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
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <LoadingButton
                    color="primary"
                    type='submit'
                    variant='contained'
                    disabled={(!isValid || saving || saved) && !isDirty}
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
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderID.toString()}>
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
                title={item.salesOrderID}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderID}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    label={t("SalesOrderID")}
                    id="salesOrderIDSelect"
                    select
                    name='salesOrderID'
                    {...register("salesOrderID", salesOrderDetailFormValidationWhenEdit.salesOrderID)}
                    autoComplete='salesOrderID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.salesOrderID}
                >
                    {salesOrderHeader_SalesOrderIDCodeList && salesOrderHeader_SalesOrderIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='salesOrderDetailID'
                    label={t('SalesOrderDetailID')}
                    value={item.salesOrderDetailID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='orderQty'
                    label={t('OrderQty')}
                    defaultValue={item.orderQty}
                    variant='outlined'
                    margin='normal'
                    {...register("orderQty", salesOrderDetailFormValidationWhenEdit.orderQty)}
                    autoComplete='orderQty'
                    error={!!errors.orderQty}
                    fullWidth
                    helperText={!!errors.orderQty ? t(errors.orderQty.message) : ''}
                />
                <TextField
                    label={t("ProductID")}
                    id="productIDSelect"
                    select
                    name='productID'
                    {...register("productID", salesOrderDetailFormValidationWhenEdit.productID)}
                    autoComplete='productID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productID}
                >
                    {product_ProductIDCodeList && product_ProductIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='unitPrice'
                    label={t('UnitPrice')}
                    defaultValue={item.unitPrice}
                    variant='outlined'
                    margin='normal'
                    {...register("unitPrice", salesOrderDetailFormValidationWhenEdit.unitPrice)}
                    autoComplete='unitPrice'
                    error={!!errors.unitPrice}
                    fullWidth
                    helperText={!!errors.unitPrice ? t(errors.unitPrice.message) : ''}
                />
                <TextField
                    name='unitPriceDiscount'
                    label={t('UnitPriceDiscount')}
                    defaultValue={item.unitPriceDiscount}
                    variant='outlined'
                    margin='normal'
                    {...register("unitPriceDiscount", salesOrderDetailFormValidationWhenEdit.unitPriceDiscount)}
                    autoComplete='unitPriceDiscount'
                    error={!!errors.unitPriceDiscount}
                    fullWidth
                    helperText={!!errors.unitPriceDiscount ? t(errors.unitPriceDiscount.message) : ''}
                />
                <TextField
                    name='lineTotal'
                    label={t('LineTotal')}
                    value={t(i18nFormats.double.format, { val: item.lineTotal })}
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
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", salesOrderDetailFormValidationWhenEdit.modifiedDate)}
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


