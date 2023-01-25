import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';

import { useTranslation } from 'react-i18next';

import { Link } from 'react-router-dom';
import { DatePicker } from '@mui/x-date-pickers';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getCustomerAddressAvatar, ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { delete1 } from 'src/slices/CustomerAddressSlice';

export default function DeletePartial(props: ItemPartialViewProps<ICustomerAddressDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
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
        dispatch(delete1({ customerID: item.customerID, addressID: item.addressID }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setDeleteMessage(item.addressType + " " + t('Deleted') + "!");
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
    const avatar = getCustomerAddressAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroup_IconButtons = () => {
        return (
            <>                {!!handleSelectItemClick && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                ><Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                /></ButtonGroup>}
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
                <IconButton aria-label="edit" onClick={() => { onDelete(); }} disabled={deleting || deleted}>
                    <DeleteIcon />
                </IconButton>
                {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
                </IconButton>}
                {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <ArrowBackIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroup_TextAndIconButtons = () => {
        return (
            <>
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
                    {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={deleting}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                    {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={deleting}
                        variant='contained'
                        startIcon={<ArrowBackIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Back')}
                    </Button>}
                </ButtonGroup>
            </>
        );
    }

    return (
        <Card>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.addressType}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.addressType}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="subtitle1">
                    {deleteMessage}
                </Typography>
            </CardContent>
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("CustomerID")}</Typography>
                                <Link to={"/customer/Details/" + item.customerID}>{item.customer_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("AddressID")}</Typography>
                                <Link to={"/address/Details/" + item.addressID}>{item.address_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='addressType'
                                label={t('AddressType')}
                                defaultValue={item.addressType}
                                variant='outlined'
                                margin='normal'
                                fullWidth
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}

