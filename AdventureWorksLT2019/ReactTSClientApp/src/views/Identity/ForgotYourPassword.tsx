import React, { useEffect, useState } from 'react'
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, Checkbox, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FilledInput, FormControl, FormControlLabel, FormHelperText, IconButton, InputAdornment, InputLabel, LinearProgress, Link, TextField, Typography } from '@mui/material';
import { AccountCircle, Visibility, VisibilityOff } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import "src/i18n"
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';

import { AppDispatch } from 'src/store/Store';
import { msIdentityFrameworkApi } from 'src/apiClients/MSIdentityFrameworkApi';
import { ApiErrorMessage } from 'src/shared/dataModels/ApiErrorMessage';
import { setEmail } from 'src/slices/msIdentityFrameworkSlice';

const formValidations = Yup.object().shape({
    email: Yup.string()
        .required('EmailRequired')
        .email('EmailFormatError'),
});

export default function ForgotYourPassword(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const [submitting, setSubmitting] = useState(false)
    const [waitingForEmailConfirmation, setWaitingForEmailConfirmation] = useState(false)
    const [apiErrorMessage, setApiErrorMessage] = useState<ApiErrorMessage>(null)

    // 1. Send ResetPasswordEmail Button
    const { register, setValue, handleSubmit, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: {
            email: '',
        },
        resolver: yupResolver(formValidations)
    });

    const onSubmit = (data: { email: string }) => {
        //console.log(data);
        setSubmitting(true);
        msIdentityFrameworkApi.forgotPassword(data)
            .then(res => {
                // set email to msIdentityFrameworkSlice, used in ResetPassword.tsx
                dispatch(setEmail(data.email));
                setWaitingForEmailConfirmation(true);
            })
            .catch((err: any) => {
                setApiErrorMessage(err.response.data as unknown as ApiErrorMessage);
                // console.log(err);
            })
            .finally(() => setSubmitting(false));
    }

    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE_") + " " + t("LogIn");
    // }, []);

    return (
        <Container component='main' maxWidth='xs'>
            <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
                <CardHeader
                    title={
                        <Typography component='h1' variant='h4'>
                            {t("ForgetYourPassword")}
                        </Typography>
                    }
                    subheader={t("Please enter the email address to send a ResetPassword link.")}
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
                {waitingForEmailConfirmation && <CardContent>
                    <Typography>{t('Please check your email, and click the reset password link')}</Typography>
                </CardContent>}
                <CardActions disableSpacing>
                    <Button variant='contained'>{t("Cancel")}</Button>
                    <Button type="submit" variant='contained'>{t(waitingForEmailConfirmation ? "Resend" : "Send")}</Button>
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
                {/* <CardActions disableSpacing >
                        <GoogleLogin onSuccess={responseMessage} onError={errorMessage}  />
                    </CardActions> */}
            </Card >
        </Container>
    );
}

