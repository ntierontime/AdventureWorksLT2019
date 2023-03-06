import React, { useEffect } from 'react'
import { Avatar, Button, Checkbox, Container, CssBaseline, FormControlLabel, Grid, LinearProgress, Link, Paper, TextField, Typography } from '@mui/material';
import { AccountCircle } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { RootState } from 'src/store/CombinedReducers';
import { login } from 'src/slices/authenticationSlice';
import { LoginViewModel } from 'src/shared/viewModels/LoginViewModel';
import { AppDispatch } from 'src/store/Store';

export default function LoginPage(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();
    const auth = useSelector((state: RootState) => state.auth);

    const { register, setValue, handleSubmit, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            email: '',
            password: '',
            rememberMe: true,
            from: ''
        },
    });

    const onSubmit = (data: LoginViewModel) => {
        dispatch(login(data));
        setValue('password', '');
    }

    const formValidations = {
        email: {
            required: t('EmailRequired'),
            pattern: {
                value: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                message: t('EmailFormatError'),
            }
        },
        password: {
            required: t('PasswordRequired'),
            pattern: {
                value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                message: t('PasswordPatternError'),
            }
        }
    };

    useEffect(() => {
        if (auth.isAuthenticated) {
            const queryParams = new URLSearchParams(window.location.search)
            const from = queryParams.get("from");
            navigate(from);
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [auth]);

    if (!auth.isLoggingIn && !auth.isAuthenticated) {
        return (
            <Container component='main' maxWidth='xs'>
                <CssBaseline />
                <Paper>
                    <Avatar>
                        <AccountCircle style={{ fontSize: 45 }} />
                    </Avatar>
                    <Typography component='h1' variant='h4'>
                        {t('LogIn')}
                    </Typography>
                    <form
                        noValidate
                        onSubmit={handleSubmit(onSubmit)}
                    >
                        <TextField
                            name='email'
                            label={t('Email')}
                            variant='outlined'
                            margin='normal'
                            {...register("email", formValidations.email)}
                            autoComplete='email'
                            error={!!errors.email}
                            fullWidth
                            autoFocus
                        />
                        {errors.email && (
                            <span>{errors.email.message}</span>
                        )}
                        <TextField
                            name='password'
                            label={t('Password')}
                            type='password'
                            variant='outlined'
                            margin='normal'
                            {...register("password", formValidations.password)}
                            error={!!errors.password}
                            fullWidth
                            autoComplete='current-password'
                        />
                        {errors.password && (
                            <span>{errors.password.message}</span>
                        )}

                        <Grid container>
                            <Grid item xs>
                                <Link href='#' variant='body2'>
                                    {t('UIStringResource:Account_PasswordRecovery_TitleText')}
                                </Link>
                            </Grid>
                        </Grid>
                        <Grid container>
                            <Grid item>
                                <FormControlLabel
                                    label={t('RememberMe')}
                                    name='rememberMe'
                                    control={
                                        <Checkbox />
                                    }
                                />
                            </Grid>
                        </Grid>

                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!isValid}>
                            {t('LogIn')}
                        </Button>
                        <Grid container>
                            <Grid item>
                                <Link href='#' variant='body2'>
                                    {t('Register')}
                                </Link>
                            </Grid>
                        </Grid>
                    </form>
                </Paper>
            </Container>
        );
    }
    else {
        return (<LinearProgress />)
    }
}
