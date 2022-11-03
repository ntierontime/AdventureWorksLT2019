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
import { SearchStringTextField } from 'src/shared/views/SearchStringTextField';
import { getDateRange } from 'src/shared/dataModels/PreDefinedDateTimeRanges';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIAddressAdvancedQuery } from 'src/dataModels/IAddressQueries';
import { defaultICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

import { defaultISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderAdvancedQuery } from 'src/dataModels/ISalesOrderHeaderQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<ISalesOrderHeaderAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();


    const iCustomerAdvancedQuery = defaultICustomerAdvancedQuery();
    const [customerCodeList, setCustomerCodeList] = useState<readonly INameValuePair[]>([]);
    const iAddressAdvancedQuery = defaultIAddressAdvancedQuery();
    const [addressCodeList, setAddressCodeList] = useState<readonly INameValuePair[]>([]);

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
        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultISalesOrderHeaderAdvancedQuery(),
    },);


    const handleOrderDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('orderDateRangeLower', newRange.lowerBound);
        setValue('orderDateRangeUpper', newRange.upperBound);
    }
    const handleDueDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('dueDateRangeLower', newRange.lowerBound);
        setValue('dueDateRangeUpper', newRange.upperBound);
    }
    const handleShipDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('shipDateRangeLower', newRange.lowerBound);
        setValue('shipDateRangeUpper', newRange.upperBound);
    }
    const handleModifiedDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('modifiedDateRangeLower', newRange.lowerBound);
        setValue('modifiedDateRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: ISalesOrderHeaderAdvancedQuery) => {
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
                    		label={t("Address")}
                    		id="billToAddressIDSelect"
                    		select
                    		name='billToAddressID'
                    		{...register("billToAddressID")}
                    		autoComplete='billToAddressID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.billToAddressID ?? ""}
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
                    		id="shipToAddressIDSelect"
                    		select
                    		name='shipToAddressID'
                    		{...register("shipToAddressID")}
                    		autoComplete='shipToAddressID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.shipToAddressID ?? ""}
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
                    		label={t("OrderDateRange")}
                    		rangeFieldName={"orderDateRange"}
                    		rangeDefaultValue={advancedQuery.orderDateRange}
                    		register={register}
                    		control={control}
                    		handleRangeChanged={handleOrderDateRangeChanged} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchDateTimeRangeField
                    		label={t("DueDateRange")}
                    		rangeFieldName={"dueDateRange"}
                    		rangeDefaultValue={advancedQuery.dueDateRange}
                    		register={register}
                    		control={control}
                    		handleRangeChanged={handleDueDateRangeChanged} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchDateTimeRangeField
                    		label={t("ShipDateRange")}
                    		rangeFieldName={"shipDateRange"}
                    		rangeDefaultValue={advancedQuery.shipDateRange}
                    		register={register}
                    		control={control}
                    		handleRangeChanged={handleShipDateRangeChanged} />
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
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("SalesOrderNumber")}
                    		fieldName={'salesOrderNumber'}
                    		searchTypeFieldName={'salesOrderNumberSearchType'}
                    		searchTypeDefaultValue={advancedQuery.salesOrderNumberSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("PurchaseOrderNumber")}
                    		fieldName={'purchaseOrderNumber'}
                    		searchTypeFieldName={'purchaseOrderNumberSearchType'}
                    		searchTypeDefaultValue={advancedQuery.purchaseOrderNumberSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("AccountNumber")}
                    		fieldName={'accountNumber'}
                    		searchTypeFieldName={'accountNumberSearchType'}
                    		searchTypeDefaultValue={advancedQuery.accountNumberSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("ShipMethod")}
                    		fieldName={'shipMethod'}
                    		searchTypeFieldName={'shipMethodSearchType'}
                    		searchTypeDefaultValue={advancedQuery.shipMethodSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("CreditCardApprovalCode")}
                    		fieldName={'creditCardApprovalCode'}
                    		searchTypeFieldName={'creditCardApprovalCodeSearchType'}
                    		searchTypeDefaultValue={advancedQuery.creditCardApprovalCodeSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("Comment")}
                    		fieldName={'comment'}
                    		searchTypeFieldName={'commentSearchType'}
                    		searchTypeDefaultValue={advancedQuery.commentSearchType}
                    		register={register} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
}

