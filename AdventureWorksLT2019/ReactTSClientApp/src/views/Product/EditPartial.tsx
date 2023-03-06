import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';
import { DatePicker } from '@mui/x-date-pickers';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';

import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { IProductCategoryAdvancedQuery, defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductAvatar, IProductDataModel, productFormValidationWhenEdit } from 'src/dataModels/IProductDataModel';
import { getIProductIdentifier } from 'src/dataModels/IProductQueries';
import { put } from 'src/slices/ProductSlice';

export default function EditPartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(productFormValidationWhenEdit)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



    const [productCategory_ParentIDCodeList, setProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.parent_Name, value: item.parentID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategoryID, setIProductCategoryAdvancedQuery_ProductCategoryID] = useState<IProductCategoryAdvancedQuery>({ ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: item.parentID, pageSize: 10000 });
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    const [sellStartDate, setSellStartDate] = useState<string>();
    const [sellEndDate, setSellEndDate] = useState<string>();
    const [discontinuedDate, setDiscontinuedDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ParentIDCodeList(res.responseBody);
            }
        });

        getProductCategory_ProductCategoryIDCodeList(iProductCategoryAdvancedQuery_ProductCategoryID, false, false);

        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });
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
        onParentIDChanged_LoadChildren(parentID);
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
            setValue('productCategoryID', -1);
        }
    }




    const onParentIDChanged_LoadChildren = (parentID: number) => {

        const iProductCategoryAdvancedQuery_ProductCategoryID_Here = { ...iProductCategoryAdvancedQuery_ProductCategoryID, parentProductCategoryID: parentID };
        setIProductCategoryAdvancedQuery_ProductCategoryID(iProductCategoryAdvancedQuery_ProductCategoryID_Here);
        getProductCategory_ProductCategoryIDCodeList(iProductCategoryAdvancedQuery_ProductCategoryID, true, false);
    }
    const onSubmit = (data: IProductDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: getIProductIdentifier(data), data: { ...data } }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error: any) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getProductAvatar(item);
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
                    disabled={!isValid || saving || saved}
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
                        disabled={!isValid || saving || saved}
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
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.name}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.name}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.sellStartDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {crudViewContainer !== CrudViewContainers.Wizard && !!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.name}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='name'
                                label={t('Name')}
                                defaultValue={item.name}
                                variant='outlined'
                                margin='normal'
                                {...register("name")}
                                autoComplete='name'
                                error={!!errors.name}
                                fullWidth
                                helperText={!!errors.name ? t(errors.name.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='productNumber'
                                label={t('ProductNumber')}
                                defaultValue={item.productNumber}
                                variant='outlined'
                                margin='normal'
                                {...register("productNumber")}
                                autoComplete='productNumber'
                                error={!!errors.productNumber}
                                fullWidth
                                helperText={!!errors.productNumber ? t(errors.productNumber.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='color'
                                label={t('Color')}
                                defaultValue={item.color}
                                variant='outlined'
                                margin='normal'
                                {...register("color")}
                                autoComplete='color'
                                error={!!errors.color}
                                fullWidth
                                helperText={!!errors.color ? t(errors.color.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='standardCost'
                                label={t('StandardCost')}
                                defaultValue={item.standardCost}
                                variant='outlined'
                                margin='normal'
                                {...register("standardCost")}
                                autoComplete='standardCost'
                                error={!!errors.standardCost}
                                fullWidth
                                helperText={!!errors.standardCost ? t(errors.standardCost.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='listPrice'
                                label={t('ListPrice')}
                                defaultValue={item.listPrice}
                                variant='outlined'
                                margin='normal'
                                {...register("listPrice")}
                                autoComplete='listPrice'
                                error={!!errors.listPrice}
                                fullWidth
                                helperText={!!errors.listPrice ? t(errors.listPrice.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='size'
                                label={t('Size')}
                                defaultValue={item.size}
                                variant='outlined'
                                margin='normal'
                                {...register("size")}
                                autoComplete='size'
                                error={!!errors.size}
                                fullWidth
                                helperText={!!errors.size ? t(errors.size.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='weight'
                                label={t('Weight')}
                                defaultValue={item.weight}
                                variant='outlined'
                                margin='normal'
                                {...register("weight")}
                                autoComplete='weight'
                                error={!!errors.weight}
                                fullWidth
                                //helperText={!!errors.weight ? t(errors.weight.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("ParentID")}
                                id="parentIDSelect"
                                select
                                name='parentID'
                                {...register("parentID")}
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("ProductCategoryID")}
                                id="productCategoryIDSelect"
                                select
                                name='productCategoryID'
                                {...register("productCategoryID")}
                                autoComplete='productCategoryID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.productCategoryID}
                            >
                                {productCategory_ProductCategoryIDCodeList && productCategory_ProductCategoryIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                            	sx={{marginTop: 2}}
                                label={t("ProductModelID")}
                                id="productModelIDSelect"
                                select
                                name='productModelID'
                                {...register("productModelID")}
                                autoComplete='productModelID'
                                variant="outlined"
                                fullWidth
                                defaultValue={item.productModelID}
                            >
                                {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                                    return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                                })}
                            </TextField>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="sellStartDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={sellStartDate}
                            				label={t('SellStartDate')}
                            				onChange={(event: string) => { setSellStartDate(event); setValue('sellStartDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.sellStartDate}
                            						helperText={!!errors.sellStartDate ? t(errors.sellStartDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="sellEndDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={sellEndDate}
                            				label={t('SellEndDate')}
                            				onChange={(event: string) => { setSellEndDate(event); setValue('sellEndDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.sellEndDate}
                            						helperText={!!errors.sellEndDate ? t(errors.sellEndDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="discontinuedDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={discontinuedDate}
                            				label={t('DiscontinuedDate')}
                            				onChange={(event: string) => { setDiscontinuedDate(event); setValue('discontinuedDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.discontinuedDate}
                            						helperText={!!errors.discontinuedDate ? t(errors.discontinuedDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='thumbNailPhoto'
                                label={t('ThumbNailPhoto')}
                                defaultValue={item.thumbNailPhoto}
                                variant='outlined'
                                margin='normal'
                                {...register("thumbNailPhoto")}
                                autoComplete='thumbNailPhoto'
                                error={!!errors.thumbNailPhoto}
                                fullWidth
                                //helperText={!!errors.thumbNailPhoto ? t(errors.thumbNailPhoto.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='thumbnailPhotoFileName'
                                label={t('ThumbnailPhotoFileName')}
                                defaultValue={item.thumbnailPhotoFileName}
                                variant='outlined'
                                margin='normal'
                                {...register("thumbnailPhotoFileName")}
                                autoComplete='thumbnailPhotoFileName'
                                error={!!errors.thumbnailPhotoFileName}
                                fullWidth
                                helperText={!!errors.thumbnailPhotoFileName ? t(errors.thumbnailPhotoFileName.message) : ''}
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
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}

