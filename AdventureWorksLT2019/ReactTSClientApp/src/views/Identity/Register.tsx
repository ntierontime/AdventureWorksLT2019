import React, { useEffect, useState } from 'react'
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Checkbox, Container, FilledInput, FormControl, FormControlLabel, FormHelperText, Grid, IconButton, InputAdornment, InputLabel, LinearProgress, Link, TextField, Typography } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers';
import { AccountCircle, Visibility, VisibilityOff } from '@mui/icons-material';
import { Stack } from '@mui/system';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate, useParams, useSearchParams } from 'react-router-dom';
import { Controller, useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useTranslation } from 'react-i18next';

import "src/i18n"
import { useGoogleLogin } from '@react-oauth/google';

import { AppDispatch } from 'src/store/Store';
import { msIdentityFrameworkApi } from "src/apiClients/MSIdentityFrameworkApi";
import { ApiErrorMessage } from 'src/shared/dataModels/ApiErrorMessage';
import { googleCallBack } from 'src/slices/msIdentityFrameworkSlice';

const formValidations = Yup.object().shape({
    email: Yup.string()
        .required('EmailRequired')
        .email('EmailFormatError'),
    password: Yup.string()
        .required('PasswordRequired')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/, 'PasswordPatternError'),
    confirmEulaRead: Yup.boolean()
        .oneOf([true], 'ConfirmEulaReadError'),
});

