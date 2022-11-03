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
import { defaultIProductDescriptionAdvancedQuery } from 'src/dataModels/IProductDescriptionQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';

import { defaultIProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionAdvancedQuery } from 'src/dataModels/IProductModelProductDescriptionQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<IProductModelProductDescriptionAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();


    const iProductModelAdvancedQuery = defaultIProductModelAdvancedQuery();
    const [productModelCodeList, setProductModelCodeList] = useState<readonly INameValuePair[]>([]);
    const iProductDescriptionAdvancedQuery = defaultIProductDescriptionAdvancedQuery();
    const [productDescriptionCodeList, setProductDescriptionCodeList] = useState<readonly INameValuePair[]>([]);

    useEffect(() => {

        codeListsApi.getProductModelCodeList({ ...iProductModelAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setProductModelCodeList(res.responseBody);
            }
        });
        codeListsApi.getProductDescriptionCodeList({ ...iProductDescriptionAdvancedQuery, pageSize: 10000 }).then((res) => {
            if (res.status === 'OK') {
                setProductDescriptionCodeList(res.responseBody);
            }
        });
        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultIProductModelProductDescriptionAdvancedQuery(),
    },);


    const handleModifiedDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('modifiedDateRangeLower', newRange.lowerBound);
        setValue('modifiedDateRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: IProductModelProductDescriptionAdvancedQuery) => {
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
                    		label={t("ProductDescription")}
                    		id="productDescriptionIDSelect"
                    		select
                    		name='productDescriptionID'
                    		{...register("productDescriptionID")}
                    		autoComplete='productDescriptionID'
                    		variant="filled"
                    		size="small"
                    		fullWidth
                    		defaultValue={advancedQuery.productDescriptionID ?? ""}
                    	>
                    		<MenuItem key={''} value={''}>{t("All")}</MenuItem>
                    		{productDescriptionCodeList && productDescriptionCodeList.map((v, index) => {
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
                    		label={t("Culture")}
                    		fieldName={'culture'}
                    		searchTypeFieldName={'cultureSearchType'}
                    		searchTypeDefaultValue={advancedQuery.cultureSearchType}
                    		register={register} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
}

