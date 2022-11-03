import { useEffect,  } from 'react';
import { Button, ButtonGroup, Card, CardContent, CardHeader, Grid, TextField } from '@mui/material';
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


import { defaultICustomerAdvancedQuery, ICustomerAdvancedQuery } from 'src/dataModels/ICustomerQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<ICustomerAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();



    useEffect(() => {

        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultICustomerAdvancedQuery(),
    },);


    const handleModifiedDateRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('modifiedDateRangeLower', newRange.lowerBound);
        setValue('modifiedDateRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: ICustomerAdvancedQuery) => {
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
                    		label={t("Title")}
                    		fieldName={'title'}
                    		searchTypeFieldName={'titleSearchType'}
                    		searchTypeDefaultValue={advancedQuery.titleSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("FirstName")}
                    		fieldName={'firstName'}
                    		searchTypeFieldName={'firstNameSearchType'}
                    		searchTypeDefaultValue={advancedQuery.firstNameSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("MiddleName")}
                    		fieldName={'middleName'}
                    		searchTypeFieldName={'middleNameSearchType'}
                    		searchTypeDefaultValue={advancedQuery.middleNameSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("LastName")}
                    		fieldName={'lastName'}
                    		searchTypeFieldName={'lastNameSearchType'}
                    		searchTypeDefaultValue={advancedQuery.lastNameSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("Suffix")}
                    		fieldName={'suffix'}
                    		searchTypeFieldName={'suffixSearchType'}
                    		searchTypeDefaultValue={advancedQuery.suffixSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("CompanyName")}
                    		fieldName={'companyName'}
                    		searchTypeFieldName={'companyNameSearchType'}
                    		searchTypeDefaultValue={advancedQuery.companyNameSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("SalesPerson")}
                    		fieldName={'salesPerson'}
                    		searchTypeFieldName={'salesPersonSearchType'}
                    		searchTypeDefaultValue={advancedQuery.salesPersonSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("EmailAddress")}
                    		fieldName={'emailAddress'}
                    		searchTypeFieldName={'emailAddressSearchType'}
                    		searchTypeDefaultValue={advancedQuery.emailAddressSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("Phone")}
                    		fieldName={'phone'}
                    		searchTypeFieldName={'phoneSearchType'}
                    		searchTypeDefaultValue={advancedQuery.phoneSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("PasswordHash")}
                    		fieldName={'passwordHash'}
                    		searchTypeFieldName={'passwordHashSearchType'}
                    		searchTypeDefaultValue={advancedQuery.passwordHashSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("PasswordSalt")}
                    		fieldName={'passwordSalt'}
                    		searchTypeFieldName={'passwordSaltSearchType'}
                    		searchTypeDefaultValue={advancedQuery.passwordSaltSearchType}
                    		register={register} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
}

