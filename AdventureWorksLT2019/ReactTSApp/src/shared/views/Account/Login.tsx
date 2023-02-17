import React, { useEffect } from 'react'
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Checkbox, Container, FilledInput, FormControl, FormControlLabel, FormHelperText, IconButton, InputAdornment, InputLabel, LinearProgress, Link, Typography } from '@mui/material';
import { AccountCircle, Visibility, VisibilityOff } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { RootState } from 'src/store/CombinedReducers';
import { login } from 'src/shared/slices/authenticationSlice';
import { LoginViewModel } from 'src/shared/viewModels/LoginViewModel';
import { AppDispatch } from 'src/store/Store';
import { Stack } from '@mui/system';

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

    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

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
                            <Typography component='h1' variant='h4'>
                                {t('LogIn')}
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
                            <InputLabel htmlFor="email">Email</InputLabel>
                            <FilledInput
                                id="email"
                                type='email'
                                {...register("email", formValidations.email)}
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
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <FilledInput
                                id="password"
                                type={showPassword ? 'text' : 'password'}
                                name='password'
                                {...register("password", formValidations.password)}
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
                    <CardActions disableSpacing>
                        <Stack direction='row' justifyContent="space-between" alignItems="center" spacing={2} sx={{ width: '100%' }} >
                            <FormControlLabel
                                label={
                                    <Typography component='span' variant='caption'>
                                        {t('RememberMe')}
                                    </Typography>
                                }
                                name='rememberMe'
                                control={
                                    <Checkbox />
                                }
                            />
                            <Link href='#' variant='caption'>
                                {t('ForgotYourPassword')}
                            </Link>
                        </Stack>
                    </CardActions>
                    <CardActions disableSpacing>
                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            disabled={!isValid}>
                            {t('LogIn')}
                        </Button>
                    </CardActions>
                    <CardActions disableSpacing>
                        <Button
                            color="secondary"
                            href='/register'
                            fullWidth
                            variant='outlined'>
                            {t('Register')}
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
