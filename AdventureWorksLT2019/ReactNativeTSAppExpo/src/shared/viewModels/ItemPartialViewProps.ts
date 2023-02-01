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

export interface ItemPartialViewProps<TDataModel> {
    buttonContainer: ContainerOptions;
    scrollableCardContent: any,
    crudViewContainer: CrudViewContainers,
    viewItemTemplate: ViewItemTemplates,
    item: TDataModel,
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
}

export function getCRUDItemPartialViewPropsOnDialog<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardBottom,
        scrollableCardContent: scrollableCardContent,
        crudViewContainer: CrudViewContainers.Dialog,
        viewItemTemplate,
        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        item: null,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
    };
}

export function getCRUDItemPartialViewPropsInline<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardToolbar,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.Inline,
        viewItemTemplate,
        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        item: null,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
    };
}

export function getCRUDItemPartialViewPropsStandalone<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardHead,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.StandaloneView,
        viewItemTemplate,
        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        item: null,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
    };
}

export function getCRUDItemPartialViewPropsCard<TDataModel>(
    viewItemTemplate: ViewItemTemplates,
    doneAction: () => void
): ItemPartialViewProps<TDataModel> {
    return {
        buttonContainer: ContainerOptions.ItemCardToolbar,
        scrollableCardContent: null,
        crudViewContainer: CrudViewContainers.Card,
        viewItemTemplate,
        isItemSelected: false,
        handleSelectItemClick: null,
        totalCountInList: -1,
        itemIndex: -1,
        setItemIndex: null,
        doneAction,
        item: null,
        previousAction: null,
        nextAction: null,
        changeViewItemTemplate: null,

        // collapsable and collapsed are for Tiles, not for HtmlTable for now
        collapsable: false,
        collapsed: false,

        handleItemDialogOpen: null, // to open RUD Dialog
    };
}