import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, IconButton, TextField, Typography, useTheme } from '@mui/material';
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

import { defaultAddress, getAddressAvatar, IAddressDataModel, addressFormValidationWhenEdit } from 'src/dataModels/IAddressDataModel';
import { put } from 'src/slices/AddressSlice';

export default function EditPartial(props: ItemPartialViewProps<IAddressDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { crudViewContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

    const { register, control, handleSubmit, reset, formState: { isValid, errors } } = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: defaultAddress(),
    },);

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    useEffect(() => {

        reset(item);
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
                        {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Inline) && <>
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
                        </>}
                        {(crudViewContainer === CrudViewContainers.StandaloneView) && <>
                            <LoadingButton
                                color="primary"
                                type='submit'
                                variant='contained'
                                disabled={!isValid || saving || saved}
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
                        </>}
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
                	defaultValue={item.addressLine1}
                    variant='outlined'
                    margin='normal'
                    {...register("addressLine1", addressFormValidationWhenEdit.addressLine1)}
                    autoComplete='addressLine1'
                    error={!!errors.addressLine1}
                    fullWidth
                    autoFocus
                    helperText={!!errors.addressLine1 ? t(errors.addressLine1.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.addressLine2 ? t(errors.addressLine2.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.city ? t(errors.city.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.stateProvince ? t(errors.stateProvince.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.countryRegion ? t(errors.countryRegion.message) : ''}
                />
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
                    autoFocus
                    helperText={!!errors.postalCode ? t(errors.postalCode.message) : ''}
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
                                autoFocus
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


