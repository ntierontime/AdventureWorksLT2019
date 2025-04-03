import React, { useEffect } from 'react'
import { LinearProgress } from '@mui/material';
import { useDispatch } from 'react-redux'
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import "src/i18n"

import { AppDispatch } from 'src/store/Store';
//import { confirmEmail } from 'src/slices/msIdentityFrameworkSlice';
import { msIdentityFrameworkApi } from "src/apiClients/MSIdentityFrameworkApi";
import { ApiErrorMessage } from 'src/shared/dataModels/ApiErrorMessage';

export default function ConfirmEmail(): JSX.Element {
    const navigate = useNavigate();
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();
    
    const [searchParams] = useSearchParams();
    const userId = searchParams.get('userId');
    const code = searchParams.get('code');
    const changedEmail = searchParams.get('changedEmail');

    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("_APPLICATION_TITLE_") + " " + t("Activating");
        msIdentityFrameworkApi.confirmEmail({userId, code, changedEmail}).then((res) => navigate("/login")).catch((err) =>  navigate("/"));
    }, []);

    return (<LinearProgress />)
}
