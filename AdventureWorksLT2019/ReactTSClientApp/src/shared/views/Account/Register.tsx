import React, { useEffect, useState } from 'react'
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Checkbox, Container, FilledInput, FormControl, FormControlLabel, FormHelperText, IconButton, InputAdornment, InputLabel, LinearProgress, Link, Typography } from '@mui/material';
import { AccountCircle, Visibility, VisibilityOff } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useTranslation } from 'react-i18next';

import "src/i18n"

import { RootState } from 'src/store/CombinedReducers';
import { register as registerNewUser } from 'src/slices/authenticationSlice';
import { RegisterViewModel } from 'src/shared/viewModels/RegisterViewModel';
import { AppDispatch } from 'src/store/Store';
import { Stack } from '@mui/system';

export default function RegisterPage(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();
    const auth = useSelector((state: RootState) => state.auth);

    const [eulaClickedAndRead, setEulaClickedAndRead] = useState(false)

    const formValidations = Yup.object().shape({
        email: Yup.string()
            .required(t('EmailRequired'))
            .email(t('EmailFormatError')),
        password: Yup.string()
            .required(t('PasswordRequired'))
            .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/, t('PasswordPatternError')),
        confirmPassword: Yup.string()
            .required(t('ConfirmPasswordRequired'))
            .oneOf([Yup.ref('password'), null], 'ConfirmPasswordNotMatchError'),
        confirmEulaRead: Yup.boolean()
            .oneOf([true], 'ConfirmEulaReadError'),
    });

    const { register, reset, getValues, setValue, handleSubmit, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            email: '',
            password: '',
            confirmPassword: '',
            confirmEulaRead: false,
        },
        resolver: yupResolver(formValidations)
    });

    const onSubmit = (data: RegisterViewModel) => {
        dispatch(registerNewUser(data));
        reset();
    }

    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    const [showConfirmPassword, setShowConfirmPassword] = React.useState(false);

    const handleClickShowConfirmPassword = () => setShowConfirmPassword((show) => !show);

    const handleMouseDownConfirmPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE_") + " " + t("RegisterANewUser");
    // }, []);

    useEffect(() => {
        if (auth.isAuthenticated) {
            const queryParams = new URLSearchParams(window.location.search)
            const from = queryParams.get("from");
            navigate(from ?? '/');
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [auth.isAuthenticated]);

    if (!auth.isLoggingIn && !auth.isAuthenticated) {
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
                            />
                            {!!errors.email && <FormHelperText>
                                {errors.email.message}
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
                                {errors.password.message}
                            </FormHelperText>}
                        </FormControl>
                    </CardContent>

                    <CardContent>
                        <FormControl fullWidth >
                            <InputLabel htmlFor="confirmPassword">{t("ConfirmPassword")}</InputLabel>
                            <FilledInput
                                required
                                id="confirmPassword"
                                type={showConfirmPassword ? 'text' : 'password'}
                                name='confirmPassword'
                                {...register("confirmPassword")}
                                error={!!errors.confirmPassword}
                                fullWidth
                                endAdornment={
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle confirmPassword visibility"
                                            onClick={handleClickShowConfirmPassword}
                                            onMouseDown={handleMouseDownConfirmPassword}
                                            edge="end"
                                        >
                                            {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                            />
                            {!!errors.confirmPassword && <FormHelperText>
                                {errors.confirmPassword.message}
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
                                    <Checkbox disabled={!eulaClickedAndRead}/>
                                }
                            />
                            <Link variant='caption' type="button" component="button" onClick={() => setEulaClickedAndRead(true)}>
                                {t('ReadEula')}
                            </Link>
                        </Stack>
                    </CardActions>
                    <CardActions disableSpacing>
                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!isValid}>
                            {t('Register')}
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
    else {
        return (<LinearProgress />)
    }
}
