import { useEffect, useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, MenuItem, TextField, Typography } from '@mui/material';
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
import { IProductDataModel, productFormValidationWhenCreate } from 'src/dataModels/IProductDataModel';
import { post } from 'src/slices/ProductSlice';

export default function CreatePartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
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
        resolver: yupResolver(productFormValidationWhenCreate)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;
    // #endregion 1. redux-hook-form related

    // #region 2. CodeLists if any
	

    const [productCategory_ParentIDCodeList, setProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.parent_Name, value: item.parentID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategoryID, setIProductCategoryAdvancedQuery_ProductCategoryID] = useState<IProductCategoryAdvancedQuery>({ ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: item.parentID, pageSize: 10000 });
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    const [sellStartDate, setSellStartDate] = useState<string>();
    const [sellEndDate, setSellEndDate] = useState<string>();
    const [discontinuedDate, setDiscontinuedDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();



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

    const onSubmit = (data: IProductDataModel) => {
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


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ParentIDCodeList(res.responseBody);
                setValue('parentID', res.responseBody[0].value);
				onParentIDChanged_LoadChildren(res.responseBody[0].value);
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
                subheader={t("Product")}
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

