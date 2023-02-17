import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function AboutUs() {
    const { t } = useTranslation();
    
    return (
        <Typography>
            {t("AboutUs")} - To Add Content
        </Typography>
    );
}