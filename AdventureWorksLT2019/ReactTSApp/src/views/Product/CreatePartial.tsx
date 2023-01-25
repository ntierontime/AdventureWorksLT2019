import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultProduct, IProductDataModel, productFormValidationWhenCreate } from 'src/dataModels/IProductDataModel';
import { post } from 'src/slices/ProductSlice';

export default function CreatePartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<IProductDataModel>(defaultProduct());
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
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



    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    const [sellStartDate, setSellStartDate] = useState<string>();
    const [sellEndDate, setSellEndDate] = useState<string>();
    const [discontinuedDate, setDiscontinuedDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ProductCategoryIDCodeList(res.responseBody);
                setValue('productCategoryID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
                setValue('productModelID', res.responseBody[0].value);
				
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
                        setItem(defaultProduct());
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


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || creating || created || !isDirty}>
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
                        disabled={!isValid || creating || created || !isDirty}
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

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("Product")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='name'
                                label={t('Name')}
                                defaultValue={item.name}
                                variant='outlined'
                                margin='normal'
                                {...register("name", productFormValidationWhenCreate.name)}
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
                                {...register("productNumber", productFormValidationWhenCreate.productNumber)}
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
                                {...register("color", productFormValidationWhenCreate.color)}
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
                                {...register("standardCost", productFormValidationWhenCreate.standardCost)}
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
                                {...register("listPrice", productFormValidationWhenCreate.listPrice)}
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
                                {...register("size", productFormValidationWhenCreate.size)}
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
                                {...register("weight", productFormValidationWhenCreate.weight)}
                                autoComplete='weight'
                                error={!!errors.weight}
                                fullWidth
                                //helperText={!!errors.weight ? t(errors.weight.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                label={t("ProductCategoryID")}
                                id="productCategoryIDSelect"
                                select
                                name='productCategoryID'
                                {...register("productCategoryID", productFormValidationWhenCreate.productCategoryID)}
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
                                label={t("ProductModelID")}
                                id="productModelIDSelect"
                                select
                                name='productModelID'
                                {...register("productModelID", productFormValidationWhenCreate.productModelID)}
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
                            <DatePicker
                                value={sellStartDate}
                                label={t('SellStartDate')}
                                onChange={(event: string) => { setSellStartDate(event); setValue('sellStartDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='sellStartDate'
                            			{...register("sellStartDate", productFormValidationWhenCreate.sellStartDate)}
                                        error={!!errors.sellStartDate}
                                        helperText={!!errors.sellStartDate ? t(errors.sellStartDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={sellEndDate}
                                label={t('SellEndDate')}
                                onChange={(event: string) => { setSellEndDate(event); setValue('sellEndDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='sellEndDate'
                            			{...register("sellEndDate", productFormValidationWhenCreate.sellEndDate)}
                                        error={!!errors.sellEndDate}
                                        helperText={!!errors.sellEndDate ? t(errors.sellEndDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={discontinuedDate}
                                label={t('DiscontinuedDate')}
                                onChange={(event: string) => { setDiscontinuedDate(event); setValue('discontinuedDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='discontinuedDate'
                            			{...register("discontinuedDate", productFormValidationWhenCreate.discontinuedDate)}
                                        error={!!errors.discontinuedDate}
                                        helperText={!!errors.discontinuedDate ? t(errors.discontinuedDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='thumbNailPhoto'
                                label={t('ThumbNailPhoto')}
                                defaultValue={item.thumbNailPhoto}
                                variant='outlined'
                                margin='normal'
                                {...register("thumbNailPhoto", productFormValidationWhenCreate.thumbNailPhoto)}
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
                                {...register("thumbnailPhotoFileName", productFormValidationWhenCreate.thumbnailPhotoFileName)}
                                autoComplete='thumbnailPhotoFileName'
                                error={!!errors.thumbnailPhotoFileName}
                                fullWidth
                                helperText={!!errors.thumbnailPhotoFileName ? t(errors.thumbnailPhotoFileName.message) : ''}
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
                            			{...register("modifiedDate", productFormValidationWhenCreate.modifiedDate)}
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


