import React from "react";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, Typography } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router";

export interface SimpleConfirmationDialogProps {
    title?: string;
    description?: string;
    id: string;
    keepMounted: boolean;
    open: boolean;
    onConfirmed: () => void;
    onCancel: () => void;
}

export function SimpleConfirmationDialog(props: SimpleConfirmationDialogProps) {
    const { t } = useTranslation();
    const {
        title = 'WarningLosingData', 
        description = 'WarningLosingDataDescription', 
        onConfirmed, onCancel, open, 
        ...other } = props;

    const handleCancel = () => {
        onCancel();
    };

    const handleConfirm = () => {
        onConfirmed();
    };

    return (
        <Dialog
            sx={{ '& .MuiDialog-paper': { width: '80%', maxHeight: 435 } }}
            maxWidth="xs"
            open={open}
            {...other}
            
        >
            <CloseIcon />
            <DialogTitle>{t(title)}</DialogTitle>
            <DialogContent dividers>
                <Typography>{t(description)}</Typography>
            </DialogContent>
            <DialogActions disableSpacing sx={{ display: 'flex', flexDirection: 'row', pt: 2, mt: "auto" }}>
                <Button autoFocus onClick={handleCancel}>{t('Cancel')}</Button>
                <Box sx={{ flex: '1 1 auto' }}></Box>
                <Button onClick={handleConfirm}>{t('Confirm')}</Button>
            </DialogActions>
        </Dialog>
    );
}