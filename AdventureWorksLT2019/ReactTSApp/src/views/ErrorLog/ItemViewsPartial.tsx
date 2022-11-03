import { useState } from 'react';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import CreatePartial from './CreatePartial';
import DeletePartial from './DeletePartial';
import DetailsPartial from './DetailsPartial';
import EditPartial from './EditPartial';

export default function ItemViewsPartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
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

    const doneAction = crudViewContainer === CrudViewContainers.Dialog
        ? props.doneAction
        : () => { changeViewItemTemplate(props.viewItemTemplate) };

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


