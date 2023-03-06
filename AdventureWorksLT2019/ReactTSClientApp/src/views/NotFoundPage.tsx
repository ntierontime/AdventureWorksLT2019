import { Typography } from '@mui/material';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

export default function NotFoundPage() {
    const { t } = useTranslation();

    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE_") + " " + t("Error404");
    // }, []);

    return (
        <>
            <Typography component="h1" variant="h1">
                {t("Error404")}
            </Typography>
            <Link to="/">
                <Typography component="h1" variant="h1">
                    {t("Home")}
                </Typography>
            </Link>
        </>
    );
}
