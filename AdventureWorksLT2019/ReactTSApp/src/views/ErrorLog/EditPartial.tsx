import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
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

import { getErrorLogAvatar, IErrorLogDataModel, errorLogFormValidationWhenEdit } from 'src/dataModels/IErrorLogDataModel';
import { put } from 'src/slices/ErrorLogSlice';

export default function EditPartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
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




    const onSubmit = (data: IErrorLogDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { errorLogID: data.errorLogID }, data: { ...data } }))
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
    const avatar = getErrorLogAvatar(item);
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
                    <Avatar sx={avatarStyle} aria-label={item.userName}>
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
                title={item.userName}
                subheader={t('{{val, datetime}}', { val: new Date(item.errorTime) })}
            />
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.userName}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
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
                <Controller
                    name="errorTime"
                    defaultValue={item.errorTime}
                    control={control}
                    {...register("errorTime", errorLogFormValidationWhenEdit.errorTime)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                                ref={null}
                                label={t('ErrorTime')}
                                onChange={(event) => { onChange(event); }}
                                renderInput={(params) =>
                                    <TextField
                                        ref={null}
                                        fullWidth
                                        autoComplete='errorTime'
                                        error={!!errors.errorTime}
                                        helperText={!!errors.errorTime ? t(errors.errorTime.message) : ''}
                                        {...params}
                                    />}
                                {...restField}
                            />
                    }
                />
                <TextField
                    name='userName'
                    label={t('UserName')}
                    defaultValue={item.userName}
                    variant='outlined'
                    margin='normal'
                    {...register("userName", errorLogFormValidationWhenEdit.userName)}
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
                    {...register("errorNumber", errorLogFormValidationWhenEdit.errorNumber)}
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
                    {...register("errorSeverity", errorLogFormValidationWhenEdit.errorSeverity)}
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
                    {...register("errorState", errorLogFormValidationWhenEdit.errorState)}
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
                    {...register("errorProcedure", errorLogFormValidationWhenEdit.errorProcedure)}
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
                    {...register("errorLine", errorLogFormValidationWhenEdit.errorLine)}
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
                    {...register("errorMessage", errorLogFormValidationWhenEdit.errorMessage)}
                    autoComplete='errorMessage'
                    error={!!errors.errorMessage}
                    fullWidth
                    helperText={!!errors.errorMessage ? t(errors.errorMessage.message) : ''}
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


