import { useState } from 'react';
import { ButtonGroup, Checkbox, IconButton } from '@mui/material';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

// Import all icons for now, for DetailsPartial only
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { AppDispatch } from 'src/store/Store';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { ButtonTypes } from 'src/shared/views/buttonGroups/ButtonTypes';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { defaultItemCardProps } from 'src/shared/views/ItemCardProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { ItemViewsPartialProps } from 'src/shared/views/ItemViewsPartialProps';

import { defaultProduct, IProductDataModel } from 'src/dataModels/IProductDataModel';
import { getIProductIdentifier, getRouteParamsOfIProductIdentifier } from 'src/dataModels/IProductQueries';

import { post, put } from 'src/generated/slices/ProductSlice';
import CreatePartial from './CreatePartial';
import DetailsPartial from './DetailsPartial';
import EditPartial from './EditPartial';

// 1. we have following use cases of CRUD Item <Card />:
// 1.1. In HtmlTablePartial, must be <Dialog />, use <ItemViewsPartial />
// 1.2. In TilesPartial, can be <Dialog /> or inline, use <ItemViewsPartial />
// 1.3. In SlideShow, inline, use <ItemViewsPartial />
// 1.4. In Dashboard, inline, use <ItemViewsPartial />, because of switch ViewTemplates
// 1.5. In Wizard(Create for now, may be Edit in the future), <CreatePartial /> directly 
// 1.6. When ChildLookup in ParentLookup, <ItemViewsPartial />

// 2. Function in this component
// 2.1. get/post/delete/put in ...slice.ts will be called in this component, as default SubmitAction if exists
// 2.1.1. together with all states
// 2.2. In Wizard or ChildLookup in ParentLookup: we pass data back to the container component, then update together when container component submit
// 2.3. Details/Delete/Edit partial views: previous/next in will be defined in this component
// 2.4. Create partial view: createAnother will be defined in this component

// 3. Change from one view to another, e.g. Details->Edit will be defined in this class.

// 4. Props:
// 4.1. Create/Details/Delete/Edit partial views are using ItemCardProps<IPartnerDataModel>
// 4.2. this component will use ItemViewsPartialProps

