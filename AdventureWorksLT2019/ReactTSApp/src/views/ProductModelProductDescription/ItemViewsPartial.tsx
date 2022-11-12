import { useState } from 'react';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import DetailsPartial from './DetailsPartial';

export default function ItemViewsPartial(props: ItemPartialViewProps<IProductModelProductDescriptionDataModel>): JSX.Element {
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
            {viewItemTemplate === ViewItemTemplates.Details &&
                <DetailsPartial {...props} previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog} changeViewItemTemplate={changeViewItemTemplate} doneAction={doneAction} />
            }
        </>
    );
}


