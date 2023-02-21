import { useEffect } from "react";
import { useTranslation } from "react-i18next";

export default function Home() {
    const { t } = useTranslation();
    
    // if you want to change page title <html><head><title>...</title></head></html>
    useEffect(() => {
        document.title = t("_APPLICATION_TITLE_");
    }, []);

    return (
        <h1>{t("Home")}</h1>
    );
}