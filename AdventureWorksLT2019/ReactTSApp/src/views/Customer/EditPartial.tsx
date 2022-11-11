import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';

import { useNavigate } from "react-router-dom";
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';


// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getCustomerAvatar, ICustomerDataModel, customerFormValidationWhenEdit } from 'src/dataModels/ICustomerDataModel';
import { put } from 'src/slices/CustomerSlice';

export default function EditPartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = (data: ICustomerDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { customerID: data.customerID }, data: { ...data } }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getCustomerAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton
                    color="primary"
                    type='submit'
                    disabled={(!isValid || saving || saved) && !isDirty}
                    aria-label="save">
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenDialog = () => {
        return (
            <>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <LoadingButton
                    color="primary"
                    type='submit'
                    variant='contained'
                    disabled={(!isValid || saving || saved) && !isDirty}
                    startIcon={<SaveIcon color='action' />}>
                    {t('Save')}
                </LoadingButton>
                <IconButton aria-label="close"
                    onClick={() => {
                        navigate(-1);
                    }} disabled={saving}
                >
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.title}>
                        {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {crudViewContainer === CrudViewContainers.Card && (renderButtonGroupWhenCard())}
                        {crudViewContainer === CrudViewContainers.Dialog && (renderButtonGroupWhenDialog())}
                        {crudViewContainer === CrudViewContainers.Inline && (renderButtonGroupWhenInline())}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && (renderButtonGroupWhenStandaloneView())}
                    </>
                }
                title={item.title}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.title}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
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
                <FormControlLabel
                    control={
                        <Controller
                            control={control}
                            name="nameStyle"
                            defaultValue={item.nameStyle}
                            {...register("nameStyle", customerFormValidationWhenEdit.nameStyle)}
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
                <TextField
                    name='title'
                    label={t('Title')}
                    defaultValue={item.title}
                    variant='outlined'
                    margin='normal'
                    {...register("title", customerFormValidationWhenEdit.title)}
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
                    {...register("firstName", customerFormValidationWhenEdit.firstName)}
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
                    {...register("middleName", customerFormValidationWhenEdit.middleName)}
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
                    {...register("lastName", customerFormValidationWhenEdit.lastName)}
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
                    {...register("suffix", customerFormValidationWhenEdit.suffix)}
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
                    {...register("companyName", customerFormValidationWhenEdit.companyName)}
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
                    {...register("salesPerson", customerFormValidationWhenEdit.salesPerson)}
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
                    {...register("emailAddress", customerFormValidationWhenEdit.emailAddress)}
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
                    {...register("phone", customerFormValidationWhenEdit.phone)}
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
                    {...register("passwordHash", customerFormValidationWhenEdit.passwordHash)}
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
                    {...register("passwordSalt", customerFormValidationWhenEdit.passwordSalt)}
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
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", customerFormValidationWhenEdit.modifiedDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('ModifiedDate')}
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                                        ref={null}
                                        fullWidth
                                        autoComplete='modifiedDate'
                                        error={!!errors.modifiedDate}
                                        helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
            </CardContent>
            {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <CardActions disableSpacing>
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
                    {!!doneAction && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                </ButtonGroup>
            </CardActions>}
        </Card >
    );
}


