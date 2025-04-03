import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, Dialog, FormControlLabel, FormLabel, Grid, IconButton, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';
import dayjs from 'dayjs';
import { Controller } from 'react-hook-form';
// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { card100PercentHeighFlex, ItemCardProps } from 'src/shared/views/ItemCardProps';
import { getAvatar, getAvatarStyle } from 'src/shared/avatarUtility';
import { getTitle } from 'src/shared/captionTextUtility';
import { EditItemActionIconButtonGroup, EditItemActionIconAndTextButtonGroup } from 'src/shared/views/buttonGroups/EditItemActionButtonGroups';
import { ButtonTypes } from 'src/shared/views/buttonGroups/ButtonTypes';
import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { RatingSummary } from 'src/shared/views/rating/RatingSummary';

import { IAddressDataModel, addressFormValidation } from 'src/dataModels/IAddressDataModel';


export default function EditPartial(props: ItemCardProps<IAddressDataModel>): JSX.Element {
    const { t } = useTranslation();

    const {
        mainButtonContainer, mainButtonType,
        gridColumns, scrollableCardContent,
        item, itemIndex,
        submitAction, submitting, submitted, submitMessage,
        doneAction, previousAction, nextAction,
        showCloseIconOnTopRight, showCardHeader,
        isItemSelected, handleSelectItemClick,
    } = props;

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(addressFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;



    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: IAddressDataModel) => {
		const dataWithProperDateTimeFormat = {...data, modifiedDate: dayjs(modifiedDate).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const addressLine1_Watched = watch("addressLine1");
    const modifiedDate_Watched = watch("modifiedDate");
    const avatar = getAvatar([addressLine1_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([addressLine1_Watched]);
    const subheader = !!modifiedDate_Watched
        ? t(i18nFormats.dateTime.format, { val: new Date(modifiedDate_Watched), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;


    const renderMainButtonGroup = () => {
        const extraButtonGroups = (
            <>

            </>
        );

        if (mainButtonType === ButtonTypes.IconAndText) {
            return (
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="AddressEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="AddressEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="AddressEditForm" sx={{ ...card100PercentHeighFlex }} >
            {showCardHeader && <CardHeader
                avatar={
                    <Avatar
                        sx={avatarStyle}>
                            {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {mainButtonContainer === CardButtonGroupPosition.CardHeader && <>
                            {renderMainButtonGroup()}
                        </>}
                        {(showCloseIconOnTopRight && !!doneAction) && <IconButton aria-label="edit" color="primary" onClick={() => { doneAction() }}>
                            <CloseIcon />
                        </IconButton>}
                    </>}
                title={title}
                subheader={subheader}
            />}
            {mainButtonContainer === CardButtonGroupPosition.BelowCardHeader && <CardActions disableSpacing>
                {renderMainButtonGroup()}
            </CardActions>}
            {!!submitMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {submitMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.addressLine1}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <TextField
                        name='addressID'
                        label={t('AddressID')}
                        value={item.addressID}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='addressLine1'
                        label={t('AddressLine1')}
                        defaultValue={item.addressLine1}
                        variant='outlined'
                        margin='normal'
                        {...register("addressLine1")}
                        autoComplete='addressLine1'
                        error={!!errors.addressLine1}
                        fullWidth
                        helperText={!!errors.addressLine1 ? t(errors.addressLine1.message) : ''}
                    />
                    <TextField
                        name='addressLine2'
                        label={t('AddressLine2')}
                        defaultValue={item.addressLine2}
                        variant='outlined'
                        margin='normal'
                        {...register("addressLine2")}
                        autoComplete='addressLine2'
                        error={!!errors.addressLine2}
                        fullWidth
                        helperText={!!errors.addressLine2 ? t(errors.addressLine2.message) : ''}
                    />
                    <TextField
                        name='city'
                        label={t('City')}
                        defaultValue={item.city}
                        variant='outlined'
                        margin='normal'
                        {...register("city")}
                        autoComplete='city'
                        error={!!errors.city}
                        fullWidth
                        helperText={!!errors.city ? t(errors.city.message) : ''}
                    />
                    <TextField
                        name='stateProvince'
                        label={t('StateProvince')}
                        defaultValue={item.stateProvince}
                        variant='outlined'
                        margin='normal'
                        {...register("stateProvince")}
                        autoComplete='stateProvince'
                        error={!!errors.stateProvince}
                        fullWidth
                        helperText={!!errors.stateProvince ? t(errors.stateProvince.message) : ''}
                    />
                    <TextField
                        name='countryRegion'
                        label={t('CountryRegion')}
                        defaultValue={item.countryRegion}
                        variant='outlined'
                        margin='normal'
                        {...register("countryRegion")}
                        autoComplete='countryRegion'
                        error={!!errors.countryRegion}
                        fullWidth
                        helperText={!!errors.countryRegion ? t(errors.countryRegion.message) : ''}
                    />
                    <TextField
                        name='postalCode'
                        label={t('PostalCode')}
                        defaultValue={item.postalCode}
                        variant='outlined'
                        margin='normal'
                        {...register("postalCode")}
                        autoComplete='postalCode'
                        error={!!errors.postalCode}
                        fullWidth
                        helperText={!!errors.postalCode ? t(errors.postalCode.message) : ''}
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
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={modifiedDate}
                        // value={modifiedDate}
                        label={t('ModifiedDate')}
                        onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                </Box>
            </CardContent>
            {mainButtonContainer === CardButtonGroupPosition.Bottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderMainButtonGroup()}
            </CardActions>}
        </Card >
    );
}

