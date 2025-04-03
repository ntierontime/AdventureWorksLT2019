import { useEffect, useState } from 'react';
import { Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Grid } from '@mui/material';

import LoadingButton from '@mui/lab/LoadingButton';

import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import CloseIcon from '@mui/icons-material/Close';
import ManageSearchIcon from '@mui/icons-material/ManageSearch';
import MapIcon from '@mui/icons-material/Map';
import RefreshIcon from '@mui/icons-material/Refresh';
import SearchIcon from '@mui/icons-material/Search';

import { useDispatch, useSelector } from 'react-redux';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"
import dayjs from 'dayjs';

import { RootState } from 'src/store/CombinedReducers';
import { AppDispatch } from 'src/store/Store';

import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { scrollableCardContent } from 'src/shared/views/ItemCardProps';
import { AdvancedSearchPartialViewProps } from 'src/shared/viewModels/AdvancedSearchPartialViewProps';

import { defaultIErrorLogAdvancedQuery, IErrorLogAdvancedQuery } from 'src/dataModels/IErrorLogQueries';
import { setIErrorLogAdvancedQuery } from 'src/generated/slices/ErrorLogSlice';

export default function AdvancedSearchPartial(props: AdvancedSearchPartialViewProps<IErrorLogAdvancedQuery>): JSX.Element {
    const { advancedQuery, submitAction, doneAction } = props;
	const { geoLocation } = useSelector((state: RootState) => state.app);
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, handleSubmit, reset, setValue } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultIErrorLogAdvancedQuery(),
    },);


    const onSubmit = (query: IErrorLogAdvancedQuery) => {
	    // TODO: any query transformations are here
        const newQuery = {
            ...query,
            // spatialLocation: geoLocation.point,
            // selectedServiceCategories: selectedServiceCategories.map(v => v.serviceCategoryID),
            // selectedSpecializations: selectedSpecializations.map(v1 => v1.specializationID)
        } as unknown as IErrorLogAdvancedQuery;
        // console.log(newQuery);

        submitAction(newQuery);
        doneAction();
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                title={t("SearchErrorLog")}
            />
            <CardContent>
                <Grid container sx={{ flexGrow: 1 }}>

                </Grid>
            </CardContent>
            <CardActions disableSpacing
                sx={{
                    alignSelf: "stretch",
                    display: "flex",
                    justifyContent: "flex-end",
                    alignItems: "flex-start",
                    // ?? Edit padding to further adjust position
                    p: 0,
                }}>
                <ButtonGroup>
                    <Button
                        color="secondary"
                        autoFocus
                        variant='contained'
                        startIcon={<RefreshIcon />}
                        onClick={() => { dispatch(setIErrorLogAdvancedQuery(defaultIErrorLogAdvancedQuery())) }}
                    >
                        {t('ReSet')}
                    </Button>
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
            </CardActions>
        </Card >
    );
}



