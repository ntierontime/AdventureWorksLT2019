import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import { ButtonTypes } from "src/shared/views/buttonGroups/ButtonTypes";

export interface ItemPageProps {
    viewItemTemplate: ViewItemTemplates;
    mainButtonType?: ButtonTypes;
    hasDoneButton?: boolean;
    doneAction?: () => void;
    scrollableCardContent?: any;
}
