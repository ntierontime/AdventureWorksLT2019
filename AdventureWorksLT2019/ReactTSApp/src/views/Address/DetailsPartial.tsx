import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, IconButton, TextField, useTheme } from '@mui/material';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
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

export default function DetailsPartial(props: ItemPartialViewProps<IAddressDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getAddressAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

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
                        {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <>
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
                            {crudViewContainer === CrudViewContainers.Dialog && <IconButton aria-label="close" onClick={() => { doneAction() }}>
                                <CloseIcon />
                            </IconButton>}
                        </>}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && <>
                            <IconButton aria-label="close"
                                onClick={() => {
                                    navigate(-1);
                                }}
                            >
                                <CloseIcon />
                            </IconButton>
                        </>}
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
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='addressLine1'
                    label={t('AddressLine1')}
                    value={item.addressLine1}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='addressLine2'
                    label={t('AddressLine2')}
                    value={item.addressLine2}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='city'
                    label={t('City')}
                    value={item.city}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='stateProvince'
                    label={t('StateProvince')}
                    value={item.stateProvince}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='countryRegion'
                    label={t('CountryRegion')}
                    value={item.countryRegion}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='postalCode'
                    label={t('PostalCode')}
                    value={item.postalCode}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='_rowguid'
                    label={t('rowguid')}
                	value={item._rowguid}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <DatePicker
                    label={t('ModifiedDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    autoFocus
                    onChange={() => {}}
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


