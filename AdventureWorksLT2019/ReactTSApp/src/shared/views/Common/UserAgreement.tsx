import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function UserAgreement() {
    const { t } = useTranslation();
                
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE") + " " + t("UserAgreement");
    // }, []);

    return (
        <Typography>
            {t("UserAgreement")} - To Add Content
        </Typography>
    );
}