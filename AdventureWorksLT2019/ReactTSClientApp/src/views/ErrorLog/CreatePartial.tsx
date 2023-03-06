import { useEffect, useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';
import { DatePicker } from '@mui/x-date-pickers';

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
import { IErrorLogDataModel, errorLogFormValidationWhenCreate } from 'src/dataModels/IErrorLogDataModel';
import { post } from 'src/slices/ErrorLogSlice';

export default function CreatePartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
    const { t } = useTranslation();
    // #region 1.start redux-hook-form related
    const { item } = props;
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props;
    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(errorLogFormValidationWhenCreate)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;
    // #endregion 1. redux-hook-form related

    // #region 2. CodeLists if any
	
    const [errorTime, setErrorTime] = useState<string>();






    // #endregion 2. CodeLists if any

    // #region 3. crudViewContainer !== CrudViewContainers.Wizard
    const { doneAction } = props; // dialog
    const dispatch = useDispatch<AppDispatch>();

    const [creating, setCreating] = useState(false);
    const [created, setCreated] = useState(false);

    const [createMessage, setCreateMessage] = useState<string>();
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };

    const onSubmit = (data: IErrorLogDataModel) => {
        if(crudViewContainer === CrudViewContainers.Wizard) {
            onWizardStepSubmit(data);
            return;
        }

        setCreating(true);
        dispatch(post({ ...data }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setCreating(false);
                        setCreated(false);
                        setCreateMessage(null);
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
            .catch((error: any) => { setCreateMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setCreating(false); console.log('finally'); });
    }


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
                <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
                <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || creating || created}>
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
	// #endregion 3. crudViewContainer !== CrudViewContainers.Wizard
	
    // #region 4. crudViewContainer === CrudViewContainers.Wizard
    const { wizardOrientation, onWizardStepSubmit, renderWizardButtonGroup, isFirstStep, isLastStep, isStepOptional } = props;
	const submitRef = useRef(); // used for external trigger submit event.
    // #endregion 4. crudViewContainer === CrudViewContainers.Wizard

    useEffect(() => {

        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [item]);
	
	useEffect(() => {
        // console.log("trigger validation");
        trigger();
    }, [trigger]);

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)} sx={{height: '100%', display: "flex", flexDirection: "column",}}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("ErrorLog")}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && !!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
                            <Controller
                                name="errorTime"
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={errorTime}
                            				label={t('ErrorTime')}
                            				onChange={(event: string) => { setErrorTime(event); setValue('errorTime', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.errorTime}
                            						helperText={!!errors.errorTime ? t(errors.errorTime.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                    </Grid>
                </Box>
            </CardContent>
            {crudViewContainer != CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
            {crudViewContainer === CrudViewContainers.Wizard && <CardActions disableSpacing sx={{ mt: "auto" }}>
                <button ref={submitRef} type="submit" style={{ display: 'none' }} />{renderWizardButtonGroup(isFirstStep, isLastStep, isStepOptional, ()=>!isValid || creating || created, submitRef)}
            </CardActions>}
        </Card >
    );
}

