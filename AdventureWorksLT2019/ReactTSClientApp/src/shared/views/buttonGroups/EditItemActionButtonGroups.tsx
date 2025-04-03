import { Button, ButtonGroup } from "@mui/material";
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import { LoadingButton } from "@mui/lab";
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';
import { useTranslation } from "react-i18next";
import { ItemActionButtonGroupProps } from "src/shared/views/buttonGroups/ItemActionButtonGroupProps";


// 1. This ItemActionIconAndTextButtonGroups can be used in Dialog and a page, in <Card />, at <CardAction/> below <CardHeader> as ToolBar preferred
// 2. Can also used in <CardHeader action={} />
// 2.1. left side are the previous/next buttons, no text,
// 2.2. right side are the submit/done buttons, without text,
export function EditItemActionIconButtonGroup(props: ItemActionButtonGroupProps): JSX.Element {
    const { t } = useTranslation();
    const {
        previousAction, nextAction,
        previousIcon = <NavigateBeforeIcon />, nextIcon = <NavigateNextIcon />,
        //previousText = "Previous", nextText = "Next",
        //submitAction,
        doneAction,
        submitIcon = <SaveIcon color='action' />, doneIcon = <CloseIcon />,
        submitting, isValid, submitted, 
        submitRef, formId,
        extraButtonGroups
    } = props;
    return (
        <>
            {!!extraButtonGroups && (extraButtonGroups)}
            {(!!previousAction || !!nextAction) && <ButtonGroup
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                {!!previousAction && <Button
                    color="secondary"
                    disabled={submitting}
                    variant='outlined'
                    startIcon={previousIcon}
                    onClick={() => { previousAction() }}
                />}
                {!!nextAction && <Button
                    color="secondary"
                    disabled={submitting}
                    variant='outlined'
                    endIcon={nextIcon}
                    onClick={() => { nextAction() }}
                />}
            </ButtonGroup>}
            <ButtonGroup sx={{ marginLeft: 'auto', }}
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                <LoadingButton
                    form={formId}
                    color="primary"
                    type={!!!submitRef? "submit": "button"}
                    variant='contained'
                    disabled={!isValid || submitting || submitted}
                    onClick={(event) => { 
                        event.stopPropagation(); 
                        if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } 
                        if(!!doneAction) {
                            doneAction();
                        }
                    }}
                    startIcon={submitIcon}>
                </LoadingButton>
                {!!doneAction && <Button
                    color="secondary"
                    autoFocus
                    disabled={submitting}
                    variant='contained'
                    startIcon={doneIcon}
                    onClick={() => { doneAction() }}
                >
                </Button>}
            </ButtonGroup>
        </>
    );
}

// 1. This ItemActionIconAndTextButtonGroups can be used in Dialog and a page, in <Card />, at bottom <CardAction/> preferred
// 2.1. left side are the previous/next buttons, no text,
// 2.2. right side are the submit/done buttons, with text, 
export function EditItemActionIconAndTextButtonGroup(props: ItemActionButtonGroupProps): JSX.Element {
    const { t } = useTranslation();
    const {
        previousAction, nextAction,
        previousIcon = <NavigateBeforeIcon />, nextIcon = <NavigateNextIcon />,
        //previousText = "Previous", nextText = "Next",
        //submitAction,
        doneAction,
        submitIcon = <SaveIcon color='action' />, doneIcon = <CloseIcon />,
        submitText = "Save", doneText = "Done",
        submitting, isValid, submitted, 
        submitRef, formId,
    } = props;
    return (
        <>
            {(!!previousAction || !!nextAction) && <ButtonGroup
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                {!!previousAction && <Button
                    color="secondary"
                    disabled={submitting}
                    variant='outlined'
                    startIcon={previousIcon}
                    onClick={() => { previousAction() }}
                />}
                {!!nextAction && <Button
                    color="secondary"
                    disabled={submitting}
                    variant='outlined'
                    endIcon={nextIcon}
                    onClick={() => { nextAction() }}
                />}
            </ButtonGroup>}
            <ButtonGroup sx={{ marginLeft: 'auto', }}
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                <LoadingButton
                    form={formId}
                    color="primary"
                    type={!!!submitRef? "submit": "button"}
                    variant='contained'
                    disabled={!isValid || submitting || submitted}
                    onClick={(event) => {
                        if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } 
                        if(!!doneAction) {
                            doneAction();
                        }
                    }}
                    startIcon={submitIcon}>
                    {t(submitText)}
                </LoadingButton>
                {!!doneAction && <Button
                    color="secondary"
                    autoFocus
                    disabled={submitting}
                    variant='contained'
                    startIcon={doneIcon}
                    onClick={() => { doneAction() }}
                >
                    {t(doneText)}
                </Button>}
            </ButtonGroup>
        </>
    );
}
