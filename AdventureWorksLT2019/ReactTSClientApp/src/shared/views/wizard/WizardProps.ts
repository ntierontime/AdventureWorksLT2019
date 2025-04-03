import { Orientation } from "@mui/material";
import { WizardStepProps } from "./WizardStepProps";

export interface WizardProps<TCompositeModel> {
    wizardSteps: WizardStepProps[];
    activeStep: number;

}