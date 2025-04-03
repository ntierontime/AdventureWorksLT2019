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

import { ICustomerDataModel, customerFormValidation } from 'src/dataModels/ICustomerDataModel';


export default function EditPartial(props: ItemCardProps<ICustomerDataModel>): JSX.Element {
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
        resolver: yupResolver(customerFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;



    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: ICustomerDataModel) => {
		const dataWithProperDateTimeFormat = {...data, modifiedDate: dayjs(modifiedDate).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const title_Watched = watch("title");
    const modifiedDate_Watched = watch("modifiedDate");
    const avatar = getAvatar([title_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([title_Watched]);
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
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="CustomerEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="CustomerEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="CustomerEditForm" sx={{ ...card100PercentHeighFlex }} >
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
                        {item.title}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <TextField
                        name='customerID'
                        label={t('CustomerID')}
                        value={item.customerID}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <Controller
                        name='nameStyle'
                        control={control}
                        defaultValue={item.nameStyle}
                        render={({ field }) => (
                            <FormControlLabel
                                control={<Checkbox {...field} />}
                                label={
                                    <Typography>{t('NameStyle')}</Typography>
                                }
                            />
                        )}
                    />
                    <TextField
                        name='title'
                        label={t('Title')}
                        defaultValue={item.title}
                        variant='outlined'
                        margin='normal'
                        {...register("title")}
                        autoComplete='title'
                        error={!!errors.title}
                        fullWidth
                        helperText={!!errors.title ? t(errors.title.message) : ''}
                    />
                    <TextField
                        name='firstName'
                        label={t('FirstName')}
                        defaultValue={item.firstName}
                        variant='outlined'
                        margin='normal'
                        {...register("firstName")}
                        autoComplete='firstName'
                        error={!!errors.firstName}
                        fullWidth
                        helperText={!!errors.firstName ? t(errors.firstName.message) : ''}
                    />
                    <TextField
                        name='middleName'
                        label={t('MiddleName')}
                        defaultValue={item.middleName}
                        variant='outlined'
                        margin='normal'
                        {...register("middleName")}
                        autoComplete='middleName'
                        error={!!errors.middleName}
                        fullWidth
                        helperText={!!errors.middleName ? t(errors.middleName.message) : ''}
                    />
                    <TextField
                        name='lastName'
                        label={t('LastName')}
                        defaultValue={item.lastName}
                        variant='outlined'
                        margin='normal'
                        {...register("lastName")}
                        autoComplete='lastName'
                        error={!!errors.lastName}
                        fullWidth
                        helperText={!!errors.lastName ? t(errors.lastName.message) : ''}
                    />
                    <TextField
                        name='suffix'
                        label={t('Suffix')}
                        defaultValue={item.suffix}
                        variant='outlined'
                        margin='normal'
                        {...register("suffix")}
                        autoComplete='suffix'
                        error={!!errors.suffix}
                        fullWidth
                        helperText={!!errors.suffix ? t(errors.suffix.message) : ''}
                    />
                    <TextField
                        name='companyName'
                        label={t('CompanyName')}
                        defaultValue={item.companyName}
                        variant='outlined'
                        margin='normal'
                        {...register("companyName")}
                        autoComplete='companyName'
                        error={!!errors.companyName}
                        fullWidth
                        helperText={!!errors.companyName ? t(errors.companyName.message) : ''}
                    />
                    <TextField
                        name='salesPerson'
                        label={t('SalesPerson')}
                        defaultValue={item.salesPerson}
                        variant='outlined'
                        margin='normal'
                        {...register("salesPerson")}
                        autoComplete='salesPerson'
                        error={!!errors.salesPerson}
                        fullWidth
                        helperText={!!errors.salesPerson ? t(errors.salesPerson.message) : ''}
                    />
                    <TextField
                        name='emailAddress'
                        label={t('EmailAddress')}
                        defaultValue={item.emailAddress}
                        variant='outlined'
                        margin='normal'
                        {...register("emailAddress")}
                        autoComplete='emailAddress'
                        error={!!errors.emailAddress}
                        fullWidth
                        helperText={!!errors.emailAddress ? t(errors.emailAddress.message) : ''}
                    />
                    <TextField
                        name='phone'
                        label={t('Phone')}
                        defaultValue={item.phone}
                        variant='outlined'
                        margin='normal'
                        {...register("phone")}
                        autoComplete='phone'
                        error={!!errors.phone}
                        fullWidth
                        helperText={!!errors.phone ? t(errors.phone.message) : ''}
                    />
                    <TextField
                        name='passwordHash'
                        label={t('PasswordHash')}
                        defaultValue={item.passwordHash}
                        variant='outlined'
                        margin='normal'
                        {...register("passwordHash")}
                        autoComplete='passwordHash'
                        error={!!errors.passwordHash}
                        fullWidth
                        helperText={!!errors.passwordHash ? t(errors.passwordHash.message) : ''}
                    />
                    <TextField
                        name='passwordSalt'
                        label={t('PasswordSalt')}
                        defaultValue={item.passwordSalt}
                        variant='outlined'
                        margin='normal'
                        {...register("passwordSalt")}
                        autoComplete='passwordSalt'
                        error={!!errors.passwordSalt}
                        fullWidth
                        helperText={!!errors.passwordSalt ? t(errors.passwordSalt.message) : ''}
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

