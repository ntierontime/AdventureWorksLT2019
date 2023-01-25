import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderDetailAvatar, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenEdit } from 'src/dataModels/ISalesOrderDetailDataModel';
import { put } from 'src/slices/SalesOrderDetailSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
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



    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false }]);

    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.product_Name, value: item.productID, selected: false }]);
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getSalesOrderHeaderCodeList({ ...defaultISalesOrderHeaderAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setSalesOrderHeader_SalesOrderIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getProductCodeList({ ...defaultIProductAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProduct_ProductIDCodeList(res.responseBody);
            }
        });
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




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


    const renderButtonGroup_IconButtons = () => {
        return (
            <>                {!!handleSelectItemClick && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                ><Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                /></ButtonGroup>}
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
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderID.toString()}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.salesOrderID}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderID}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                                        fullWidth
                                        autoComplete='modifiedDate'
                            			{...register("modifiedDate", salesOrderDetailFormValidationWhenEdit.modifiedDate)}
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

