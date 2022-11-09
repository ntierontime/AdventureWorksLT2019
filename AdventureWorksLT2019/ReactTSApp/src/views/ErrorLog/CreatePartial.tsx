import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';


import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultErrorLog, IErrorLogDataModel, errorLogFormValidationWhenCreate } from 'src/dataModels/IErrorLogDataModel';
import { post } from 'src/slices/ErrorLogSlice';

export default function CreatePartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<IErrorLogDataModel>(defaultErrorLog());
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

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

    const renderButtonGroupWhenDialog = () => {
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
                        disabled={!isValid || creating || created}
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

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                <IconButton type='submit' aria-label="create" disabled={!isValid || creating || created}>
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
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
                        disabled={!isValid || creating || created}
                        startIcon={<SaveIcon />}>
                        {t('Create')}
                    </Button>
                    <IconButton aria-label="close" onClick={() => { doneAction() }}>
                        <ChevronLeftIcon />
                    </IconButton>
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={
                    <>
                        {crudViewContainer === CrudViewContainers.Inline && (renderButtonGroupWhenInline())}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && (renderButtonGroupWhenStandaloneView())}
                    </>
                }
                title={t("Create_New")}
                subheader={t("ErrorLog")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Controller
                    name="errorTime"
                    defaultValue={item.errorTime}
                    control={control}
                    {...register("errorTime", errorLogFormValidationWhenCreate.errorTime)}
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
                    {...register("userName", errorLogFormValidationWhenCreate.userName)}
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
                    {...register("errorNumber", errorLogFormValidationWhenCreate.errorNumber)}
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
                    {...register("errorSeverity", errorLogFormValidationWhenCreate.errorSeverity)}
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
                    {...register("errorState", errorLogFormValidationWhenCreate.errorState)}
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
                    {...register("errorProcedure", errorLogFormValidationWhenCreate.errorProcedure)}
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
                    {...register("errorLine", errorLogFormValidationWhenCreate.errorLine)}
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
                    {...register("errorMessage", errorLogFormValidationWhenCreate.errorMessage)}
                    autoComplete='errorMessage'
                    error={!!errors.errorMessage}
                    fullWidth
                    helperText={!!errors.errorMessage ? t(errors.errorMessage.message) : ''}
                />
            </CardContent>
            {(crudViewContainer === CrudViewContainers.Dialog) && <CardActions disableSpacing>
                {renderButtonGroupWhenDialog()}
            </CardActions>}
        </Card >
    );
}


