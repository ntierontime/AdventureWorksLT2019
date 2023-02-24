import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getErrorLogAvatar, IErrorLogDataModel, errorLogFormValidationWhenEdit } from 'src/dataModels/IErrorLogDataModel';
import { getIErrorLogIdentifier } from 'src/dataModels/IErrorLogQueries';
import { put } from 'src/slices/ErrorLogSlice';

export default function EditPartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    const [errorTime, setErrorTime] = useState<string>();
    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = (data: IErrorLogDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: getIErrorLogIdentifier(data), data: { ...data } }))
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
                    disabled={!isValid || saving || saved || !isDirty}
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
                        disabled={!isValid || saving || saved || !isDirty}
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
                    <Avatar sx={avatarStyle} aria-label={item.userName}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.userName}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.errorTime), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {crudViewContainer !== CrudViewContainers.Wizard && !!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.userName}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={errorTime}
                                label={t('ErrorTime')}
                                onChange={(event: string) => { setErrorTime(event); setValue('errorTime', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='errorTime'
                            			{...register("errorTime", errorLogFormValidationWhenEdit.errorTime)}
                                        error={!!errors.errorTime}
                                        helperText={!!errors.errorTime ? t(errors.errorTime.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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