// 5. ButtonGroups: will be rendered in each CreatePartial/DeletePartial/DetailsPartial/EditPartial
// 5.1. the reason is Create/Edit are using redux-hook-form, isValid and isDirty must be used to enable/disable submit button
export default function ItemViewsPartial(props: ItemViewsPartialProps<IProductDataModel>): JSX.Element {
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();
	const navigate = useNavigate();

    const {
        item,
        crudViewContainer,
        totalCountInList, itemIndex, setItemIndex,
        isItemSelected, handleSelectItemClick,
        hasDoneButton,
        submitAction,
        mainButtonContainer, mainButtonType,
		handleItemDialogOpen, // for DetailsPartial to render extraButtonGroups
        gridColumns, //  by default how to display editors/inputs in mui Grid system.
        scrollableCardContent,
    } = props;
    const [viewItemTemplate, setViewItemTemplate] = useState<ViewItemTemplates>(props.viewItemTemplate);
	
    // 1. doneAction changed to changeViewItemTemplate(newViewItemTemplate)   // 1. CrudViewContainers.Dialog:
    // 1.1.1. always close Dialog // use existing doneAction
    // 1.2. CrudViewContainers.StandaloneView:
    // 1.2.1. go back to previous page. // use existing doneAction
    // 1.3. CrudViewContainers.Inline:
    // 1.3.1. When Details: no doneAction (== null).
    // 1.3.2. changeViewItemTemplate(ViewItemTemplates.Details)
    // 1.4. CrudViewContainers.Card: When Master Table
    // 1.4.1. When Details: go back to previous page.
    // 1.4.2. Otherwise: changeViewItemTemplate(ViewItemTemplates.Details)
    const doneAction = 
        !hasDoneButton
            ? null
            : 	() => {
                if ((crudViewContainer === CrudViewContainers.Inline || crudViewContainer === CrudViewContainers.Card) &&
                    viewItemTemplate !== ViewItemTemplates.Details) {
                    changeViewItemTemplate(props.viewItemTemplate);
                }
                else {
                    props.doneAction();
                }
            }
	
    const changeViewItemTemplate = (newViewItemTemplate: ViewItemTemplates) => {
        setViewItemTemplate(newViewItemTemplate);
    }

    // 2. Previous/Next When Dialog
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


    const [submitting, setSubmitting] = useState(false);
    const [submitted, setSubmitted] = useState(false);

    const [submitMessage, setSubmitMessage] = useState<string>();

    // 3.1. Create submitAction
    const [createAnother, setCreateAnother] = useState(true);
    const handleChangeCreateAnother = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreateAnother(event.target.checked);
    };

    const onSubmit_WhenCreate_DispatchAsyncThunk = (data: IProductDataModel, index: number) => {
        setSubmitting(true);

        dispatch(post({ ...data }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    if (createAnother) {
                        setSubmitting(false);
                        setSubmitted(false);
                        setSubmitMessage(null);
                        // reset(item);
                    }
                    else {
                        setSubmitMessage(t('SuccessfullySaved'));
                        setSubmitted(true);
                    }
                }
                else { // failed
                    setSubmitMessage(t('FailedToSave'));
                }
                // console.log(result);
            })
            .catch((error: any) => { setSubmitMessage(t('FailedToSave')); })
            .finally(() => { setSubmitting(false); });
    }

    // 3.4. Edit submitAction
    const onSubmit_WhenEdit_DispatchAsyncThunk = (data: IProductDataModel, index: number) => {
        setSubmitting(true);
        dispatch(put({ identifier: getIProductIdentifier(data), data: { ...data } }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSubmitMessage(t('SuccessfullySaved'));
                    setSubmitted(true);
                }
                else { // failed
                    setSubmitMessage(t('FailedToSave'));
                }
            })
            .catch((error: any) => { setSubmitMessage(t('FailedToSave')); })
            .finally(() => { setSubmitting(false); });
    }


    const renderExtraButtonGroups_OnDetailsPartial = () => (
        <>

            <ButtonGroup
                disableElevation
                variant="contained"
                aria-label="navigation buttons"
                >
            {(crudViewContainer === CrudViewContainers.Card || crudViewContainer === CrudViewContainers.Inline) &&
                <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, null) }}>
                    <EditIcon />
                </IconButton>
            }
            </ButtonGroup>
        </>
    );

    return (
        <>


            {viewItemTemplate === ViewItemTemplates.Create &&
                <CreatePartial {...defaultItemCardProps} doneAction={doneAction} item={defaultProduct()}
                    mainButtonContainer={mainButtonContainer} mainButtonType={mainButtonType}
                    hasDoneButton={hasDoneButton}
                    submitAction={submitAction ?? onSubmit_WhenCreate_DispatchAsyncThunk} submitting={submitting} submitted={submitted} submitMessage={submitMessage}
                    handleChangeCreateAnother={handleChangeCreateAnother}
                    gridColumns={gridColumns}
                    scrollableCardContent={scrollableCardContent}
                />}

            {viewItemTemplate === ViewItemTemplates.Details &&
                <DetailsPartial {...defaultItemCardProps} doneAction={doneAction} item={item} itemIndex={itemIndex}
                    mainButtonContainer={mainButtonContainer} mainButtonType={mainButtonType}
                    previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog}
                    hasDoneButton={hasDoneButton}
                    isItemSelected={isItemSelected} handleSelectItemClick={handleSelectItemClick}
                    extraButtonGroups={renderExtraButtonGroups_OnDetailsPartial()}
                    gridColumns={gridColumns}
                    scrollableCardContent={scrollableCardContent}
                />}

            {viewItemTemplate === ViewItemTemplates.Edit &&
                <EditPartial {...defaultItemCardProps} doneAction={doneAction} item={item} itemIndex={itemIndex}
                    mainButtonContainer={mainButtonContainer} mainButtonType={mainButtonType}
                    previousAction={gotoPreviousItemOnDialog} nextAction={gotoNextItemOnDialog}
                    hasDoneButton={hasDoneButton}
                    submitAction={submitAction ?? onSubmit_WhenEdit_DispatchAsyncThunk} submitting={submitting} submitted={submitted} submitMessage={submitMessage}
                    isItemSelected={isItemSelected} handleSelectItemClick={handleSelectItemClick}
                    gridColumns={gridColumns}
                    scrollableCardContent={scrollableCardContent}
                />}
        </>
    );
}

