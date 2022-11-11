import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, useTheme } from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from "react-router-dom";
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getAddressAvatar, IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { getRouteParamsOfIAddressIdentifier } from 'src/dataModels/IAddressQueries';

export default function DetailsPartial(props: ItemPartialViewProps<IAddressDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getAddressAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/Address/Dashboard/" + getRouteParamsOfIAddressIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
				{!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <ChevronLeftIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenDialog = () => {
        return (
            <>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/Address/Dashboard/" + getRouteParamsOfIAddressIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
                {!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
                {crudViewContainer === CrudViewContainers.Dialog && <IconButton aria-label="close" onClick={() => { doneAction() }}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
                <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/Address/Dashboard/" + getRouteParamsOfIAddressIdentifier(item)) }}>
                    <AccountTreeIcon />
                </IconButton>
                {!!handleSelectItemClick && <Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                />}
                {!!handleItemDialogOpen && <>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, itemIndex) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, itemIndex) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, itemIndex) }}>
                        <EditIcon />
                    </IconButton>
                </>}
                <IconButton aria-label="delete" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                    <EditIcon />
                </IconButton>
                <IconButton aria-label="more">
                    <MoreVertIcon />
                </IconButton>
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <IconButton aria-label="close"
                    onClick={() => {
                        navigate(-1);
                    }}
                >
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    return (
        <Card sx={{ minHeight: '100%' }}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.addressLine1}>
                        {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {crudViewContainer === CrudViewContainers.Card && (renderButtonGroupWhenCard())}
                        {crudViewContainer === CrudViewContainers.Dialog && (renderButtonGroupWhenDialog())}
                        {crudViewContainer === CrudViewContainers.Inline && (renderButtonGroupWhenInline())}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && (renderButtonGroupWhenStandaloneView())}
                    </>
                }
                title={item.addressLine1}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            <CardContent>
                <TextField
                    name='addressID'
                    label={t('AddressID')}
                    value={item.addressID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='addressLine1'
                    label={t('AddressLine1')}
                    defaultValue={item.addressLine1}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='addressLine2'
                    label={t('AddressLine2')}
                    defaultValue={item.addressLine2}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='city'
                    label={t('City')}
                    defaultValue={item.city}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='stateProvince'
                    label={t('StateProvince')}
                    defaultValue={item.stateProvince}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='countryRegion'
                    label={t('CountryRegion')}
                    defaultValue={item.countryRegion}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='postalCode'
                    label={t('PostalCode')}
                    defaultValue={item.postalCode}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='rowguid'
                    label={t('rowguid')}
                    value={item.rowguid}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <DatePicker
                    label={t('ModifiedDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => { }}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='modifiedDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
            </CardContent>
            {crudViewContainer === CrudViewContainers.Dialog && <CardActions disableSpacing>
                {(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                <ButtonGroup sx={{ marginLeft: 'auto' }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <Button sx={{ marginLeft: 'auto' }}
                        color="primary"
                        autoFocus
                        variant='contained'
                        startIcon={<CloseIcon color='action' />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>
                </ButtonGroup>
            </CardActions>}
        </Card >
    );
}

