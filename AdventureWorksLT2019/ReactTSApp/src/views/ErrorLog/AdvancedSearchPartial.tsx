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


import { defaultIErrorLogAdvancedQuery, IErrorLogAdvancedQuery } from 'src/dataModels/IErrorLogQueries';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<IErrorLogAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
    const { t } = useTranslation();



    useEffect(() => {

        reset(advancedQuery);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultIErrorLogAdvancedQuery(),
    },);


    const handleErrorTimeRangeChanged = (event: any) => {
        const newRange = getDateRange(new Date(), event.target.value);
        // console.log(newRange);
        setValue('errorTimeRangeLower', newRange.lowerBound);
        setValue('errorTimeRangeUpper', newRange.upperBound);
    }


    const onSubmit = (query: IErrorLogAdvancedQuery) => {
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
                    		label={t("ErrorTimeRange")}
                    		rangeFieldName={"errorTimeRange"}
                    		rangeDefaultValue={advancedQuery.errorTimeRange}
                    		register={register}
                    		control={control}
                    		handleRangeChanged={handleErrorTimeRangeChanged} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("UserName")}
                    		fieldName={'userName'}
                    		searchTypeFieldName={'userNameSearchType'}
                    		searchTypeDefaultValue={advancedQuery.userNameSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("ErrorProcedure")}
                    		fieldName={'errorProcedure'}
                    		searchTypeFieldName={'errorProcedureSearchType'}
                    		searchTypeDefaultValue={advancedQuery.errorProcedureSearchType}
                    		register={register} />
                    </Grid>
                    <Grid item xs={12} md={6} lg={4} >
                    	<SearchStringTextField
                    		label={t("ErrorMessage")}
                    		fieldName={'errorMessage'}
                    		searchTypeFieldName={'errorMessageSearchType'}
                    		searchTypeDefaultValue={advancedQuery.errorMessageSearchType}
                    		register={register} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
}

