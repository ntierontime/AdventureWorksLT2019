import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';

import { Controller } from 'react-hook-form';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getCustomerAvatar, ICustomerDataModel, customerFormValidationWhenEdit } from 'src/dataModels/ICustomerDataModel';
import { getICustomerIdentifier } from 'src/dataModels/ICustomerQueries';
import { put } from 'src/slices/CustomerSlice';

export default function EditPartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(customerFormValidationWhenEdit)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);






    const onSubmit = (data: ICustomerDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: getICustomerIdentifier(data), data: { ...data } }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error: any) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getCustomerAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroup_IconButtons = () => {
        return (
            <>                {!!handleSelectItemClick && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                ><Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                /></ButtonGroup>}
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
                <IconButton aria-label="Save"
                    color="primary"
                    type='submit'
                    disabled={!isValid || saving || saved}
                >
                    <SaveIcon />
                </IconButton>
                {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
                {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <ArrowBackIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
        return (
            <>
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
                    {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                    {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<ArrowBackIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Back')}
                    </Button>}
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.title}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.title}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {crudViewContainer !== CrudViewContainers.Wizard && !!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.title}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <FormControlLabel
                                control={
                                    <Controller
                                        control={control}
                                        name="nameStyle"
                                        defaultValue={item.nameStyle}
                            			{...register("nameStyle")}
                                        render={({ field: { onChange } }) => (
                                            <Checkbox
                                                color="primary"
                                                onChange={(e) => onChange(e.target.checked)}
                                            />
                                        )}
                                    />
                                }
                                label={
                                    <Typography>{t('NameStyle')}</Typography>
                                }
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="modifiedDate"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={modifiedDate}
                            				label={t('ModifiedDate')}
                            				onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.modifiedDate}
                            						helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}

