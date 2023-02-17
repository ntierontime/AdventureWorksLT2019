import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function PrivacyPolicy() {
    const { t } = useTranslation();
    
    return (
        <Typography>
            {t("PrivacyPolicy")} - To Add Content
        </Typography>
    );
}