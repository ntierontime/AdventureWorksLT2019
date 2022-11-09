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
import { IProductCategoryAdvancedQuery, defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { IProductModelAdvancedQuery, defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductAvatar, IProductDataModel, productFormValidationWhenEdit } from 'src/dataModels/IProductDataModel';
import { put } from 'src/slices/ProductSlice';

export default function EditPartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, setValue, handleSubmit, reset, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



    const [iProductCategoryAdvancedQuery_ParentID, setIProductCategoryAdvancedQuery_ParentID] = useState<IProductCategoryAdvancedQuery>();
    const [productCategory_ParentIDCodeList, setProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.parent_Name, value: item.parentID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategoryID, setIProductCategoryAdvancedQuery_ProductCategoryID] = useState<IProductCategoryAdvancedQuery>({ ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: item.parentID, pageSize: 10000 });
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [iProductModelAdvancedQuery_ProductModelID, setIProductModelAdvancedQuery_ProductModelID] = useState<IProductModelAdvancedQuery>();
    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList(iProductCategoryAdvancedQuery_ParentID).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ParentIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getProductModelCodeList(iProductModelAdvancedQuery_ProductModelID).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });
        reset(item);
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);



    const onParentIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ParentID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const parentID = nameValuePair.value as number;

        setIProductCategoryAdvancedQuery_ProductCategoryID({ ...iProductCategoryAdvancedQuery_ProductCategoryID, parentProductCategoryID: parentID });
        getProductCategory_ProductCategoryIDCodeList(iProductCategoryAdvancedQuery_ProductCategoryID, true, false);
    }


    const getProductCategory_ProductCategoryIDCodeList = (query: IProductCategoryAdvancedQuery, toSetSelectedValue: boolean, setCodeListToEmpty: boolean) => {
        if (!setCodeListToEmpty) {
			codeListsApi.getProductCategoryCodeList({ ...query, pageSize: 10000 }).then((res) => {
				if (res.status === "OK") {
					if (toSetSelectedValue) {
						if (res.responseBody.findIndex(t => t.value === item.productCategoryID) === -1) {
							if (res.responseBody.length > 0) {
								setValue('productCategoryID', res.responseBody[0].value);
							}
							else {
								setValue('productCategoryID', -1);
							}
						}
						else {
							setValue('productCategoryID', item.productCategoryID);
						}
					}
					setProductCategory_ProductCategoryIDCodeList(res.responseBody);
				}
			});
		}
        else {
            setProductCategory_ProductCategoryIDCodeList([]);
            setValue('productCategoryID', '');
        }
    }

    const onSubmit = (data: IProductDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { productID: data.productID }, data: { ...data } }))
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
    const avatar = getProductAvatar(item);
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
                    <Avatar sx={avatarStyle} aria-label={item.name}>
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
                title={item.name}
                subheader={t('{{val, datetime}}', { val: new Date(item.sellStartDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.name}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    name='productID'
                    label={t('ProductID')}
                	value={item.productID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='name'
                    label={t('Name')}
                    defaultValue={item.name}
                    variant='outlined'
                    margin='normal'
                    {...register("name", productFormValidationWhenEdit.name)}
                    autoComplete='name'
                    error={!!errors.name}
                    fullWidth
                    helperText={!!errors.name ? t(errors.name.message) : ''}
                />
                <TextField
                    name='productNumber'
                    label={t('ProductNumber')}
                    defaultValue={item.productNumber}
                    variant='outlined'
                    margin='normal'
                    {...register("productNumber", productFormValidationWhenEdit.productNumber)}
                    autoComplete='productNumber'
                    error={!!errors.productNumber}
                    fullWidth
                    helperText={!!errors.productNumber ? t(errors.productNumber.message) : ''}
                />
                <TextField
                    name='color'
                    label={t('Color')}
                    defaultValue={item.color}
                    variant='outlined'
                    margin='normal'
                    {...register("color", productFormValidationWhenEdit.color)}
                    autoComplete='color'
                    error={!!errors.color}
                    fullWidth
                    helperText={!!errors.color ? t(errors.color.message) : ''}
                />
                <TextField
                    name='standardCost'
                    label={t('StandardCost')}
                    defaultValue={item.standardCost}
                    variant='outlined'
                    margin='normal'
                    {...register("standardCost", productFormValidationWhenEdit.standardCost)}
                    autoComplete='standardCost'
                    error={!!errors.standardCost}
                    fullWidth
                    helperText={!!errors.standardCost ? t(errors.standardCost.message) : ''}
                />
                <TextField
                    name='listPrice'
                    label={t('ListPrice')}
                    defaultValue={item.listPrice}
                    variant='outlined'
                    margin='normal'
                    {...register("listPrice", productFormValidationWhenEdit.listPrice)}
                    autoComplete='listPrice'
                    error={!!errors.listPrice}
                    fullWidth
                    helperText={!!errors.listPrice ? t(errors.listPrice.message) : ''}
                />
                <TextField
                    name='size'
                    label={t('Size')}
                    defaultValue={item.size}
                    variant='outlined'
                    margin='normal'
                    {...register("size", productFormValidationWhenEdit.size)}
                    autoComplete='size'
                    error={!!errors.size}
                    fullWidth
                    helperText={!!errors.size ? t(errors.size.message) : ''}
                />
                <TextField
                    name='weight'
                    label={t('Weight')}
                    defaultValue={item.weight}
                    variant='outlined'
                    margin='normal'
                    {...register("weight", productFormValidationWhenEdit.weight)}
                    autoComplete='weight'
                    error={!!errors.weight}
                    fullWidth
                    //helperText={!!errors.weight ? t(errors.weight.message) : ''}
                />
                <TextField
                    label={t("ParentID")}
                    id="parentIDSelect"
                    select
                    name='parentID'
                    {...register("parentID", productFormValidationWhenEdit.parentID)}
                    autoComplete='parentID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.parentID}
                	onChange={(event: any) => { onParentIDChanged(event) }}
                >
                    {productCategory_ParentIDCodeList && productCategory_ParentIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    label={t("ProductCategoryID")}
                    id="productCategoryIDSelect"
                    select
                    name='productCategoryID'
                    {...register("productCategoryID", productFormValidationWhenEdit.productCategoryID)}
                    autoComplete='productCategoryID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productCategoryID}
                >
                    {productCategory_ProductCategoryIDCodeList && productCategory_ProductCategoryIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    label={t("ProductModelID")}
                    id="productModelIDSelect"
                    select
                    name='productModelID'
                    {...register("productModelID", productFormValidationWhenEdit.productModelID)}
                    autoComplete='productModelID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productModelID}
                >
                    {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <Controller
                    name="sellStartDate"
                    defaultValue={item.sellStartDate}
                    control={control}
                    {...register("sellStartDate", productFormValidationWhenEdit.sellStartDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('SellStartDate')}
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                                        ref={null}
                                        fullWidth
                                        autoComplete='sellStartDate'
                                        error={!!errors.sellStartDate}
                                        helperText={!!errors.sellStartDate ? t(errors.sellStartDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <Controller
                    name="sellEndDate"
                    defaultValue={item.sellEndDate}
                    control={control}
                    {...register("sellEndDate", productFormValidationWhenEdit.sellEndDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('SellEndDate')}
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                                        ref={null}
                                        fullWidth
                                        autoComplete='sellEndDate'
                                        error={!!errors.sellEndDate}
                                        //helperText={!!errors.sellEndDate ? t(errors.sellEndDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <Controller
                    name="discontinuedDate"
                    defaultValue={item.discontinuedDate}
                    control={control}
                    {...register("discontinuedDate", productFormValidationWhenEdit.discontinuedDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('DiscontinuedDate')}
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                                        ref={null}
                                        fullWidth
                                        autoComplete='discontinuedDate'
                                        error={!!errors.discontinuedDate}
                                        //helperText={!!errors.discontinuedDate ? t(errors.discontinuedDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <TextField
                    name='thumbNailPhoto'
                    label={t('ThumbNailPhoto')}
                    defaultValue={item.thumbNailPhoto}
                    variant='outlined'
                    margin='normal'
                    {...register("thumbNailPhoto", productFormValidationWhenEdit.thumbNailPhoto)}
                    autoComplete='thumbNailPhoto'
                    error={!!errors.thumbNailPhoto}
                    fullWidth
                    //helperText={!!errors.thumbNailPhoto ? t(errors.thumbNailPhoto.message) : ''}
                />
                <TextField
                    name='thumbnailPhotoFileName'
                    label={t('ThumbnailPhotoFileName')}
                    defaultValue={item.thumbnailPhotoFileName}
                    variant='outlined'
                    margin='normal'
                    {...register("thumbnailPhotoFileName", productFormValidationWhenEdit.thumbnailPhotoFileName)}
                    autoComplete='thumbnailPhotoFileName'
                    error={!!errors.thumbnailPhotoFileName}
                    fullWidth
                    helperText={!!errors.thumbnailPhotoFileName ? t(errors.thumbnailPhotoFileName.message) : ''}
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
                    {...register("modifiedDate", productFormValidationWhenEdit.modifiedDate)}
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


