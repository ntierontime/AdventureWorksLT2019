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
import { IProductCategoryAdvancedQuery, defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { IProductModelAdvancedQuery, defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';

import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultProduct, IProductDataModel, productFormValidationWhenCreate } from 'src/dataModels/IProductDataModel';
import { post } from 'src/slices/ProductSlice';

export default function CreatePartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<IProductDataModel>(defaultProduct());
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



    const [productCategory_ParentIDCodeList, setProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.parent_Name, value: item.parentID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategoryID, setIProductCategoryAdvancedQuery_ProductCategoryID] = useState<IProductCategoryAdvancedQuery>({ ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: item.parentID, pageSize: 10000 });
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ParentIDCodeList(res.responseBody);
                const parentID = res.responseBody[0].value;
                setValue('parentID', res.responseBody[0].value);
				onParentIDChanged_LoadChildren(parentID);
            }
        });

        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
                const productModelID = res.responseBody[0].value;
                setValue('productModelID', res.responseBody[0].value);
				
            }
        });
        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);



    const onParentIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ParentID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const parentID = nameValuePair.value as number;
        onParentIDChanged_LoadChildren(parentID);
    }

    const onParentIDChanged_LoadChildren = (parentID: number) => {

        const iProductCategoryAdvancedQuery_ProductCategoryID_Here = { ...iProductCategoryAdvancedQuery_ProductCategoryID, parentProductCategoryID: parentID };
        setIProductCategoryAdvancedQuery_ProductCategoryID(iProductCategoryAdvancedQuery_ProductCategoryID_Here);
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
            setValue('productCategoryID', -1);
        }
    }

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
                        disabled={(!isValid || creating || created) && !isDirty}
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
                <IconButton type='submit' aria-label="create" disabled={(!isValid || creating || created) && !isDirty}>
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
                        disabled={(!isValid || creating || created) && !isDirty}
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
                subheader={t("Product")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
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
                <TextField
                    label={t("ParentID")}
                    id="parentIDSelect"
                    select
                    name='parentID'
                    {...register("parentID", productFormValidationWhenCreate.parentID)}
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
                <Controller
                    name="sellStartDate"
                    defaultValue={item.sellStartDate}
                    control={control}
                    {...register("sellStartDate", productFormValidationWhenCreate.sellStartDate)}
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
                    {...register("sellEndDate", productFormValidationWhenCreate.sellEndDate)}
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
                    {...register("discontinuedDate", productFormValidationWhenCreate.discontinuedDate)}
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
                    {...register("thumbNailPhoto", productFormValidationWhenCreate.thumbNailPhoto)}
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
                    {...register("thumbnailPhotoFileName", productFormValidationWhenCreate.thumbnailPhotoFileName)}
                    autoComplete='thumbnailPhotoFileName'
                    error={!!errors.thumbnailPhotoFileName}
                    fullWidth
                    helperText={!!errors.thumbnailPhotoFileName ? t(errors.thumbnailPhotoFileName.message) : ''}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", productFormValidationWhenCreate.modifiedDate)}
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


