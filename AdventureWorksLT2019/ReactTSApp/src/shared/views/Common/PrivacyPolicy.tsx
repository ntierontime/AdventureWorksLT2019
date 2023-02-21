import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function PrivacyPolicy() {
    const { t } = useTranslation();
            
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE") + " " + t("PrivacyPolicy");
    // }, []);

    return (
        <Typography>
            {t("PrivacyPolicy")} - To Add Content
        </Typography>
    );
}