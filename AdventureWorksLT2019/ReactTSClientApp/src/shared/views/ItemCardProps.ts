import { Orientation, RegularBreakpoints } from "@mui/material";
import { ButtonTypes } from "./buttonGroups/ButtonTypes";
import { CardButtonGroupPosition } from "./buttonGroups/CardButtonGroupPosition";

// card height 100% of parent
// <CardActions disableSpacing sx={{ mt: "auto" }} /> will stick to the bottom of this card
export const card100PercentHeighFlex = {
    height: "100%",
    display: "flex",
    flexDirection: "column",
}
export const scrollableCardContent = { 
    maxHeight: '60vh', overflow: 'auto' 
};

export interface ItemCardProps<TDataModel> {
    // 6. for DetailsPartial only for now
    extraButtonGroups?: JSX.Element;
    // 5. for Wizard only
    wizardOrientation?: Orientation | null,
    // buttonContainerRef is not null when Orientation === horizontal, Buttons will wrapped in <Portal /> in the card, then display in the wizard bottom action bard
    // buttonContainerRef is null when Orientation === vertical, Buttons will be in the card
    renderWizardButtonGroup?: (isFirstStep: boolean, isLastStep: boolean, isStepOptional:boolean, disableNextButton: ()=>boolean, submitRef: React.MutableRefObject<any>) => JSX.Element,
    isFirstStep?: boolean,
    isLastStep?: boolean,
    isStepOptional?: boolean,

    // 4. when used in ListPartial.tsx, when Details/Delete/Edit, passed in when props in <ItemViewsPartial />
    totalCountInList?: number;
    itemIndex?: number;

    // 4.1. Defined in <ItemViewsPartial />
    previousAction?: () => void,
    nextAction?: () => void,

    // 3. when select enabled, when used in ListPartial.tsx, when Details/Delete/Edit, passed in when props in <ItemViewsPartial />
    // 3.1. disable/hide select checkbox when handleSelectItemClick is null
    isItemSelected?: boolean,
    handleSelectItemClick?: (item: TDataModel) => void,


    // 2.2. used by Create PartialViews only
    // createAnother: boolean;
    handleChangeCreateAnother?: (event: React.ChangeEvent<HTMLInputElement>) => void;

    // 2.1. used by Create/Delete/Edit PartialViews
    // We are process submitted data outside of the CRUD partial, together with save/delete/create state: saving/saved..., 
    // "isDirty", "isValid" will be in the ItemCard because they are from redux-hook-form, must inside partial views.  
    submitting?: boolean;
    submitted?: boolean;
    submitMessage?: string;
    submitAction?: (item: TDataModel, index: number) => void;

    // 1. Below are common messages, used for several views
    hasDoneButton?: boolean;
    doneAction: () => void,

    mainButtonContainer: CardButtonGroupPosition;
    mainButtonType: ButtonTypes;

    showCloseIconOnTopRight: boolean; // display an x/Cancel/Close button on Top Right
    
    showCardHeader: boolean; // false when in wizard?
    showCardContent: boolean; // false when gmail-like UI, summary CardHeader only.
    showCardActionButtonGroups: boolean;  // false when in wizard, in wizard, all buttons will be out side.

    //buttonGroups: Record<CardButtonGroupPosition, JSX.Element>;
    item: TDataModel;

    // export const multiColumnItemViewGrid = {
    //     xs: 12,
    //     sm: 12,
    //     md: 6,
    //     lg: 4,
    //     xl: 3,
    // }
    // gmail like UI, when all are 12, with left and/or right SideVertMenu
    // gridColumns, by default, is null, will use the customized value in each React Component
    // gridColumns, set to a not null value, will not-null-value in each React Component, e.g. 1/2 column
    gridColumns?: RegularBreakpoints; 
    scrollableCardContent?: any;
}

// default ItemCardProps
export const defaultItemCardProps = {
    showCloseIconOnTopRight: true, // display an x/Cancel/Close button on Top Right
    
    showCardHeader: true, // false when in wizard?
    showCardContent: true, // false when gmail-like UI, summary CardHeader only.
    showCardActionButtonGroups: true,  // false when in wizard, in wizard, all buttons will be out side.

    // gmail like UI, when all are 12, with left and/or right SideVertMenu
    scrollableCardContent: scrollableCardContent,
}
