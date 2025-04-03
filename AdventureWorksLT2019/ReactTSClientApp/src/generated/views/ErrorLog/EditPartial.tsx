import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, Dialog, FormControlLabel, FormLabel, Grid, IconButton, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { DatePicker } from '@mui/x-date-pickers';
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

import { IErrorLogDataModel, errorLogFormValidation } from 'src/dataModels/IErrorLogDataModel';


export default function EditPartial(props: ItemCardProps<IErrorLogDataModel>): JSX.Element {
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
        resolver: yupResolver(errorLogFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;



    const [errorTime, setErrorTime] = useState<string>();
    useEffect(() => {

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: IErrorLogDataModel) => {
		const dataWithProperDateTimeFormat = {...data, errorTime: dayjs(errorTime).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const userName_Watched = watch("userName");
    const errorTime_Watched = watch("errorTime");
    const avatar = getAvatar([userName_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([userName_Watched]);
    const subheader = !!errorTime_Watched
        ? t(i18nFormats.dateTime.format, { val: new Date(errorTime_Watched), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;


    const renderMainButtonGroup = () => {
        const extraButtonGroups = (
            <>

            </>
        );

        if (mainButtonType === ButtonTypes.IconAndText) {
            return (
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ErrorLogEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ErrorLogEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="ErrorLogEditForm" sx={{ ...card100PercentHeighFlex }} >
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
                        {item.userName}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <TextField
                        name='errorLogID'
                        label={t('ErrorLogID')}
                        value={item.errorLogID}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={errorTime}
                        // value={errorTime}
                        label={t('ErrorTime')}
                        onChange={(event: string) => { setErrorTime(event); setValue('errorTime', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <TextField
                        name='userName'
                        label={t('UserName')}
                        defaultValue={item.userName}
                        variant='outlined'
                        margin='normal'
                        {...register("userName")}
                        autoComplete='userName'
                        error={!!errors.userName}
                        fullWidth
                        helperText={!!errors.userName ? t(errors.userName.message) : ''}
                    />
                    <TextField
                        name='errorNumber'
                        label={t('ErrorNumber')}
                        defaultValue={item.errorNumber}
                        variant='outlined'
                        margin='normal'
                        {...register("errorNumber")}
                        autoComplete='errorNumber'
                        error={!!errors.errorNumber}
                        fullWidth
                        helperText={!!errors.errorNumber ? t(errors.errorNumber.message) : ''}
                    />
                    <TextField
                        name='errorSeverity'
                        label={t('ErrorSeverity')}
                        defaultValue={item.errorSeverity}
                        variant='outlined'
                        margin='normal'
                        {...register("errorSeverity")}
                        autoComplete='errorSeverity'
                        error={!!errors.errorSeverity}
                        fullWidth
                        //helperText={!!errors.errorSeverity ? t(errors.errorSeverity.message) : ''}
                    />
                    <TextField
                        name='errorState'
                        label={t('ErrorState')}
                        defaultValue={item.errorState}
                        variant='outlined'
                        margin='normal'
                        {...register("errorState")}
                        autoComplete='errorState'
                        error={!!errors.errorState}
                        fullWidth
                        //helperText={!!errors.errorState ? t(errors.errorState.message) : ''}
                    />
                    <TextField
                        name='errorProcedure'
                        label={t('ErrorProcedure')}
                        defaultValue={item.errorProcedure}
                        variant='outlined'
                        margin='normal'
                        {...register("errorProcedure")}
                        autoComplete='errorProcedure'
                        error={!!errors.errorProcedure}
                        fullWidth
                        helperText={!!errors.errorProcedure ? t(errors.errorProcedure.message) : ''}
                    />
                    <TextField
                        name='errorLine'
                        label={t('ErrorLine')}
                        defaultValue={item.errorLine}
                        variant='outlined'
                        margin='normal'
                        {...register("errorLine")}
                        autoComplete='errorLine'
                        error={!!errors.errorLine}
                        fullWidth
                        //helperText={!!errors.errorLine ? t(errors.errorLine.message) : ''}
                    />
                    <TextField
                        name='errorMessage'
                        label={t('ErrorMessage')}
                        defaultValue={item.errorMessage}
                        variant='outlined'
                        margin='normal'
                        {...register("errorMessage")}
                        autoComplete='errorMessage'
                        error={!!errors.errorMessage}
                        fullWidth
                        helperText={!!errors.errorMessage ? t(errors.errorMessage.message) : ''}
                    />
                </Box>
            </CardContent>
            {mainButtonContainer === CardButtonGroupPosition.Bottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderMainButtonGroup()}
            </CardActions>}
        </Card >
    );
}

