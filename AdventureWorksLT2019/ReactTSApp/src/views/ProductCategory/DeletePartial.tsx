import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { Link } from 'react-router-dom';
import { DatePicker } from '@mui/x-date-pickers';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductCategoryAvatar, IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { delete1 } from 'src/slices/ProductCategorySlice';

export default function DeletePartial(props: ItemPartialViewProps<IProductCategoryDataModel>): JSX.Element {
    const navigate = useNavigate();
    
    const { gridColumns, scrollableCardContent, crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
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
        dispatch(delete1({ productCategoryID: item.productCategoryID }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setDeleteMessage(item.name + " " + t('Deleted') + "!");
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
    const avatar = getProductCategoryAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton 
                    color="primary"
                    disabled={deleted}
                    aria-label="delete" 
                    onClick={() => { onDelete() }}>
                    <DeleteIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
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

                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit); }} disabled={deleting || deleted}>
                    <EditIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={deleting}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenInline = () => {
        return (
            <>
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
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
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
            </>
        );
    }

    return (
        <Card>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.name}>
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
                title={item.name}
                subheader={t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}
            />
            <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="subtitle1">
                    {deleteMessage}
                </Typography>
            </CardContent>
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='productCategoryID'
                                label={t('ProductCategoryID')}
                                value={item.productCategoryID}
                                variant='outlined'
                                margin='normal'
                                fullWidth
                                InputProps={{
                                    readOnly: true
                                }}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("ParentProductCategoryID")}</Typography>
                                <Link to={"/productCategory/Details/" + item.parentProductCategoryID}>{item.parent_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='name'
                                label={t('Name')}
                                defaultValue={item.name}
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

