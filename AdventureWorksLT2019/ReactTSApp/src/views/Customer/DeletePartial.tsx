import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getCustomerAvatar, ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { delete1 } from 'src/slices/CustomerSlice';

export default function DeletePartial(props: ItemPartialViewProps<ICustomerDataModel>): JSX.Element {
    const navigate = useNavigate();
    
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const [deleting, setDeleting] = useState(false);
    const [deleted, setDeleted] = useState(false);

    const [deleteMessage, setDeleteMessage] = useState<string>();

    useEffect(() => {
        setDeleting(false);
        setDeleted(false);
        setDeleteMessage(t('Are_you_sure_you_want_to_delete_this?'));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [item]);

    const onDelete = () => {
        setDeleting(true)
        setDeleteMessage(t('Deleting'));
        dispatch(delete1({ customerID: item.customerID }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setDeleteMessage(item.title + " " + t('Deleted') + "!");
                    setDeleted(true);
                }
                else { // failed
                    setDeleteMessage(t('DeletionFailed'));
                }
                //console.log(result);
            })
            .catch((error) => { setDeleteMessage(t('DeletionFailed')); /*console.log(error);*/ })
            .finally(() => { setDeleting(true); });
    }

    const theme = useTheme();
    const avatar = getCustomerAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    return (
        <Card>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.title}>
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

                            {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit); }} disabled={deleting || deleted}>
                                <EditIcon />
                            </IconButton>}
                            {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                                <CloseIcon />
                            </IconButton>}
                        </>}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && <>
                            <LoadingButton
                                color='primary'
                                variant='contained'
                                loading={deleting}
                                disabled={deleted}
                                startIcon={<DeleteIcon color='action' />}
                                onClick={() => { onDelete(); }}>
                                {t('Delete')}
                            </LoadingButton>
                            <IconButton aria-label="close"
                                onClick={() => {
                                    navigate(-1);
                                }} disabled={deleting}
                            >
                                <CloseIcon />
                            </IconButton>
                        </>}
                    </>
                }
                title={item.title}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="subtitle1">
                    {deleteMessage}
                </Typography>
            </CardContent>
            <CardContent>
                <TextField
                    name='customerID'
                    label={t('CustomerID')}
                	value={item.customerID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <FormControlLabel control={<Checkbox checked={item.nameStyle} readOnly />} label={t('NameStyle')} />
                <TextField
                    name='title'
                    label={t('Title')}
                    value={item.title}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='firstName'
                    label={t('FirstName')}
                    value={item.firstName}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='middleName'
                    label={t('MiddleName')}
                    value={item.middleName}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='lastName'
                    label={t('LastName')}
                    value={item.lastName}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='suffix'
                    label={t('Suffix')}
                    value={item.suffix}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='companyName'
                    label={t('CompanyName')}
                    value={item.companyName}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='salesPerson'
                    label={t('SalesPerson')}
                    value={item.salesPerson}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='emailAddress'
                    label={t('EmailAddress')}
                    value={item.emailAddress}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='phone'
                    label={t('Phone')}
                    value={item.phone}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='passwordHash'
                    label={t('PasswordHash')}
                    value={item.passwordHash}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    autoFocus
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='passwordSalt'
                    label={t('PasswordSalt')}
                    value={item.passwordSalt}
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
            {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <CardActions disableSpacing>
                {(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        disabled={deleting}
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        disabled={deleting}
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
                    <LoadingButton
                        color='primary'
                        variant='contained'
                        loading={deleting}
                        disabled={deleted}
                        startIcon={<DeleteIcon color='action' />}
                        onClick={() => { onDelete(); }}>
                        {t('Delete')}
                    </LoadingButton>
                    {!!doneAction && <Button sx={{ marginLeft: 'auto' }}
                        color='secondary'
                        autoFocus
                        disabled={deleting}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                </ButtonGroup>
            </CardActions>}
        </Card >
    );
}

