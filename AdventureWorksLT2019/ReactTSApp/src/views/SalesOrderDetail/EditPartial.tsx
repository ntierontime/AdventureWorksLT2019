import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, IconButton, MenuItem, TextField, Typography, useTheme } from '@mui/material';
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
import { defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { defaultSalesOrderDetail, getSalesOrderDetailAvatar, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenEdit } from 'src/dataModels/ISalesOrderDetailDataModel';
import { put } from 'src/slices/SalesOrderDetailSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, handleSubmit, reset, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultSalesOrderDetail(),
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();



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
        reset(item);
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const onSubmit = (data: ISalesOrderDetailDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { salesOrderID: data.salesOrderID, salesOrderDetailID: data.salesOrderDetailID }, data: { ...data } }))
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
    const avatar = getSalesOrderDetailAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderID.toString()}>
                        {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <>
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
                        </>}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && <>
                            <LoadingButton
                                color="primary"
                                type='submit'
                                variant='contained'
                                disabled={!isValid || saving || saved}
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
                        </>}
                    </>
                }
                title={item.salesOrderID}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.salesOrderID}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <TextField
                    label={t("SalesOrderID")}
                    id="salesOrderIDSelect"
                    select
                    name='salesOrderID'
                    {...register("salesOrderID", salesOrderDetailFormValidationWhenEdit.salesOrderID)}
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
                    name='salesOrderDetailID'
                    label={t('SalesOrderDetailID')}
                	value={item.salesOrderDetailID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='orderQty'
                    label={t('OrderQty')}
                	defaultValue={item.orderQty}
                    variant='outlined'
                    margin='normal'
                    {...register("orderQty", salesOrderDetailFormValidationWhenEdit.orderQty)}
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
                    {...register("productID", salesOrderDetailFormValidationWhenEdit.productID)}
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
                    {...register("unitPrice", salesOrderDetailFormValidationWhenEdit.unitPrice)}
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
                    {...register("unitPriceDiscount", salesOrderDetailFormValidationWhenEdit.unitPriceDiscount)}
                    autoComplete='unitPriceDiscount'
                    error={!!errors.unitPriceDiscount}
                    fullWidth
                    autoFocus
                    helperText={!!errors.unitPriceDiscount ? t(errors.unitPriceDiscount.message) : ''}
                />
                <TextField
                    name='lineTotal'
                    label={t('LineTotal')}
                	value={t(i18nFormats.double.format, { val: item.lineTotal })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='_rowguid'
                    label={t('rowguid')}
                	value={item._rowguid}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", salesOrderDetailFormValidationWhenEdit.modifiedDate)}
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
                    value={item.product_Name}
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
                    {...register("productCategoryID", salesOrderDetailFormValidationWhenEdit.productCategoryID)}
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
                    value={item.productCategory_Name}
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
                    {...register("productCategory_ParentID", salesOrderDetailFormValidationWhenEdit.productCategory_ParentID)}
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
                    value={item.productCategory_Parent_Name}
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
                    {...register("productModelID", salesOrderDetailFormValidationWhenEdit.productModelID)}
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
                    value={item.productModel_Name}
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
                    value={item.salesOrderHeader_Name}
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
                    {...register("billToID", salesOrderDetailFormValidationWhenEdit.billToID)}
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
                    value={item.billTo_Name}
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
                    {...register("customerID", salesOrderDetailFormValidationWhenEdit.customerID)}
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
                    value={item.customer_Name}
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
                    {...register("shipToID", salesOrderDetailFormValidationWhenEdit.shipToID)}
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
                    value={item.shipTo_Name}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
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


