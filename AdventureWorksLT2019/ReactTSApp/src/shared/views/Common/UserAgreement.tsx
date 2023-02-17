import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";

export default function UserAgreement() {
    const { t } = useTranslation();
    
    return (
        <Typography>
            {t("UserAgreement")} - To Add Content
        </Typography>
    );
}