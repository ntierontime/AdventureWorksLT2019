import { IconButtonPropsSizeOverrides } from "@mui/material";
import { OverridableStringUnion } from "@mui/types";
import { WorkflowModel, WorkflowStepModel } from "src/shared/dataModels/WorkflowModel";

export default interface EnumWorkflowStepButtonProps {
    workflowStep: WorkflowStepModel;
    identifier: any;
    item: any;
    value: any;
    submitting: boolean;
    onButtonClick?: (identifier: any, item: any, currentValue:any) => void;
    confirmDialogCaption: string;
    note?: string;
    size?: OverridableStringUnion<'small' | 'medium' | 'large', IconButtonPropsSizeOverrides>;
}

export interface EnumWorkflowStepButtonGroupProps {
    currentWorkflow: WorkflowModel;
    identifier: any;
    item: any;
    submitting: boolean;
    onButtonClick?: (identifier: any, item: any, currentValue:any) => void;
    confirmDialogCaption: string;
    size?: OverridableStringUnion<'small' | 'medium' | 'large', IconButtonPropsSizeOverrides>;
}