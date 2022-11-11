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
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { defaultIProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';

import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultProductModelProductDescription, IProductModelProductDescriptionDataModel, productModelProductDescriptionFormValidationWhenCreate } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { post } from 'src/slices/ProductModelProductDescriptionSlice';

export default function CreatePartial(props: ItemPartialViewProps<IProductModelProductDescriptionDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<IProductModelProductDescriptionDataModel>(defaultProductModelProductDescription());
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



    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);

    const [productDescription_ProductDescriptionIDCodeList, setProductDescription_ProductDescriptionIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productDescription_Name, value: item.productDescriptionID, selected: false }]);
    useEffect(() => {


        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
                const productModelID = res.responseBody[0].value;
                setValue('productModelID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getProductDescriptionCodeList({ ...defaultIProductDescriptionAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductDescription_ProductDescriptionIDCodeList(res.responseBody);
                const productDescriptionID = res.responseBody[0].value;
                setValue('productDescriptionID', res.responseBody[0].value);
				
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
                        setItem(defaultProductModelProductDescription());
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
                subheader={t("ProductModelProductDescription")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    label={t("ProductModelID")}
                    id="productModelIDSelect"
                    select
                    name='productModelID'
                    {...register("productModelID", productModelProductDescriptionFormValidationWhenCreate.productModelID)}
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
                    {...register("productDescriptionID", productModelProductDescriptionFormValidationWhenCreate.productDescriptionID)}
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
                    {...register("culture", productModelProductDescriptionFormValidationWhenCreate.culture)}
                    autoComplete='culture'
                    error={!!errors.culture}
                    fullWidth
                    helperText={!!errors.culture ? t(errors.culture.message) : ''}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", productModelProductDescriptionFormValidationWhenCreate.modifiedDate)}
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


