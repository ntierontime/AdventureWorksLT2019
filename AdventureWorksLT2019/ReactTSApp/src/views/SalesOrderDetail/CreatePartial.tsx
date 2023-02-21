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
import { defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultSalesOrderDetail, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenCreate } from 'src/dataModels/ISalesOrderDetailDataModel';
import { post } from 'src/slices/SalesOrderDetailSlice';

export default function CreatePartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<ISalesOrderDetailDataModel>(defaultSalesOrderDetail());
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



    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false }]);

    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.product_Name, value: item.productID, selected: false }]);
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getSalesOrderHeaderCodeList({ ...defaultISalesOrderHeaderAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setSalesOrderHeader_SalesOrderIDCodeList(res.responseBody);
                setValue('salesOrderID', res.responseBody[0].value);
				
            }
        });

        codeListsApi.getProductCodeList({ ...defaultIProductAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProduct_ProductIDCodeList(res.responseBody);
                setValue('productID', res.responseBody[0].value);
				
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
                subheader={t("SalesOrderDetail")}
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={modifiedDate}
                                label={t('ModifiedDate')}
                                onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='modifiedDate'
                            			{...register("modifiedDate", salesOrderDetailFormValidationWhenCreate.modifiedDate)}
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


