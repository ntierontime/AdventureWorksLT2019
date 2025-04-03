import * as React from 'react';
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";


// Register
import PersonAddIcon from '@mui/icons-material/PersonAdd';

// Cancel
import CancelIcon from '@mui/icons-material/Cancel';

// Revoke
import UndoIcon from '@mui/icons-material/Undo';

// ShoppingCart
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

// AddToCart
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';

// RemoveFromShoppngCart
import RemoveShoppingCartIcon from '@mui/icons-material/RemoveShoppingCart';

// WriteAReview
import RateReviewIcon from '@mui/icons-material/RateReview';

// AddComment
import AddCommentIcon from '@mui/icons-material/AddComment';

// Register
import PersonAddAltIcon from '@mui/icons-material/PersonAddAlt';

// Join
import StartIcon from '@mui/icons-material/Start';

import EnumWorkflowStepButtonProps, { EnumWorkflowStepButtonGroupProps } from "./EnumWorkflowStepButtonProps";
import { AppWellKnownActions } from "../AppWellKnownActions";
import { t } from 'i18next';

export function EnumWorkflowStepIconAndTextButtonGroup(
    props: EnumWorkflowStepButtonGroupProps
): JSX.Element { 
    const { currentWorkflow, identifier, item, submitting, onButtonClick, size, confirmDialogCaption: itemCaption } = props;

    return (
        <>
            {!!currentWorkflow?.nextSteps && currentWorkflow?.nextSteps.map(v => {
                // if (v.action === AppWellKnownActions.Register)
                //     return <RegisterIconAndTextButton workflowStep={v} identifier={identifier} item={item} value={v.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
                if (v.action  === AppWellKnownActions.Cancel)
                    return <CancelIconAndTextButton workflowStep={v} identifier={identifier} item={item} value={v.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
                if (v.action  === AppWellKnownActions.Revoke)
                    return <RevokeIconAndTextButton workflowStep={v} identifier={identifier} item={item} value={v.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
                return <></>;
            })}
        </>
    )
}

export default function EnumWorkflowStepIconAndTextButton(
    props: EnumWorkflowStepButtonProps
): JSX.Element { 
    const { workflowStep, identifier, item, submitting, onButtonClick, size, confirmDialogCaption: itemCaption } = props;
    const { t } = useTranslation();

    // if (workflowStep.action === AppWellKnownActions.Register)
    //     return <RegisterIconAndTextButton workflowStep={workflowStep} identifier={identifier} item={item} value={workflowStep.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
    if (workflowStep.action === AppWellKnownActions.Cancel)
        return <CancelIconAndTextButton workflowStep={workflowStep} identifier={identifier} item={item} value={workflowStep.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
    if (workflowStep.action === AppWellKnownActions.Revoke)
        return <RevokeIconAndTextButton workflowStep={workflowStep} identifier={identifier} item={item} value={workflowStep.key} submitting={submitting} onButtonClick={onButtonClick} confirmDialogCaption={itemCaption} size={size} />;
    return <></>;
}


// // 1. Register
// export function RegisterIconAndTextButton(
//     props: EnumWorkflowStepButtonProps
// ): JSX.Element {
//     const { identifier, item, value, submitting, onButtonClick, confirmDialogCaption: itemCaption, note, size  } = props;
//     const { t } = useTranslation();
//     const [open, setOpen] = React.useState(false);

//     const handleClickOpen = () => {
//       setOpen(true);
//     };
  
//     const handleClose = () => {
//       setOpen(false);
//     };

//     return (
//         <React.Fragment>
//             <Button
//                 onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); handleClickOpen(); return false; }}
//                 aria-label="Register"
//                 size={size}
//                 disabled={submitting}
//                 color="primary"
//                 variant="contained"
//                 startIcon={<PersonAddIcon />}
//             >
//                 {t("Register")}
//             </Button>
//             <Dialog
//                 open={open}
//                 onClose={handleClose}
//                 aria-labelledby="alert-dialog-title"
//                 aria-describedby="alert-dialog-description"
//                 fullWidth={true}
//                 maxWidth="md"
//             >
//                 <DialogTitle id="alert-dialog-title">
//                     {t("Confirm Registration?")}
//                 </DialogTitle>
//                 <DialogContent>
//                     <DialogContentText id="alert-dialog-description">
//                         {t("Please confirm registration")} - <Typography gutterBottom variant="h6" component="div" padding={0} margin={0}>
//                             {itemCaption}
//                         </Typography>
//                     </DialogContentText>
//                 </DialogContent>
//                 <DialogActions>
//                     <Button onClick={handleClose}>{t("Close")}</Button>
//                     <Button onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); onButtonClick(identifier, item, value); handleClose(); return false;}} autoFocus>{t("Confirm")}</Button>
//                 </DialogActions>
//             </Dialog>
//         </React.Fragment>
//     );
// }

// 2. Cancel
export function CancelIconAndTextButton(
    props: EnumWorkflowStepButtonProps
): JSX.Element {
    const { identifier, item, value, submitting, onButtonClick, confirmDialogCaption: itemCaption, note, size  } = props;
    const { t } = useTranslation();
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
      setOpen(true);
    };
  
    const handleClose = () => {
      setOpen(false);
    };

    return (
        <React.Fragment>
            <Button
                onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); handleClickOpen(); return false; }}
                aria-label="Cancel"
                size={size}
                disabled={submitting}
                color="primary"
                variant="contained"
                startIcon={<CancelIcon />}
            >
                {t("Cancel")}
            </Button>
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                fullWidth={true}
                maxWidth="md"
            >
                <DialogTitle id="alert-dialog-title">
                    {t("Confirm Cancellation?")}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {t("Please confirm Cancellation")} - <Typography gutterBottom variant="h6" component="div" padding={0} margin={0}>
                            {itemCaption}
                        </Typography>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>{t("Close")}</Button>
                    <Button onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); onButtonClick(identifier, item, value); handleClose(); return false;}} autoFocus>{t("Confirm")}</Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    );
}

// 3. Revoke
export function RevokeIconAndTextButton(
    props: EnumWorkflowStepButtonProps
): JSX.Element {
    const { identifier, item, value, submitting, onButtonClick, confirmDialogCaption: itemCaption, note, size  } = props;
    const { t } = useTranslation();
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
      setOpen(true);
    };
  
    const handleClose = () => {
      setOpen(false);
    };

    return (
        <React.Fragment>
            <Button
                onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); handleClickOpen(); return false; }}
                aria-label="Revoke"
                size={size}
                disabled={submitting}
                color="primary"
                variant="contained"
                startIcon={<UndoIcon />}
            >
                {t("Revoke")}
            </Button>
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                fullWidth={true}
                maxWidth="md"
            >
                <DialogTitle id="alert-dialog-title">
                    {t("Confirm Revoke?")}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {t("Please confirm Revoke")} - <Typography gutterBottom variant="h6" component="div" padding={0} margin={0}>
                            {itemCaption}
                        </Typography>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>{t("Close")}</Button>
                    <Button onClick={(e: any) => { e.preventDefault(); e.stopPropagation(); e.nativeEvent.stopImmediatePropagation(); onButtonClick(identifier, item, value); handleClose(); return false; }} autoFocus>{t("Confirm")}</Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    );
}
