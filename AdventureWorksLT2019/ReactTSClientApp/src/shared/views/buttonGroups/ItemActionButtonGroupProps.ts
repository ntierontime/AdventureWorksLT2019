export interface ItemActionButtonGroupProps {
    formId?: string;
    submitRef?: React.MutableRefObject<any>; // used for external trigger submit event.
    submitting?: boolean;
    isValid?: boolean;
    submitted?: boolean;

    // submitAction?: (item: TDataModel, index: number) => void,
    doneAction: () => void,
    
    submitIcon?: JSX.Element;
    doneIcon?: JSX.Element;

    submitText?: string;
    doneText?: string;

    // For Edit/Details/Delete Views    
    previousAction?: () => void;
    nextAction?: () => void;

    previousIcon?: JSX.Element;
    nextIcon?: JSX.Element;

    // previousText?: string;
    // nextText?: string;

    // For Create View only
    handleChangeCreateAnother?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    createAnotherLabel?: string;

    // For Details View only
    extraButtonGroups?: JSX.Element;
};

