import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, IconButton, MenuItem, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

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

    const { register, control, handleSubmit, reset, formState: { isValid, errors } } = useForm({
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



    const iSalesOrderHeaderAdvancedQuery_SalesOrderID = defaultISalesOrderHeaderAdvancedQuery();
    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false}]);

    const iProductAdvancedQuery_ProductID = defaultIProductAdvancedQuery();
    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{name: item.product_Name, value: item.productID, selected: false}]);

    const iProductCategoryAdvancedQuery_ProductCategoryID = defaultIProductCategoryAdvancedQuery();
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{name: item.productCategory_Name, value: item.productCategoryID, selected: false}]);

    const iProductCategoryAdvancedQuery_ProductCategory_ParentID = defaultIProductCategoryAdvancedQuery();
    const [productCategory_ProductCategory_ParentIDCodeList, setProductCategory_ProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{name: item.productCategory_Parent_Name, value: item.productCategory_ParentID, selected: false}]);

    const iProductModelAdvancedQuery_ProductModelID = defaultIProductModelAdvancedQuery();
    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{name: item.productModel_Name, value: item.productModelID, selected: false}]);

    const iAddressAdvancedQuery_BillToID = defaultIAddressAdvancedQuery();
    const [address_BillToIDCodeList, setAddress_BillToIDCodeList] = useState<readonly INameValuePair[]>([{name: item.billTo_Name, value: item.billToID, selected: false}]);

    const iCustomerAdvancedQuery_CustomerID = defaultICustomerAdvancedQuery();
    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{name: item.customer_Name, value: item.customerID, selected: false}]);

    const iAddressAdvancedQuery_ShipToID = defaultIAddressAdvancedQuery();
    const [address_ShipToIDCodeList, setAddress_ShipToIDCodeList] = useState<readonly INameValuePair[]>([{name: item.shipTo_Name, value: item.shipToID, selected: false}]);
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...iProductCategoryAdvancedQuery_ProductCategory_ParentID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ProductCategory_ParentIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getProductModelCodeList({ ...iProductModelAdvancedQuery_ProductModelID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_BillToID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getCustomerCodeList({ ...iCustomerAdvancedQuery_CustomerID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery_ShipToID, pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToIDCodeList(res.responseBody);
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

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={
                    (crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.StandaloneView) && <>
                        <IconButton type='submit' aria-label="create" disabled={!isValid || creating || created}>
                            <SaveIcon />
                        </IconButton>
                        <IconButton aria-label="close" onClick={() => { doneAction() }}>
                            <CloseIcon />
                        </IconButton>
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
                    autoFocus
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
                    autoFocus
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
                    autoFocus
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
                                autoFocus
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
                <TextField
                    name='product_Name'
                    label={t('Product_Name')}
                    defaultValue={item.product_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("ProductCategoryID")}
                    id="productCategoryIDSelect"
                    select
                    name='productCategoryID'
                    {...register("productCategoryID", salesOrderDetailFormValidationWhenCreate.productCategoryID)}
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
                    name='productCategory_Name'
                    label={t('ProductCategory_Name')}
                    defaultValue={item.productCategory_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("ProductCategory_ParentID")}
                    id="productCategory_ParentIDSelect"
                    select
                    name='productCategory_ParentID'
                    {...register("productCategory_ParentID", salesOrderDetailFormValidationWhenCreate.productCategory_ParentID)}
                    autoComplete='productCategory_ParentID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.productCategory_ParentID}
                >
                    {productCategory_ProductCategory_ParentIDCodeList && productCategory_ProductCategory_ParentIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='productCategory_Parent_Name'
                    label={t('ProductCategory_Parent_Name')}
                    defaultValue={item.productCategory_Parent_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("ProductModelID")}
                    id="productModelIDSelect"
                    select
                    name='productModelID'
                    {...register("productModelID", salesOrderDetailFormValidationWhenCreate.productModelID)}
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
                    name='productModel_Name'
                    label={t('ProductModel_Name')}
                    defaultValue={item.productModel_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='salesOrderHeader_Name'
                    label={t('SalesOrderHeader_Name')}
                    defaultValue={item.salesOrderHeader_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("BillToID")}
                    id="billToIDSelect"
                    select
                    name='billToID'
                    {...register("billToID", salesOrderDetailFormValidationWhenCreate.billToID)}
                    autoComplete='billToID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.billToID}
                >
                    {address_BillToIDCodeList && address_BillToIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='billTo_Name'
                    label={t('BillTo_Name')}
                    defaultValue={item.billTo_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("CustomerID")}
                    id="customerIDSelect"
                    select
                    name='customerID'
                    {...register("customerID", salesOrderDetailFormValidationWhenCreate.customerID)}
                    autoComplete='customerID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.customerID}
                >
                    {customer_CustomerIDCodeList && customer_CustomerIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='customer_Name'
                    label={t('Customer_Name')}
                    defaultValue={item.customer_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    label={t("ShipToID")}
                    id="shipToIDSelect"
                    select
                    name='shipToID'
                    {...register("shipToID", salesOrderDetailFormValidationWhenCreate.shipToID)}
                    autoComplete='shipToID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.shipToID}
                >
                    {address_ShipToIDCodeList && address_ShipToIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
                <TextField
                    name='shipTo_Name'
                    label={t('ShipTo_Name')}
                    defaultValue={item.shipTo_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
            </CardContent>
            {(crudViewContainer === CrudViewContainers.Dialog) && <CardActions disableSpacing>
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
            </CardActions>}
        </Card >
    );
}


