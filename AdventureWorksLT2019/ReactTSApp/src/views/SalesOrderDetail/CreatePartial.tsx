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
import { ISalesOrderHeaderAdvancedQuery, defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { IProductAdvancedQuery, defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';

import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultSalesOrderDetail, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenCreate } from 'src/dataModels/ISalesOrderDetailDataModel';
import { post } from 'src/slices/SalesOrderDetailSlice';

export default function CreatePartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<ISalesOrderDetailDataModel>(defaultSalesOrderDetail());
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



    const [iSalesOrderHeaderAdvancedQuery_SalesOrderID, setISalesOrderHeaderAdvancedQuery_SalesOrderID] = useState<ISalesOrderHeaderAdvancedQuery>({ ...defaultISalesOrderHeaderAdvancedQuery(), pageSize: 10000 });
    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false }]);

    const [iProductAdvancedQuery_ProductID, setIProductAdvancedQuery_ProductID] = useState<IProductAdvancedQuery>({ ...defaultIProductAdvancedQuery(), pageSize: 10000 });
    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.product_Name, value: item.productID, selected: false }]);
    useEffect(() => {

        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
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

    const onSubmit = () => {
        setCreating(true);
        dispatch(post({ ...item }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
                        setItem(defaultSalesOrderDetail());
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
                subheader={t("SalesOrderDetail")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    label={t("SalesOrderID")}
                    id="salesOrderIDSelect"
                    select
                    name='salesOrderID'
                    {...register("salesOrderID", salesOrderDetailFormValidationWhenCreate.salesOrderID)}
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
                    name='orderQty'
                    label={t('OrderQty')}
                    defaultValue={item.orderQty}
                    variant='outlined'
                    margin='normal'
                    {...register("orderQty", salesOrderDetailFormValidationWhenCreate.orderQty)}
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
                    {...register("productID", salesOrderDetailFormValidationWhenCreate.productID)}
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
                    {...register("unitPrice", salesOrderDetailFormValidationWhenCreate.unitPrice)}
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
                    {...register("unitPriceDiscount", salesOrderDetailFormValidationWhenCreate.unitPriceDiscount)}
                    autoComplete='unitPriceDiscount'
                    error={!!errors.unitPriceDiscount}
                    fullWidth
                    helperText={!!errors.unitPriceDiscount ? t(errors.unitPriceDiscount.message) : ''}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", salesOrderDetailFormValidationWhenCreate.modifiedDate)}
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


