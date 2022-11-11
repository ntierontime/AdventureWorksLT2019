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
import { IProductModelAdvancedQuery, defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { IProductDescriptionAdvancedQuery, defaultIProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductModelProductDescriptionAvatar, IProductModelProductDescriptionDataModel, productModelProductDescriptionFormValidationWhenEdit } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { put } from 'src/slices/ProductModelProductDescriptionSlice';

export default function EditPartial(props: ItemPartialViewProps<IProductModelProductDescriptionDataModel>): JSX.Element {
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



    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);

    const [productDescription_ProductDescriptionIDCodeList, setProductDescription_ProductDescriptionIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productDescription_Name, value: item.productDescriptionID, selected: false }]);
    useEffect(() => {


        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getProductDescriptionCodeList({ ...defaultIProductDescriptionAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductDescription_ProductDescriptionIDCodeList(res.responseBody);
            }
        });
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = (data: IProductModelProductDescriptionDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { productModelID: data.productModelID, productDescriptionID: data.productDescriptionID, culture: data.culture }, data: { ...data } }))
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
    const avatar = getProductModelProductDescriptionAvatar(item);
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
                    <Avatar sx={avatarStyle} aria-label={item.culture}>
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
                title={item.culture}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.culture}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    label={t("ProductModelID")}
                    id="productModelIDSelect"
                    select
                    name='productModelID'
                    {...register("productModelID", productModelProductDescriptionFormValidationWhenEdit.productModelID)}
                    autoComplete='productModelID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productModelID}
                >
                    {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    label={t("ProductDescriptionID")}
                    id="productDescriptionIDSelect"
                    select
                    name='productDescriptionID'
                    {...register("productDescriptionID", productModelProductDescriptionFormValidationWhenEdit.productDescriptionID)}
                    autoComplete='productDescriptionID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productDescriptionID}
                >
                    {productDescription_ProductDescriptionIDCodeList && productDescription_ProductDescriptionIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='culture'
                    label={t('Culture')}
                    defaultValue={item.culture}
                    variant='outlined'
                    margin='normal'
                    {...register("culture", productModelProductDescriptionFormValidationWhenEdit.culture)}
                    autoComplete='culture'
                    error={!!errors.culture}
                    fullWidth
                    helperText={!!errors.culture ? t(errors.culture.message) : ''}
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
                    {...register("modifiedDate", productModelProductDescriptionFormValidationWhenEdit.modifiedDate)}
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


