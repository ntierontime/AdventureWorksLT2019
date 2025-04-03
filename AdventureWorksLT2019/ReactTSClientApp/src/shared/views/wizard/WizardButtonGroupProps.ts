import { WizardStepProps } from "./WizardStepProps";

export interface WizardButtonGroupProps {
    
    activeStep: number;
    wizardSteps: WizardStepProps[];

    handleBack: () => void;
    handleSkip: () => void;
    handleReset: () => void;
    handleNext: () => void;

    submitting: boolean;
    submitted: boolean;

    isFirstStep: boolean;
    isLastStep: boolean; 
    isStepOptional: boolean;
    disableNextButton: () => boolean;
    submitRef: React.MutableRefObject<any>;
}