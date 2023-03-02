import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, Typography, useTheme } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';
import SaveIcon from '@mui/icons-material/Save';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';

import { Controller } from 'react-hook-form';



// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { AppDispatch } from 'src/store/Store';

import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getProductModelAvatar, IProductModelDataModel, productModelFormValidationWhenEdit } from 'src/dataModels/IProductModelDataModel';
import { getIProductModelIdentifier } from 'src/dataModels/IProductModelQueries';
import { put } from 'src/slices/ProductModelSlice';

export default function EditPartial(props: ItemPartialViewProps<IProductModelDataModel>): JSX.Element {
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, isItemSelected, handleSelectItemClick, changeViewItemTemplate } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();
    const dispatch = useDispatch<AppDispatch>();

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(productModelFormValidationWhenEdit)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;

    const [saving, setSaving] = useState(false);
    const [saved, setSaved] = useState(false);

    const [saveMessage, setSaveMessage] = useState<string>();


    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {

        setSaving(false);
        setSaved(false);
        setSaveMessage(null);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);






    const onSubmit = (data: IProductModelDataModel) => {
        setSaving(true);
        dispatch(put({ identifier: getIProductModelIdentifier(data), data: { ...data } }))
            .then((result: any) => {
                if (!!result && !!result.meta && result.meta.requestStatus === 'fulfilled') { // success
                    setSaveMessage(t('SuccessfullySaved'));
                    setSaved(true);
                }
                else { // failed
                    setSaveMessage(t('FailedToSave'));
                }
                //console.log(result);
            })
            .catch((error: any) => { setSaveMessage(t('FailedToSave')); /*console.log(error);*/ })
            .finally(() => { setSaving(false); console.log('finally'); });
    }

    const theme = useTheme();
    const avatar = getProductModelAvatar(item);
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
                <IconButton aria-label="Save"
                    color="primary"
                    type='submit'
                    disabled={!isValid || saving || saved}
                >
                    <SaveIcon />
                </IconButton>
                {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
                    <CloseIcon />
                </IconButton>}
                {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <IconButton aria-label="close" onClick={() => { doneAction() }} disabled={saving}>
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
                    {!!doneAction && crudViewContainer !== CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
                        variant='contained'
                        startIcon={<CloseIcon />}
                        onClick={() => { doneAction() }}
                    >
                        {t('Cancel')}
                    </Button>}
                    {!!doneAction && crudViewContainer === CrudViewContainers.StandaloneView && <Button
                        color="secondary"
                        autoFocus
                        disabled={saving}
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
        <Card component="form" noValidate onSubmit={handleSubmit(onSubmit)}>
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.name}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && <>
                    {crudViewContainer !== CrudViewContainers.StandaloneView && renderButtonGroup_IconButtons()}
                    {crudViewContainer === CrudViewContainers.StandaloneView && renderButtonGroup_TextAndIconButtons()}
                </>}
                title={item.name}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            {crudViewContainer !== CrudViewContainers.Wizard && !!saveMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {saveMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.name}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='productModelID'
                                label={t('ProductModelID')}
                                value={item.productModelID}
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
                                name='name'
                                label={t('Name')}
                                defaultValue={item.name}
                                variant='outlined'
                                margin='normal'
                                {...register("name")}
                                autoComplete='name'
                                error={!!errors.name}
                                fullWidth
                                helperText={!!errors.name ? t(errors.name.message) : ''}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='catalogDescription'
                                label={t('CatalogDescription')}
                                defaultValue={item.catalogDescription}
                                variant='outlined'
                                margin='normal'
                                {...register("catalogDescription")}
                                autoComplete='catalogDescription'
                                error={!!errors.catalogDescription}
                                fullWidth
                                //helperText={!!errors.catalogDescription ? t(errors.catalogDescription.message) : ''}
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
                                control={control}
                                defaultValue={null}
                                render={({ field, ...props }) => {
                                    return (
                            			<DatePicker
                            				value={modifiedDate}
                            				label={t('ModifiedDate')}
                            				onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldDirty: true }); }}
                            				renderInput={(params) =>
                            					<TextField
                            						sx={{marginTop: 2}}
                            						fullWidth
                            						error={!!errors.modifiedDate}
                            						helperText={!!errors.modifiedDate ? t(errors.modifiedDate.message) : ''}
                            						{...params}
                            					/>}
                            			/>
                                    );
                                }}
                            />
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_TextAndIconButtons()}
            </CardActions>}
        </Card >
    );
}

