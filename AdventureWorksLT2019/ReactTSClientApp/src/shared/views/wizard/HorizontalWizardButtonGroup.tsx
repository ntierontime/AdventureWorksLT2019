import { Box, Button } from "@mui/material";
import { useTranslation } from "react-i18next";
import { WizardButtonGroupProps } from "./WizardButtonGroupProps";
import { WizardStepOptions } from "./WizardStepProps";

// #region 4.1. Render Horizontal Wizard
export function HorizontalWizardButtonGroup(props: WizardButtonGroupProps) {
    const { t } = useTranslation();
    
    const {
        isFirstStep, isLastStep, isStepOptional, disableNextButton,
        activeStep, wizardSteps,
        submitRef,
        submitting, submitted,
        handleBack, handleSkip, handleReset, handleNext, 
    } = props;
    
    return (
        <>
            <Button
                color="inherit"
                disabled={isFirstStep || submitting || submitted}
                onClick={() => { handleBack(); if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } }}
                sx={{ mr: 1 }}
            >
                {t('Back')}
            </Button>
            <Box sx={{ flex: '1 1 auto' }} />
            {isStepOptional && !isLastStep && (
                <Button color="inherit" onClick={handleSkip} sx={{ mr: 1 }}>
                    {t('Skip')}
                </Button>
            )}
            {activeStep === wizardSteps.length - 1 && (
                <Button onClick={() => { handleReset(); }}>
                    {t('Reset')}
                </Button>
            )}
            <Button onClick={() => { handleNext(); if (!!submitRef && !!submitRef.current) { submitRef.current?.click() } }} disabled={disableNextButton() || submitting}>
                {wizardSteps[activeStep].wizardStepOptions === WizardStepOptions.ReviewAndSubmit
                    ? t('Comfirm')
                    : isLastStep ? t('Finish') : t('Next')}
            </Button>
        </>
    );
}