import { Button, ButtonGroup, Checkbox, FormControlLabel, IconButton } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';
import { useTranslation } from "react-i18next";
import { ItemActionButtonGroupProps } from "src/shared/views/buttonGroups/ItemActionButtonGroupProps";


// 1. This ItemActionIconAndTextButtonGroups can be used in Dialog and a page, in <Card />, at <CardAction/> below <CardHeader> as ToolBar preferred
// 2. Can also used in <CardHeader action={} />
// 2.1. left side are the previous/next buttons, no text,
// 2.2. right side are the submit/done buttons, without text,
export function CreateItemActionIconButtonGroup(props: ItemActionButtonGroupProps): JSX.Element {
    const { t } = useTranslation();
    const {
        handleChangeCreateAnother, createAnotherLabel = "CreateAnotherOne",
        doneAction,
        submitIcon = <SaveIcon color='action' />, doneIcon = <CloseIcon />,
        submitting, isValid, submitted, } = props;
    return (
        <>
            <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t(createAnotherLabel)} />
            <IconButton aria-label="create" color="primary" type='submit' disabled={!isValid || submitting || submitted}>
                {submitIcon}
            </IconButton>
            <IconButton aria-label="close" disabled={submitting} onClick={() => { doneAction() }}>
                {doneIcon}
            </IconButton>
        </>
    );
}

// 1. This ItemActionIconAndTextButtonGroups can be used in Dialog and a page, in <Card />, at bottom <CardAction/> preferred
// 2.1. left side are the previous/next buttons, no text,
// 2.2. right side are the submit/done buttons, with text, 
export function CreateItemActionIconAndTextButtonGroup(props: ItemActionButtonGroupProps): JSX.Element {
    const { t } = useTranslation();
    const {
        handleChangeCreateAnother, createAnotherLabel = "CreateAnotherOne",
        doneAction,
        submitIcon = <SaveIcon color='action' />, doneIcon = <CloseIcon />,
        submitText = "Save", doneText = "Done",
        submitting, isValid, submitted, } = props;
    return (
        <>
            <FormControlLabel control={<Checkbox defaultChecked onChange={handleChangeCreateAnother} />} label={t("CreateAnotherOne")} />
            <ButtonGroup sx={{ marginLeft: 'auto', }}
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                <Button
                    type='submit'
                    fullWidth
                    variant='contained'
                    disabled={!isValid || submitting || submitted}
                    startIcon={submitIcon}>
                    {t(submitText)}
                </Button>
                <Button
                    autoFocus
                    disabled={submitting}
                    fullWidth
                    variant='contained'
                    startIcon={doneIcon}
                    onClick={() => { doneAction() }}
                >
                    {t(doneText)}
                </Button>
            </ButtonGroup>
        </>
    );
}
