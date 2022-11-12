import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';

import { useNavigate } from "react-router-dom";
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Controller } from 'react-hook-form';
import { DatePicker } from '@mui/x-date-pickers';


// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getAddressAvatar, IAddressDataModel, addressFormValidationWhenEdit } from 'src/dataModels/IAddressDataModel';
import { put } from 'src/slices/AddressSlice';

export default function EditPartial(props: ItemPartialViewProps<IAddressDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { gridColumns, scrollableCardContent, crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, setValue, handleSubmit, formState: { isValid, errors, isDirty } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);




    const onSubmit = (data: IAddressDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: { addressID: data.addressID }, data: { ...data } }))
            .then((result) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getAddressAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);

    const renderButtonGroupWhenCard = () => {
        return (
            <>
                <IconButton
                    color="primary"
                    type='submit'
                    disabled={(!isValid || saving || saved) && !isDirty}
                    aria-label="save">
                    <SaveIcon />
                </IconButton>
                <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
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
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
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
                {!!changeViewItemTemplate && <IconButton aria-label="edit" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete); }} disabled={saving}>
                    <DeleteIcon />
                </IconButton>}
                {!!doneAction && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
            </>
        );
    }

    const renderButtonGroupWhenStandaloneView = () => {
        return (
            <>
                <LoadingButton
                    color="primary"
                    type='submit'
                    variant='contained'
                    disabled={(!isValid || saving || saved) && !isDirty}
                    startIcon={<SaveIcon color='action' />}>
                    {t('Save')}
                </LoadingButton>
                <IconButton aria-label="close"
                    onClick={() => {
                        navigate(-1);
                    }} disabled={saving}
                >
                    <CloseIcon />
                </IconButton>
            </>
        );
    }

    return (
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
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
            {!!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.addressLine1}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='addressLine1'
                                label={t('AddressLine1')}
                                defaultValue={item.addressLine1}
                                variant='outlined'
                                margin='normal'
                                {...register("addressLine1", addressFormValidationWhenEdit.addressLine1)}
                                autoComplete='addressLine1'
                                error={!!errors.addressLine1}
                                fullWidth
                                helperText={!!errors.addressLine1 ? t(errors.addressLine1.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='addressLine2'
                                label={t('AddressLine2')}
                                defaultValue={item.addressLine2}
                                variant='outlined'
                                margin='normal'
                                {...register("addressLine2", addressFormValidationWhenEdit.addressLine2)}
                                autoComplete='addressLine2'
                                error={!!errors.addressLine2}
                                fullWidth
                                helperText={!!errors.addressLine2 ? t(errors.addressLine2.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='city'
                                label={t('City')}
                                defaultValue={item.city}
                                variant='outlined'
                                margin='normal'
                                {...register("city", addressFormValidationWhenEdit.city)}
                                autoComplete='city'
                                error={!!errors.city}
                                fullWidth
                                helperText={!!errors.city ? t(errors.city.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='stateProvince'
                                label={t('StateProvince')}
                                defaultValue={item.stateProvince}
                                variant='outlined'
                                margin='normal'
                                {...register("stateProvince", addressFormValidationWhenEdit.stateProvince)}
                                autoComplete='stateProvince'
                                error={!!errors.stateProvince}
                                fullWidth
                                helperText={!!errors.stateProvince ? t(errors.stateProvince.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='countryRegion'
                                label={t('CountryRegion')}
                                defaultValue={item.countryRegion}
                                variant='outlined'
                                margin='normal'
                                {...register("countryRegion", addressFormValidationWhenEdit.countryRegion)}
                                autoComplete='countryRegion'
                                error={!!errors.countryRegion}
                                fullWidth
                                helperText={!!errors.countryRegion ? t(errors.countryRegion.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='postalCode'
                                label={t('PostalCode')}
                                defaultValue={item.postalCode}
                                variant='outlined'
                                margin='normal'
                                {...register("postalCode", addressFormValidationWhenEdit.postalCode)}
                                autoComplete='postalCode'
                                error={!!errors.postalCode}
                                fullWidth
                                helperText={!!errors.postalCode ? t(errors.postalCode.message) : ''}
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
                            <Controller
                                name="modifiedDate"
                                defaultValue={item.modifiedDate}
                                control={control}
                                {...register("modifiedDate", addressFormValidationWhenEdit.modifiedDate)}
                                render={
                                    ({ field: { onChange, ...restField } }) =>
                                        <DatePicker
                                            ref={null}
                                            label={t('ModifiedDate')}
                                            onChange={(event) => { onChange(event); }}
                                            renderInput={(params) =>
                                                <TextField
                                                    ref={null}
                                                    fullWidth
                                                    autoComplete='modifiedDate'
                                                    error={!!errors.modifiedDate}
                                                    helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                                                    {...params}
                                                />}
                                            {...restField}
                                        />
                                }
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
                        disabled={saving}
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        disabled={saving}
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                <ButtonGroup sx={{ marginLeft: 'auto', }}
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    <LoadingButton
                        color="primary"
                        type='submit'
                        variant='contained'
                        disabled={!isValid || saving || saved}
                        startIcon={<SaveIcon color='action' />}>
                        {t('Save')}
                    </LoadingButton>
                    {!!doneAction && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
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


