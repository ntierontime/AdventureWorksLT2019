
export enum WizardStepOptions {
    Welcome = 'Welcome',
    ReadOnly = 'ReadOnly',
    Editor = 'Editor',
    ReviewAndSubmit = 'ReviewAndSubmit',
    Done = 'Done',
}
export interface WizardStepProps {
    id: string;
    wizardStepOptions: WizardStepOptions;
    label: string;
    description: string;
    icon: React.ReactElement;
    optional: boolean;
}