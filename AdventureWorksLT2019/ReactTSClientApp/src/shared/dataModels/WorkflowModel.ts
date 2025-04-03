import { AppWellKnownActions } from "../views/AppWellKnownActions";

export interface WorkflowModel {
    key: any;
    nextSteps: WorkflowStepModel[]
}

export interface WorkflowStepModel {
    key: any,
    action: AppWellKnownActions;
}
