import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function ContactUs() {
    const { t } = useTranslation();
    
    return (
        <Typography>
            {t("ContactUs")} - To Add Content
        </Typography>
    );
}