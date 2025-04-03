import { Box, Step, StepButton, Stepper, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import { PurchaseUIWorkflowSteps } from "src/shared/dataModels/PurchaseUIWorkflowSteps";

export interface PurchaseStepProps {
    key: PurchaseUIWorkflowSteps;
    description: string;
}

export interface PurchaseStepsProps {
    activeStepKey: PurchaseUIWorkflowSteps;
    completed: { [k: number]: boolean };
    steps: PurchaseStepProps[];
    handleStep?: (step: PurchaseUIWorkflowSteps) => void;
}

export const defaultPurchaseSteps = {
    activeStepKey: PurchaseUIWorkflowSteps.ChooseItems,
    completed: {},
    steps: [
        {key: PurchaseUIWorkflowSteps.ChooseItems, description: PurchaseUIWorkflowSteps.ChooseItems + 'Description'},
        {key: PurchaseUIWorkflowSteps.ChoosePurchaseFor, description: PurchaseUIWorkflowSteps.ChoosePurchaseFor + 'Description'},
        {key: PurchaseUIWorkflowSteps.Confirm, description: PurchaseUIWorkflowSteps.Confirm + 'Description'},
        {key: PurchaseUIWorkflowSteps.Checkout, description: PurchaseUIWorkflowSteps.Checkout + 'Description'},
        {key: PurchaseUIWorkflowSteps.CheckoutFinished, description: PurchaseUIWorkflowSteps.CheckoutFinished + 'Description'},
    ]
}

export default function PurchaseStepsHeader(props: PurchaseStepsProps): JSX.Element {
    const { activeStepKey, steps, completed, handleStep } = props;
    const activeStep = steps.findIndex(v => v.key === activeStepKey);
    const { t } = useTranslation();
    return <Box sx={{ width: '100%' }}>
        <Stepper alternativeLabel activeStep={activeStep}>
            {steps.map((step, index) => (
                <Step key={step.key} completed={completed[index]}>
                    <StepButton color="inherit" onClick={() => { handleStep(step.key) }}>
                        {t(step.key)}
                    </StepButton>
                </Step>
            ))}
        </Stepper>
    </Box>
}
