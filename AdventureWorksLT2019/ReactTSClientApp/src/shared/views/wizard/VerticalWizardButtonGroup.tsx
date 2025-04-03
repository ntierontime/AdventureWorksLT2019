import { Button, ButtonGroup } from "@mui/material";
import { useTranslation } from "react-i18next";
import { WizardButtonGroupProps } from "./WizardButtonGroupProps";
import { WizardStepOptions } from "./WizardStepProps";

export function VerticalWizardButtonGroup(props: WizardButtonGroupProps) {
    const { t } = useTranslation();

    const {
        isFirstStep, isLastStep, isStepOptional/* no optional when vertical for now */, disableNextButton,
        activeStep, wizardSteps,
        submitRef,
        submitting, submitted,
        handleBack, handleSkip, handleReset, handleNext,
    } = props;

    return (
        <ButtonGroup sx={{ marginLeft: 'auto', }}
            disableElevation
            variant="contained"
            aria-label="navigation buttons"
        >
            <Button
                variant="contained"
                disabled={disableNextButton() || submitting}
                onClick={() => { handleNext(); if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } }}
                sx={{ mt: 1, mr: 1 }}
            >
                {wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.ReviewAndSubmit
                    ? t('Comfirm')
                    : isLastStep ? t('Finish') : t('Next')}
            </Button>
            {activeStep === wizardSteps.length - 1 && (
                <Button onClick={() => { handleReset(); }}>
                    {t('Reset')}
                </Button>
            )}
            <Button
                disabled={isFirstStep || submitting || submitted}
                onClick={() => { handleBack(); if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } }}
                sx={{ mt: 1, mr: 1 }}
            >
                {t('Back')}
            </Button>
        </ButtonGroup>
    );
}