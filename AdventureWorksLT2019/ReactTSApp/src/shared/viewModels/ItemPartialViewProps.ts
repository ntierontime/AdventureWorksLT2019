import { Orientation } from "@mui/material";
import { RegularBreakpoints } from "@mui/material/Grid";
import { ContainerOptions } from "./ContainerOptions";
import { CrudViewContainers } from "./CrudViewContainers";
import { ViewItemTemplates } from "./ViewItemTemplates";

export const multiColumnItemViewGrid = {
    xs: 12,
    sm: 12,
    md: 6,
    lg: 4,
    xl: 3,
}

export const scrollableCardContent = { maxHeight: '75vh', overflow: 'auto' };

export interface ItemPartialViewPropsBase<TDataModel> {
    buttonContainer: ContainerOptions;
    gridColumns: RegularBreakpoints;
    scrollableCardContent: any,
    crudViewContainer: CrudViewContainers,
    viewItemTemplate: ViewItemTemplates,
    item: TDataModel,
}

// This props is used for regular CRUD item partial views
export interface ItemPartialViewProps<TDataModel> extends ItemPartialViewPropsBase<TDataModel> {

    // #region crudViewContainer !== CrudViewContainers.Wizard
    isItemSelected: boolean,
    handleSelectItemClick: (item: TDataModel) => void,
    changeViewItemTemplate: (newViewItemTemplate: ViewItemTemplates) => void,

    // collapsable and collapsed are for Tiles, not for HtmlTable for now
    collapsable: boolean,
    collapsed: boolean,

    doneAction: () => void,
    // the following will be set in Markup
    totalCountInList: number,
    itemIndex: number,
    setItemIndex: React.Dispatch<React.SetStateAction<number>>,
    // the following will be set in ItemViewsPartial
    previousAction: () => void,
    nextAction: () => void,

    handleItemDialogOpen: (viewItemTemplate: ViewItemTemplates, itemIndex: number) => void, // to open RUD Dialog
    // #endregion crudViewContainer !== CrudViewContainers.Wizard

    // #region crudViewContainer === CrudViewContainers.Wizard
    wizardOrientation: Orientation | null,
    // buttonContainerRef is not null when Orientation === horizontal, Buttons will wrapped in <Portal /> in the card, then display in the wizard bottom action bard
    // buttonContainerRef is null when Orientation === vertical, Buttons will be in the card
    onWizardStepSubmit: (data: TDataModel) => void,
    renderWizardButtonGroup: (isFirstStep: boolean, isLastStep: boolean, isStepOptional:boolean, disableNextButton: ()=>boolean, submitRef: React.MutableRefObject<any>) => JSX.Element,
    isFirstStep: boolean,
    isLastStep: boolean,
    isStepOptional:boolean,
    // #endregion crudViewContainer === CrudViewContainers.Wizard
}

export function getCRUDItemPartialViewPropsOnDialog<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardBottom,
        gridColumns: multiColumnItemViewGrid,
        scrollableCardContent: scrollableCardContent,
        crudViewContainer: CrudViewContainers.Dialog,
        viewItemTemplate,
        item: null,

        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog

        // #region crudViewContainer === CrudViewContainers.Wizard
        wizardOrientation: null,
        onWizardStepSubmit: null,
        renderWizardButtonGroup: null,
        isFirstStep: false,
        isLastStep: false,
        isStepOptional: false,
        // #endregion crudViewContainer === CrudViewContainers.Wizard
    };
}

export function getCRUDItemPartialViewPropsInline<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardToolbar,
        gridColumns: null,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.Inline,
        viewItemTemplate,
        item: null,

        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog

        // #region crudViewContainer === CrudViewContainers.Wizard
        wizardOrientation: null,
        onWizardStepSubmit: null,
        renderWizardButtonGroup: null,
        isFirstStep: false,
        isLastStep: false,
        isStepOptional: false,
        // #endregion crudViewContainer === CrudViewContainers.Wizard
    };
}

export function getCRUDItemPartialViewPropsStandalone<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardHead,
        gridColumns: multiColumnItemViewGrid,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.StandaloneView,
        viewItemTemplate,
        item: null,

        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
        
        // #region crudViewContainer === CrudViewContainers.Wizard
        wizardOrientation: null,
        onWizardStepSubmit: null,
        renderWizardButtonGroup: null,
        isFirstStep: false,
        isLastStep: false,
        isStepOptional: false,
        // #endregion crudViewContainer === CrudViewContainers.Wizard
    };
}

export function getCRUDItemPartialViewPropsCard<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardToolbar,
        gridColumns: null,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.Card,
        viewItemTemplate,
        item: null,

        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
        
        // #region crudViewContainer === CrudViewContainers.Wizard
        wizardOrientation: null,
        onWizardStepSubmit: null,
        renderWizardButtonGroup: null,
        isFirstStep: false,
        isLastStep: false,
        isStepOptional: false,
        // #endregion crudViewContainer === CrudViewContainers.Wizard
    };
}

export function getCRUDItemPartialViewPropsWizard<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    wizardOrientation: Orientation,
    onWizardStepSubmit: (data: TDataModel) => void,
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardHead,
        gridColumns: multiColumnItemViewGrid,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.Wizard,
        viewItemTemplate,
        item: null,
        
        // #region crudViewContainer === CrudViewContainers.Wizard
        wizardOrientation,
        onWizardStepSubmit,
        renderWizardButtonGroup: null,
        isFirstStep: false,
        isLastStep: false,
        isStepOptional: false,
        // #endregion crudViewContainer === CrudViewContainers.Wizard

        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction: null,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog

    };
}