import { Button, ButtonGroup } from "@mui/material";
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import { useTranslation } from "react-i18next";
import { ItemActionButtonGroupProps } from "src/shared/views/buttonGroups/ItemActionButtonGroupProps";


// 1. This ItemActionIconAndTextButtonGroups can be used in Dialog and a page, in <Card />, at <CardAction/> below <CardHeader> as ToolBar preferred
// 2. Can also used in <CardHeader action={} />
// 2.1. left side are the previous/next buttons, no text,
// 2.2. right side are the submit/done buttons, without text,
export function DetailsItemActionIconButtonGroup(props: ItemActionButtonGroupProps): JSX.Element {
    const { t } = useTranslation();
    const {
        previousAction, nextAction,
        previousIcon = <NavigateBeforeIcon />, nextIcon = <NavigateNextIcon />,
        //previousText = "Previous", nextText = "Next",
        doneAction,
        doneIcon = <CloseIcon />,
        extraButtonGroups
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
                    variant='outlined'
                    startIcon={previousIcon}
                    onClick={() => { previousAction() }}
                />}
                {!!nextAction && <Button
                    color="secondary"
                    variant='outlined'
                    endIcon={nextIcon}
                    onClick={() => { nextAction() }}
                />}
            </ButtonGroup>}
            {extraButtonGroups}
            <ButtonGroup sx={{ marginLeft: 'auto', }}
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
            >
                {!!doneAction && <Button
                    color="secondary"
                    autoFocus
                    variant='contained'
                    startIcon={doneIcon}
                    onClick={() => { doneAction() }}
                >
                </Button>}
            </ButtonGroup>
        </>
    );
}
