import { useEffect,  useState } from 'react';
import { Button, ButtonGroup, Card, CardContent, CardHeader, Grid, MenuItem, TextField } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import CloseIcon from '@mui/icons-material/Close';
import SearchIcon from '@mui/icons-material/Search';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { AdvancedSearchPartialViewProps } from 'src/shared/viewModels/AdvancedSearchPartialViewProps';

import { SearchDateTimeRangeField } from 'src/shared/views/SearchDateTimeRangeField';
import { getDateRange } from 'src/shared/dataModels/PreDefinedDateTimeRanges';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';
import { defaultIProductAdvancedQuery } from 'src/dataModels/IProductQueries';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
import { defaultISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';

import { defaultISalesOrderDetailAdvancedQuery, ISalesOrderDetailAdvancedQuery } from 'src/dataModels/ISalesOrderDetailQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<ISalesOrderDetailAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();


    const iCustomerAdvancedQuery = defaultICustomerAdvancedQuery();
    const [customerCodeList, setCustomerCodeList] = useState<readonly INameValuePair[]>([]);
    const iAddressAdvancedQuery = defaultIAddressAdvancedQuery();
    const [addressCodeList, setAddressCodeList] = useState<readonly INameValuePair[]>([]);
    const iSalesOrderHeaderAdvancedQuery_SalesOrderID = defaultISalesOrderHeaderAdvancedQuery();
    const [salesOrderHeader_SalesOrderIDCodeList, setSalesOrderHeader_SalesOrderIDCodeList] = useState<readonly INameValuePair[]>([]);
    const iProductModelAdvancedQuery = defaultIProductModelAdvancedQuery();
    const [productModelCodeList, setProductModelCodeList] = useState<readonly INameValuePair[]>([]);
    const iProductCategoryAdvancedQuery = defaultIProductCategoryAdvancedQuery();
    const [productCategoryCodeList, setProductCategoryCodeList] = useState<readonly INameValuePair[]>([]);
    const iProductCategoryAdvancedQuery_ProductCategoryID = defaultIProductCategoryAdvancedQuery();
    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([]);
    const iProductAdvancedQuery_ProductID = defaultIProductAdvancedQuery();
    const [product_ProductIDCodeList, setProduct_ProductIDCodeList] = useState<readonly INameValuePair[]>([]);

    useEffect(() => {

        codeListsApi.getCustomerCodeList({ ...iCustomerAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setCustomerCodeList(res.responseBody);
            }
        });
        codeListsApi.getAddressCodeList({ ...iAddressAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setAddressCodeList(res.responseBody);
            }
        });
        codeListsApi.getProductModelCodeList({ ...iProductModelAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setProductModelCodeList(res.responseBody);
            }
        });
        codeListsApi.getProductCategoryCodeList({ ...iProductCategoryAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setProductCategoryCodeList(res.responseBody);
            }
        });
        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultISalesOrderDetailAdvancedQuery(),
    },);


    const handleModifiedDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('modifiedDateRangeLower', newRange.lowerBound);
        setValue('modifiedDateRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: ISalesOrderDetailAdvancedQuery) => {
        submitAction({ ...query });
        doneAction();
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={
                    <ButtonGroup sx={{ marginLeft: 'auto', }}
                        disableElevation
                        variant="contained"
                        aria-label="navigation buttons"
                    >
                        <LoadingButton
                            color="primary"
                            type='submit'
                            variant='contained'
                            startIcon={<SearchIcon color='action' />}>
                            {t('Search')}
                        </LoadingButton>
                        {!!doneAction && <Button
                            color="secondary"
                            autoFocus
                            variant='contained'
                            startIcon={<CloseIcon />}
                            onClick={() => { doneAction() }}
                        >
                            {t('Cancel')}
                        </Button>}
                    </ButtonGroup>
                }
                title={t("Product")}
                subheader={t("AdvancedQuery")}
            />
            <CardContent>
                <Grid container spacing={1}>
                    <Grid item xs={12} >
                        <TextField
                            label={t("textSearch")}
                            name='textSearch'
                            {...register("textSearch")}
                            autoComplete='textSearch'
                            fullWidth
                            autoFocus
                            variant='filled'
                            size="small"
                        />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<TextField
                    		label={t("ProductCategory")}
                    		id="productCategory_ParentIDSelect"
                    		select
                    		name='productCategory_ParentID'
                    		{...register("productCategory_ParentID")}
                    		autoComplete='productCategory_ParentID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.productCategory_ParentID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{productCategoryCodeList && productCategoryCodeList.map((v, index) => {
                    			return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    		})}
                    	</TextField>
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<TextField
                    		label={t("ProductModel")}
                    		id="productModelIDSelect"
                    		select
                    		name='productModelID'
                    		{...register("productModelID")}
                    		autoComplete='productModelID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.productModelID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{productModelCodeList && productModelCodeList.map((v, index) => {
                    			return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    		})}
                    	</TextField>
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<TextField
                    		label={t("Address")}
                    		id="billToIDSelect"
                    		select
                    		name='billToID'
                    		{...register("billToID")}
                    		autoComplete='billToID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.billToID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{addressCodeList && addressCodeList.map((v, index) => {
                    			return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    		})}
                    	</TextField>
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<TextField
                    		label={t("Address")}
                    		id="shipToIDSelect"
                    		select
                    		name='shipToID'
                    		{...register("shipToID")}
                    		autoComplete='shipToID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.shipToID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{addressCodeList && addressCodeList.map((v, index) => {
                    			return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    		})}
                    	</TextField>
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<TextField
                    		label={t("Customer")}
                    		id="customerIDSelect"
                    		select
                    		name='customerID'
                    		{...register("customerID")}
                    		autoComplete='customerID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.customerID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{customerCodeList && customerCodeList.map((v, index) => {
                    			return (<MenuItem key={v.value} value={v.value}>{v.name}</MenuItem>)
                    		})}
                    	</TextField>
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchDateTimeRangeField
                    		label={t("ModifiedDateRange")}
                    		rangeFieldName={"modifiedDateRange"}
                    		rangeDefaultValue={advancedQuery.modifiedDateRange}
                    		register={register}
                    		control={control}
                    		handleRangeChanged={handleModifiedDateRangeChanged} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
}

