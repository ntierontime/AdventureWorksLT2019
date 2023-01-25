import { useState } from 'react';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import CreatePartial from './CreatePartial';
import DeletePartial from './DeletePartial';
import DetailsPartial from './DetailsPartial';
import EditPartial from './EditPartial';

export default function ItemViewsPartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const { crudViewContainer, totalCountInList, itemIndex, setItemIndex } = props;
    const [viewItemTemplate, setViewItemTemplate] = useState<ViewItemTemplates>(props.viewItemTemplate);

    const gotoPreviousItemOnDialog = crudViewContainer === CrudViewContainers.Dialog && itemIndex >= 0
        ? () => {
            const previousIndex = itemIndex === 0 ? totalCountInList - 1 : itemIndex - 1;
            setItemIndex(previousIndex);
        }
        : null;

    const gotoNextItemOnDialog = crudViewContainer === CrudViewContainers.Dialog && itemIndex >= 0
        ? () => {
            const nextIndex = itemIndex === totalCountInList - 1 ? 0 : itemIndex + 1;
            setItemIndex(nextIndex);
        }
        : null;

    const changeViewItemTemplate = (newViewItemTemplate: ViewItemTemplates) => {
        setViewItemTemplate(newViewItemTemplate);
    }


    // 1. CrudViewContainers.Dialog:
    // 1.1. always close Dialog // use existing doneAction
    // 2. CrudViewContainers.StandaloneView:
    // 2.1. go back to previous page. // use existing doneAction
    // 3. CrudViewContainers.Inline:
    // 3.1. When Details: no doneAction (== null).
    // 3.2. changeViewItemTemplate(ViewItemTemplates.Details)
    // 4. CrudViewContainers.Card: When Master Table
    // 4.1. When Details: go back to previous page.
    // 4.2. Otherwise: changeViewItemTemplate(ViewItemTemplates.Details)
    const doneAction = () => {
        // 1. and 2.
        if (crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.StandaloneView) {
            props.doneAction()
        }

        // 3.
        if (crudViewContainer === CrudViewContainers.Inline) {
            if(viewItemTemplate !== ViewItemTemplates.Details) {
                changeViewItemTemplate(props.viewItemTemplate);
            }
        }
        // 4.
        else if (crudViewContainer === CrudViewContainers.Card) {
            // when master
            if(viewItemTemplate !== ViewItemTemplates.Details) {
                changeViewItemTemplate(props.viewItemTemplate);
            }
            else {
                props.doneAction();
            }
        }
    }

    return (
        <>
            {viewItemTemplate === ViewItemTemplates.Create &&
                <CreatePartial {...props} />
            }
            {viewItemTemplate === ViewItemTemplates.Delete &&
                <DeletePartial {...props} previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog} changeViewItemTemplate={changeViewItemTemplate} doneAction={doneAction} />
            }
            {viewItemTemplate === ViewItemTemplates.Details &&
                <DetailsPartial {...props} previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog} changeViewItemTemplate={changeViewItemTemplate} doneAction={doneAction} />
            }
            {viewItemTemplate === ViewItemTemplates.Edit &&
                <EditPartial {...props} previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog} changeViewItemTemplate={changeViewItemTemplate} doneAction={doneAction} />
            }
        </>
    );
}

