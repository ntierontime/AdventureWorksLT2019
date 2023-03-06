import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function AboutUs() {
    const { t } = useTranslation();
    
    // // if you want to change page title <html><head><title>...</title></head></html>
    // useEffect(() => {
    //     document.title = t("_APPLICATION_TITLE_") + " " + t("AboutUs");
    // }, []);

    return (
        <Typography>
            {t("AboutUs")} - To Add Content
        </Typography>
    );
}