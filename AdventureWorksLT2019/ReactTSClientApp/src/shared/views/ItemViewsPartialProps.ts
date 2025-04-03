import { RegularBreakpoints } from "@mui/material";
import { CrudViewContainers } from "../viewModels/CrudViewContainers";
import { ViewItemTemplates } from "../viewModels/ViewItemTemplates";
import { ButtonTypes } from "./buttonGroups/ButtonTypes";
import { CardButtonGroupPosition } from "./buttonGroups/CardButtonGroupPosition";

export interface ItemViewsPartialProps<TDataModel> {
    mainButtonContainer: CardButtonGroupPosition;
    mainButtonType: ButtonTypes;
    
    crudViewContainer: CrudViewContainers;
    viewItemTemplate: ViewItemTemplates;
    item?: TDataModel; // null when CreatePartial
    hasDoneButton?: boolean;
    doneAction?: () => void; // use change ViewItemTemplate if ..., look at details in <ItemViewsPartial />

    // used in Delete/Details/Edit
    submitAction?: (item: TDataModel, index: number) => void;

    // used in Delete/Details/Edit when in ListPartial
    totalCountInList?: number;
    itemIndex?: number;
    setItemIndex?: React.Dispatch<React.SetStateAction<number>>;
    isItemSelected?: boolean;
    handleSelectItemClick?: (item: TDataModel) => void;

    // used in Details to render extraButtonGroups
    handleItemDialogOpen?: (viewItemTemplate: ViewItemTemplates, itemIndex: number) => void, // to open RUD Dialog
    
    gridColumns?: RegularBreakpoints; //  by default how to display editors/inputs in mui Grid system.
    scrollableCardContent?: any;
}

