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
import { ISalesOrderHeaderAdvancedQuery, defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';
import { IProductAdvancedQuery, defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';
import { IProductCategoryAdvancedQuery, defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { IProductModelAdvancedQuery, defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { IAddressAdvancedQuery, defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { ICustomerAdvancedQuery, defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderDetailAvatar, ISalesOrderDetailDataModel, salesOrderDetailFormValidationWhenEdit } from 'src/dataModels/ISalesOrderDetailDataModel';
import { put } from 'src/slices/SalesOrderDetailSlice';

export default function EditPartial(props: ItemPartialViewProps<ISalesOrderDetailDataModel>): JSX.Element {
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



    const [iAddressAdvancedQuery_ShipToID, setIAddressAdvancedQuery_ShipToID] = useState<IAddressAdvancedQuery>();
    const [address_ShipToIDCodeList, setAddress_ShipToIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.shipTo_Name, value: item.shipToID, selected: false }]);

    const [iCustomerAdvancedQuery_CustomerID, setICustomerAdvancedQuery_CustomerID] = useState<ICustomerAdvancedQuery>();
    const [customer_CustomerIDCodeList, setCustomer_CustomerIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.customer_Name, value: item.customerID, selected: false }]);

    const [iAddressAdvancedQuery_BillToID, setIAddressAdvancedQuery_BillToID] = useState<IAddressAdvancedQuery>();
    const [address_BillToIDCodeList, setAddress_BillToIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.billTo_Name, value: item.billToID, selected: false }]);

    const [iSalesOrderHeaderAdvancedQuery_SalesOrderID, setISalesOrderHeaderAdvancedQuery_SalesOrderID] = useState<ISalesOrderHeaderAdvancedQuery>({ ...defaultISalesOrderHeaderAdvancedQuery(), billToAddressID: item.billToID, customerID: item.customerID, shipToAddressID: item.shipToID, pageSize: 10000 });
    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.salesOrderHeader_Name, value: item.salesOrderID, selected: false }]);

    const [iProductModelAdvancedQuery_ProductModelID, setIProductModelAdvancedQuery_ProductModelID] = useState<IProductModelAdvancedQuery>();
    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategory_ParentID, setIProductCategoryAdvancedQuery_ProductCategory_ParentID] = useState<IProductCategoryAdvancedQuery>();
    const [productCategory_ProductCategory_ParentIDCodeList, setProductCategory_ProductCategory_ParentIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Parent_Name, value: item.productCategory_ParentID, selected: false }]);

    const [iProductCategoryAdvancedQuery_ProductCategoryID, setIProductCategoryAdvancedQuery_ProductCategoryID] = useState<IProductCategoryAdvancedQuery>({ ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: item.productCategory_ParentID, pageSize: 10000 });
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [iProductAdvancedQuery_ProductID, setIProductAdvancedQuery_ProductID] = useState<IProductAdvancedQuery>({ ...defaultIProductAdvancedQuery(), productCategoryID: item.productCategoryID, parentID: item.productCategory_ParentID, productModelID: item.productModelID, pageSize: 10000 });
    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.product_Name, value: item.productID, selected: false }]);
    useEffect(() => {


		setIAddressAdvancedQuery_ShipToID({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getAddressCodeList(iAddressAdvancedQuery_ShipToID).then((res) => {
            if (res.status === "OK") {
                setAddress_ShipToIDCodeList(res.responseBody);
            }
        });

		setICustomerAdvancedQuery_CustomerID({ ...defaultICustomerAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getCustomerCodeList(iCustomerAdvancedQuery_CustomerID).then((res) => {
            if (res.status === "OK") {
                setCustomer_CustomerIDCodeList(res.responseBody);
            }
        });

		setIAddressAdvancedQuery_BillToID({ ...defaultIAddressAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getAddressCodeList(iAddressAdvancedQuery_BillToID).then((res) => {
            if (res.status === "OK") {
                setAddress_BillToIDCodeList(res.responseBody);
            }
        });

        getSalesOrderHeader_SalesOrderIDCodeList(iSalesOrderHeaderAdvancedQuery_SalesOrderID, false, false);

		setIProductModelAdvancedQuery_ProductModelID({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getProductModelCodeList(iProductModelAdvancedQuery_ProductModelID).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });

		setIProductCategoryAdvancedQuery_ProductCategory_ParentID({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 });
        codeListsApi.getProductCategoryCodeList(iProductCategoryAdvancedQuery_ProductCategory_ParentID).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ProductCategory_ParentIDCodeList(res.responseBody);
            }
        });

        getProductCategory_ProductCategoryIDCodeList(iProductCategoryAdvancedQuery_ProductCategoryID, false, false);

        getProduct_ProductIDCodeList(iProductAdvancedQuery_ProductID, false, false);
        reset(item);
        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);



    const onShipToIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ShipToID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const shipToID = nameValuePair.value as number;

        setISalesOrderHeaderAdvancedQuery_SalesOrderID({ ...iSalesOrderHeaderAdvancedQuery_SalesOrderID, shipToAddressID: shipToID });
        getSalesOrderHeader_SalesOrderIDCodeList(iSalesOrderHeaderAdvancedQuery_SalesOrderID, true, false);
    }

    const onCustomerIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("CustomerID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const customerID = nameValuePair.value as number;

        setISalesOrderHeaderAdvancedQuery_SalesOrderID({ ...iSalesOrderHeaderAdvancedQuery_SalesOrderID, customerID: customerID });
        getSalesOrderHeader_SalesOrderIDCodeList(iSalesOrderHeaderAdvancedQuery_SalesOrderID, true, false);
    }

    const onBillToIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("BillToID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const billToID = nameValuePair.value as number;

        setISalesOrderHeaderAdvancedQuery_SalesOrderID({ ...iSalesOrderHeaderAdvancedQuery_SalesOrderID, billToAddressID: billToID });
        getSalesOrderHeader_SalesOrderIDCodeList(iSalesOrderHeaderAdvancedQuery_SalesOrderID, true, false);
    }

    const onProductModelIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ProductModelID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const productModelID = nameValuePair.value as number;

        setIProductAdvancedQuery_ProductID({ ...iProductAdvancedQuery_ProductID, productModelID: productModelID });
        getProduct_ProductIDCodeList(iProductAdvancedQuery_ProductID, true, false);
    }

    const onProductCategory_ParentIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ProductCategory_ParentID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const productCategory_ParentID = nameValuePair.value as number;

        setIProductAdvancedQuery_ProductID({ ...iProductAdvancedQuery_ProductID, parentID: productCategory_ParentID });
        getProduct_ProductIDCodeList(iProductAdvancedQuery_ProductID, true, false);


        setIProductCategoryAdvancedQuery_ProductCategoryID({ ...iProductCategoryAdvancedQuery_ProductCategoryID, parentProductCategoryID: productCategory_ParentID });
        getProductCategory_ProductCategoryIDCodeList(iProductCategoryAdvancedQuery_ProductCategoryID, true, false);
    }

    const onProductCategoryIDChanged = (event: React.PointerEvent<INameValuePair>) => {
        console.log("ProductCategoryID");
        // name is the property name, value is the selected value
        const nameValuePair = event.target as unknown as INameValuePair;

        const productCategoryID = nameValuePair.value as number;

        setIProductAdvancedQuery_ProductID({ ...iProductAdvancedQuery_ProductID, productCategoryID: productCategoryID });
        getProduct_ProductIDCodeList(iProductAdvancedQuery_ProductID, true, false);
    }


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
            setValue('salesOrderID', '');
        }
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
            setValue('productID', '');
        }
    }

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
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderID.toString()}>
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
                    label={t("ShipToID")}
                    id="shipToIDSelect"
                    select
                    name='shipToID'
                    {...register("shipToID", salesOrderDetailFormValidationWhenEdit.shipToID)}
                    autoComplete='shipToID'
                    variant="outlined"
                    fullWidth
                    defaultValue={item.shipToID}
                	onChange={(event: any) => { onShipToIDChanged(event) }}
                >
                    {address_ShipToIDCodeList && address_ShipToIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                	onChange={(event: any) => { onCustomerIDChanged(event) }}
                >
                    {customer_CustomerIDCodeList && customer_CustomerIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                	onChange={(event: any) => { onBillToIDChanged(event) }}
                >
                    {address_BillToIDCodeList && address_BillToIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                    helperText={!!errors.orderQty ? t(errors.orderQty.message) : ''}
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
                	onChange={(event: any) => { onProductModelIDChanged(event) }}
                >
                    {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                	onChange={(event: any) => { onProductCategory_ParentIDChanged(event) }}
                >
                    {productCategory_ProductCategory_ParentIDCodeList && productCategory_ProductCategory_ParentIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                	onChange={(event: any) => { onProductCategoryIDChanged(event) }}
                >
                    {productCategory_ProductCategoryIDCodeList && productCategory_ProductCategoryIDCodeList.map((v, index) => {
                        return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    })}
                </TextField>
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
                    helperText={!!errors.unitPriceDiscount ? t(errors.unitPriceDiscount.message) : ''}
                />
                <TextField
                    name='lineTotal'
                    label={t('LineTotal')}
                	value={t(i18nFormats.double.format, { val: item.lineTotal })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
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
                    {...register("modifiedDate", salesOrderDetailFormValidationWhenEdit.modifiedDate)}
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


