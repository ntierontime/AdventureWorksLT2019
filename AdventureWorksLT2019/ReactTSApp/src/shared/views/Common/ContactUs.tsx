import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function ContactUs() {
    const { t } = useTranslation();
        
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE") + " " + t("ContactUs");
    // }, []);

    return (
        <Typography>
            {t("ContactUs")} - To Add Content
        </Typography>
    );
}