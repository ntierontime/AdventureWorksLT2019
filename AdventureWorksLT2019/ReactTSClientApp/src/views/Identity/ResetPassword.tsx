import React, { useEffect, useState } from 'react'
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Checkbox, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FilledInput, FormControl, FormControlLabel, FormHelperText, IconButton, InputAdornment, InputLabel, LinearProgress, Link, TextField, Typography } from '@mui/material';
import { Stack } from '@mui/system';
import { AccountCircle, Visibility, VisibilityOff } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import "src/i18n"

import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useTranslation } from 'react-i18next';

import "src/i18n"

import { msIdentityFrameworkApi } from "src/apiClients/MSIdentityFrameworkApi";
import { ApiErrorMessage } from 'src/shared/dataModels/ApiErrorMessage';

import { RootState } from 'src/store/CombinedReducers';

const formValidations = Yup.object().shape({
    password: Yup.string()
        .required('PasswordRequired')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/, 'PasswordPatternError'),
    confirmPassword: Yup.string()
        .required('ConfirmPasswordRequired')
        .oneOf([Yup.ref('password'), null], 'ConfirmPasswordNotMatchError'),
});

export default function ResetPassword(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const auth = useSelector((state: RootState) => state.auth);
    const { email } = auth;

    const [searchParams] = useSearchParams();
    const resetCode = searchParams.get('resetCode');

    const [submitting, setSubmitting] = useState(false)
    const [apiErrorMessage, setApiErrorMessage] = useState<ApiErrorMessage>(null)

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

    // console.log(email, resetCode);
    
    // 1. LogIn Button
    const { register, setValue, handleSubmit, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            password: '',
            confirmPassword: '',
        },
        resolver: yupResolver(formValidations)
    });

    const onSubmit = (data: {password: string}) => {
        // console.log(data);
        setSubmitting(true);
        setApiErrorMessage(null);

        const { password } = data;
        const dataToInsert = {
            email, newPassword: password, resetCode
        };

        msIdentityFrameworkApi.resetPassword(dataToInsert)
            .then(res => {
                navigate('/login')
            })
            .catch((err: any) => {
                setApiErrorMessage(err.response.data as unknown as ApiErrorMessage);
                // console.log(err);
            })
            .finally(() => setSubmitting(false));        
    }
  
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("_APPLICATION_TITLE_") + " " + t("LogIn");
    }, []);

        return (
            <Container component='main' maxWidth='xs'>
                <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
                    <CardHeader
                        title={
                            <Typography component='h1' variant='h4'>
                                {t("ResetPassword")}
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
                                {t(errors.confirmPassword.message)}
                            </FormHelperText>}
                        </FormControl>
                    </CardContent>
                    <CardActions disableSpacing>
                        <Button variant='contained'>{t("Cancel")}</Button>
                        <Button type="submit" variant='contained'>{t("ChangePassword")}</Button>
                    </CardActions>
                </Card >
            </Container>
        );
}

