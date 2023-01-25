import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography } from '@mui/material';
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
import { defaultErrorLog, IErrorLogDataModel, errorLogFormValidationWhenCreate } from 'src/dataModels/IErrorLogDataModel';
import { post } from 'src/slices/ErrorLogSlice';

export default function CreatePartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<IErrorLogDataModel>(defaultErrorLog());
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, setValue, handleSubmit, reset, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    });

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };


    const [errorTime, setErrorTime] = useState<string>();
    useEffect(() => {

        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = () => {
        setCreating(true);
        dispatch(post({ ...item }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
                        setItem(defaultErrorLog());
                        reset(item);
                    }
                    else {
                        setCreateMessage(t('SuccessfullySaved'));
                        setCreated(true);
                    }
                }
                else { // failed
                    setCreateMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || creating || created || !isDirty}>
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" disabled={creating || created}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        disabled={!isValid || creating || created || !isDirty}
                        startIcon={<SaveIcon />}>
                        {t('Create')}
                    </Button>
                    <Button
                        autoFocus
                        disabled={creating || created}
                        fullWidth
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("ErrorLog")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={errorTime}
                                label={t('ErrorTime')}
                                onChange={(event: string) => { setErrorTime(event); setValue('errorTime', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='errorTime'
                            			{...register("errorTime", errorLogFormValidationWhenCreate.errorTime)}
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
                                {...register("userName", errorLogFormValidationWhenCreate.userName)}
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
                                {...register("errorNumber", errorLogFormValidationWhenCreate.errorNumber)}
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
                                {...register("errorSeverity", errorLogFormValidationWhenCreate.errorSeverity)}
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
                                {...register("errorState", errorLogFormValidationWhenCreate.errorState)}
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
                                {...register("errorProcedure", errorLogFormValidationWhenCreate.errorProcedure)}
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
                                {...register("errorLine", errorLogFormValidationWhenCreate.errorLine)}
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
                                {...register("errorMessage", errorLogFormValidationWhenCreate.errorMessage)}
                                autoComplete='errorMessage'
                                error={!!errors.errorMessage}
                                fullWidth
                                helperText={!!errors.errorMessage ? t(errors.errorMessage.message) : ''}
                            />
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}


