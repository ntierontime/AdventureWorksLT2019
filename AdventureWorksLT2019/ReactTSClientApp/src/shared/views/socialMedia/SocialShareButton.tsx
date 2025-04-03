import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    FormControlLabel,
    IconButton,
    Switch,
} from "@mui/material";
import ShareIcon from "@mui/icons-material/Share";
import { useTranslation } from "react-i18next";
import { useState } from "react";
import SocialShare from "./SocialShare";
import { SocialShareProps } from "./SocialShareProps";
import { ButtonTypes } from "../buttonGroups/ButtonTypes";

export default function SocialShareButton(props: SocialShareProps): JSX.Element {
    const { buttonType = ButtonTypes.Icon } = props;
    const { t } = useTranslation();
    const [open, setOpen] = useState(false);
    const [stayOpen, setStayOpen] = useState(false);
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <>
            {buttonType === ButtonTypes.Text && <Button
                autoFocus
                variant="outlined"
                onClick={() => {
                    handleClickOpen();
                }}
            >
                {t("Share")}
            </Button>}

            {buttonType === ButtonTypes.IconAndText && <Button
                autoFocus
                variant="text"
                startIcon={<ShareIcon />}
                onClick={() => {
                    handleClickOpen();
                }}
            >
                {t("Share")}
            </Button>}
            {buttonType === ButtonTypes.Icon && <IconButton
                aria-label="Share"
                color="primary"
                onClick={() => {
                    handleClickOpen();
                }}
            >
                <ShareIcon />
            </IconButton>}
            {buttonType === ButtonTypes.MenuItem && <IconButton
                aria-label="Share"
                color="primary"
                onClick={() => {
                    handleClickOpen();
                }}
            >
                <ShareIcon />
            </IconButton>}
            <Dialog
                sx={{ "& .MuiDialog-paper": { width: "80%", maxHeight: 435 } }}
                maxWidth="xs"
                onClose={() => {
                    if (!stayOpen) handleClose();
                }}
                open={open}
            >
                <DialogContent dividers>
                    <SocialShare
                        {...props}
                    />
                </DialogContent>
                <DialogActions>
                    <FormControlLabel
                        control={
                            <Switch
                                checked={stayOpen}
                                onChange={(e) => {
                                    setStayOpen(e.target.checked);
                                }}
                                name="gilad"
                            />
                        }
                        label={t("StayOpen")}
                    />
                    <Button autoFocus onClick={handleClose}>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
