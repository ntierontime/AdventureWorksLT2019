import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';




// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ICustomerDataModel, customerFormValidationWhenCreate } from 'src/dataModels/ICustomerDataModel';
import { post } from 'src/slices/CustomerSlice';

export default function CreatePartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const { t } = useTranslation();
    // #region 1.start redux-hook-form related
    const { item } = props;
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer } = props;
    // 'control' is only used by boolean fields, you can remove it if this form doesn't have it
    // 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const { register, control, setValue, handleSubmit, reset, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    });
    // #endregion 1. redux-hook-form related

    // #region 2. CodeLists if any
	
    const [modifiedDate, setModifiedDate] = useState<string>();



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

    const onSubmit = (data: ICustomerDataModel) => {
        if(crudViewContainer === CrudViewContainers.Wizard) {
            onWizardStepSubmit(data);
            return;
        }

        setCreating(true);
        dispatch(post({ ...data }))
            .then((result) => {
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
	// #endregion 3. crudViewContainer !== CrudViewContainers.Wizard
	
    // #region 4. crudViewContainer === CrudViewContainers.Wizard
    const { wizardOrientation, onWizardStepSubmit, renderWizardButtonGroup, isFirstStep, isLastStep, isStepOptional } = props;

    // #endregion 4. crudViewContainer === CrudViewContainers.Wizard

    useEffect(() => {

        setCreating(false);
        setCreated(false);
        setCreateMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={t("Create_New")}
                subheader={t("Customer")}
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
                            <FormControlLabel
                                control={
                                    <Controller
                                        control={control}
                                        name="nameStyle"
                                        defaultValue={item.nameStyle}
                                        {...register("nameStyle", customerFormValidationWhenCreate.nameStyle)}
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
                                {...register("title", customerFormValidationWhenCreate.title)}
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
                                {...register("firstName", customerFormValidationWhenCreate.firstName)}
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
                                {...register("middleName", customerFormValidationWhenCreate.middleName)}
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
                                {...register("lastName", customerFormValidationWhenCreate.lastName)}
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
                                {...register("suffix", customerFormValidationWhenCreate.suffix)}
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
                                {...register("companyName", customerFormValidationWhenCreate.companyName)}
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
                                {...register("salesPerson", customerFormValidationWhenCreate.salesPerson)}
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
                                {...register("emailAddress", customerFormValidationWhenCreate.emailAddress)}
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
                                {...register("phone", customerFormValidationWhenCreate.phone)}
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
                                {...register("passwordHash", customerFormValidationWhenCreate.passwordHash)}
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
                                {...register("passwordSalt", customerFormValidationWhenCreate.passwordSalt)}
                                autoComplete='passwordSalt'
                                error={!!errors.passwordSalt}
                                fullWidth
                                helperText={!!errors.passwordSalt ? t(errors.passwordSalt.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                value={modifiedDate}
                                label={t('ModifiedDate')}
                                onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                                renderInput={(params) =>
                                    <TextField
                            			sx={{marginTop: 2}}
                                        fullWidth
                                        autoComplete='modifiedDate'
                            			{...register("modifiedDate", customerFormValidationWhenCreate.modifiedDate)}
                                        error={!!errors.modifiedDate}
                                        helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                                        {...params}
                                    />}
                            />
                        </Grid>
                    </Grid>
                </Box>
            </CardContent>
            {crudViewContainer != CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
            {crudViewContainer === CrudViewContainers.Wizard && <CardActions disableSpacing>
                {renderWizardButtonGroup(isFirstStep, isLastStep, isStepOptional, ()=>!isValid || creating || created || !isDirty)}
            </CardActions>}
        </Card >
    );
}

