import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, IconButton, TextField, Typography } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';


import { AppDispatch } from 'src/store/Store';

import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { defaultCustomer, ICustomerDataModel, customerFormValidationWhenCreate } from 'src/dataModels/ICustomerDataModel';
import { post } from 'src/slices/CustomerSlice';

export default function CreatePartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const { crudViewContainer } = props; // item
    const { doneAction } = props; // dialog
    const [item, setItem] = useState<ICustomerDataModel>(defaultCustomer());
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, handleSubmit, reset, formState: { isValid, errors } } = useForm({
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
                        setItem(defaultCustomer());
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

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            <CardHeader
                action={
                    (crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.StandaloneView) && <>
                        <IconButton type='submit' aria-label="create" disabled={!isValid || creating || created}>
                            <SaveIcon />
                        </IconButton>
                        <IconButton aria-label="close" onClick={() => { doneAction() }}>
                            <CloseIcon />
                        </IconButton>
                    </>
                }
                title={t("Create_New")}
                subheader={t("Customer")}
            />
            {!!createMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {createMessage + " "}
                </Typography>
            </CardContent>}
            <CardContent>
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
                    autoFocus
                    helperText={!!errors.title ? t(errors.title.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.firstName ? t(errors.firstName.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.middleName ? t(errors.middleName.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.lastName ? t(errors.lastName.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.suffix ? t(errors.suffix.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.companyName ? t(errors.companyName.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.salesPerson ? t(errors.salesPerson.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.emailAddress ? t(errors.emailAddress.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.phone ? t(errors.phone.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.passwordHash ? t(errors.passwordHash.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.passwordSalt ? t(errors.passwordSalt.message) : ''}
                />
                <Controller
                    name="modifiedDate"
                    defaultValue={item.modifiedDate}
                    control={control}
                    {...register("modifiedDate", customerFormValidationWhenCreate.modifiedDate)}
                    render={
                        ({ field: { onChange, ...restField } }) =>
                            <DatePicker
                				ref={null}
                                label={t('ModifiedDate')}
                                autoFocus
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
            {(crudViewContainer === CrudViewContainers.Dialog) && <CardActions disableSpacing>
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
            </CardActions>}
        </Card >
    );
}


