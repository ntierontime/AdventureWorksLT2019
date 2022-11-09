import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
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

import { Link } from 'react-router-dom';
import { DatePicker } from '@mui/x-date-pickers';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductAvatar, IProductDataModel } from 'src/dataModels/IProductDataModel';

export default function DetailsPartial(props: ItemPartialViewProps<IProductDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getProductAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
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
                subheader={t('{{val, datetime}}', { val: new Date(item.sellStartDate) })}
            />
            <CardContent>
                <TextField
                    name='productID'
                    label={t('ProductID')}
                	value={item.productID}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
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
                <TextField
                    name='productNumber'
                    label={t('ProductNumber')}
                    defaultValue={item.productNumber}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='color'
                    label={t('Color')}
                    defaultValue={item.color}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='standardCost'
                    label={t('StandardCost')}
                	value={t(i18nFormats.double.format, { val: item.standardCost })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='listPrice'
                    label={t('ListPrice')}
                	value={t(i18nFormats.double.format, { val: item.listPrice })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='size'
                    label={t('Size')}
                    defaultValue={item.size}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='weight'
                    label={t('Weight')}
                	value={t(i18nFormats.double.format, { val: item.weight })}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("ProductCategoryID")}</Typography>
                    <Link to={"/productCategory/Details/" + item.productCategoryID}>{item.productCategory_Name}</Link>
                </Stack>
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("ProductModelID")}</Typography>
                    <Link to={"/productModel/Details/" + item.productModelID}>{item.productModel_Name}</Link>
                </Stack>
                <DatePicker
                    label={t('SellStartDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.sellStartDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => {}}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='sellStartDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <DatePicker
                    label={t('SellEndDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.sellEndDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => {}}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='sellEndDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <DatePicker
                    label={t('DiscontinuedDate')}
                    value={t(i18nFormats.dateTime.format, { val: new Date(item.discontinuedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                    onChange={() => {}}
                    renderInput={(params) =>
                        <TextField
                            fullWidth
                            autoComplete='discontinuedDate'
                            {...params}
                            InputProps={{
                                readOnly: true
                            }}
                        />}
                />
                <TextField
                    name='thumbNailPhoto'
                    label={t('ThumbNailPhoto')}
                	value={item.thumbNailPhoto}
                    variant='outlined'
                    margin='normal'
                    fullWidth
                    InputProps={{
                        readOnly: true
                    }}
                />
                <TextField
                    name='thumbnailPhotoFileName'
                    label={t('ThumbnailPhotoFileName')}
                    defaultValue={item.thumbnailPhotoFileName}
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
                <Stack sx={{ p: 2 }}
                    direction="row"
                    justifyContent="space-between"
                    alignItems="center"
                    spacing={2}
                >
                    <Typography>{t("ParentID")}</Typography>
                    <Link to={"/productCategory/Details/" + item.parentID}>{item.parent_Name}</Link>
                </Stack>
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