export default function RegisterPage(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    // 1. email registration
    const [submitting, setSubmitting] = useState(false)
    const [waitingForEmailConfirmation, setWaitingForEmailConfirmation] = useState(false)
    const [apiErrorMessage, setApiErrorMessage] = useState<ApiErrorMessage>(null)

    const [eulaClickedAndRead, setEulaClickedAndRead] = useState(false)
    const [showPassword, setShowPassword] = React.useState(false);
    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    const { register, control, reset, getValues, setValue, handleSubmit, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            // email: '',
            // password: '',
            // confirmEulaRead: false,

            email: 'david4chao@gmail.com',
            password: 'Gzha0!23',
            confirmEulaRead: true,
        },
        resolver: yupResolver(formValidations)
    });

    const onSubmit = (data: any) => {
        setSubmitting(true);
        setApiErrorMessage(null);

        const { email, password } = data;
        const dataToInsert = {
            email, password,
        };

        msIdentityFrameworkApi.register(dataToInsert)
            .then(res => {
                setWaitingForEmailConfirmation(true);
            })
            .catch((err: any) => {
                setApiErrorMessage(err.response.data as unknown as ApiErrorMessage);
                console.log(err);
            })
            .finally(() => setSubmitting(false));

        reset();
    }

    const onResendConfirmEmail = (data: any) => {
        setSubmitting(true);
        setApiErrorMessage(null);

        const { email, password } = data;
        const dataToInsert = {
            email, password,
        };

        msIdentityFrameworkApi.resendConfirmationEmail(dataToInsert)
            .then(res => {
                setWaitingForEmailConfirmation(true);
            })
            .catch((err: any) => {
                setApiErrorMessage(err.response.data as unknown as ApiErrorMessage);
                console.log(err);
            })
            .finally(() => setSubmitting(false));
        ;
        reset();
    }

    // 2. Google Registration
    const onLoginAsGoogleClicked = useGoogleLogin({
        onSuccess: (response) => {
            console.log(response)
            dispatch(googleCallBack(response));
        },
        onError: (error) => console.log('Login Failed:', error)
    });
    
    
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE_") + " " + t("RegisterANewUser");
    // }, []);

    if (!waitingForEmailConfirmation) {
        return (
            <Container component='main' maxWidth='xs'>
                <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
                    <CardHeader
                        title={
                            <Typography component='h5' variant='h5'>
                                {t('RegisterANewUser')}
                            </Typography>
                        }
                        avatar={
                            <Avatar>
                                <AccountCircle style={{ fontSize: 45 }} />
                            </Avatar>
                        }
                    />
                    <CardContent>
                        <FormControl fullWidth >
                            <InputLabel htmlFor="email">{t("Email")}</InputLabel>
                            <FilledInput
                                required
                                id="email"
                                type='email'
                                {...register("email")}
                                autoComplete='email'
                                error={!!errors.email}
                                fullWidth
                                autoFocus
                                disabled={submitting}
                            />
                            {!!errors.email && <FormHelperText>
                                {t(errors.email.message)}
                            </FormHelperText>}
                        </FormControl>
                    </CardContent>

                    <CardContent>
                        <FormControl fullWidth >
                            <InputLabel htmlFor="password">{t("Password")}</InputLabel>
                            <FilledInput
                                required
                                id="password"
                                type={showPassword ? 'text' : 'password'}
                                name='password'
                                {...register("password")}
                                error={!!errors.password}
                                fullWidth
                                disabled={submitting}
                                endAdornment={
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            onMouseDown={handleMouseDownPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                            />
                            {!!errors.password && <FormHelperText>
                                {t(errors.password.message)}
                            </FormHelperText>}
                        </FormControl>
                    </CardContent>
                    <CardActions disableSpacing>
                        <Stack direction="row">
                            <FormControlLabel
                                disabled={!eulaClickedAndRead}
                                label={
                                    <Typography component='span' variant='caption'>

                                    </Typography>
                                }
                                name='confirmEulaRead'
                                {...register("confirmEulaRead")}
                                control={
                                    <Checkbox disabled={!eulaClickedAndRead || submitting} />
                                }
                            />
                            <Link variant='caption' type="button" component="button" onClick={() => setEulaClickedAndRead(true)}>
                                {t('ReadEula')}
                            </Link>
                        </Stack>
                    </CardActions>
                    {!!apiErrorMessage && <CardContent>
                        <Typography>{apiErrorMessage.title}</Typography>
                        {!!apiErrorMessage.errors && Object.keys(apiErrorMessage.errors).map(oneError => {
                            return (<Typography key={oneError}>{t(oneError)}-{apiErrorMessage.errors[oneError]}</Typography>);
                        })}
                    </CardContent>}

                    <CardActions disableSpacing>
                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!isValid || submitting}>
                            {t('Register')}
                        </Button>
                    </CardActions>
                    <CardActions disableSpacing>
                        <Button
                            color="secondary"
                            onClick={() => onLoginAsGoogleClicked()}
                            fullWidth
                            variant='outlined'>
                            {t('Login As Google')}
                        </Button>
                    </CardActions>
                    <CardActions disableSpacing>
                        <Button
                            href='/login'
                            color="secondary"
                            fullWidth
                            variant='outlined'>
                            {t('Login')}
                        </Button>
                    </CardActions>
                </Card >
            </Container>
        );
    }
    else if (!submitting && waitingForEmailConfirmation) {
        return (
            <Container component='main' maxWidth='xs'>
                <Card component="form" noValidate onSubmit={handleSubmit(onResendConfirmEmail)}>
                    <CardHeader
                        title={
                            <Typography component='h5' variant='h5'>
                                {t('EmailActivationTitle')}
                            </Typography>
                        }
                        avatar={
                            <Avatar>
                                <AccountCircle style={{ fontSize: 45 }} />
                            </Avatar>
                        }
                    />
                    <CardContent>
                        <FormControl fullWidth >
                            <InputLabel htmlFor="email">{t("Email")}</InputLabel>
                            <FilledInput
                                required
                                id="email"
                                type='email'
                                {...register("email")}
                                autoComplete='email'
                                error={!!errors.email}
                                fullWidth
                                autoFocus
                                disabled={true}
                            />
                            {!!errors.email && <FormHelperText>
                                {t(errors.email.message)}
                            </FormHelperText>}
                        </FormControl>
                    </CardContent>
                    <CardContent>
                        <Typography>{t('EmailActivationMessage')}</Typography>
                    </CardContent>
                    <CardActions disableSpacing>
                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!isValid}>
                            {t('ResendActivationEmail')}
                        </Button>
                    </CardActions>
                </Card >
            </Container>
        );
    }
    else {
        return (<LinearProgress />)
    }
}
