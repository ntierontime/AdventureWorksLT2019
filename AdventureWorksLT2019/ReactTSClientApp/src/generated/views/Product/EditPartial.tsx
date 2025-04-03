import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, Dialog, FormControlLabel, FormLabel, Grid, IconButton, MenuItem, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { DatePicker } from '@mui/x-date-pickers';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';
import dayjs from 'dayjs';
import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
import { defaultIProductModelAdvancedQuery } from 'src/dataModels/IProductModelQueries';
// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';

import { card100PercentHeighFlex, ItemCardProps } from 'src/shared/views/ItemCardProps';
import { getAvatar, getAvatarStyle } from 'src/shared/avatarUtility';
import { getTitle } from 'src/shared/captionTextUtility';
import { EditItemActionIconButtonGroup, EditItemActionIconAndTextButtonGroup } from 'src/shared/views/buttonGroups/EditItemActionButtonGroups';
import { ButtonTypes } from 'src/shared/views/buttonGroups/ButtonTypes';
import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { RatingSummary } from 'src/shared/views/rating/RatingSummary';

import { IProductDataModel, productFormValidation } from 'src/dataModels/IProductDataModel';


export default function EditPartial(props: ItemCardProps<IProductDataModel>): JSX.Element {
    const { t } = useTranslation();

    const {
        mainButtonContainer, mainButtonType,
        gridColumns, scrollableCardContent,
        item, itemIndex,
        submitAction, submitting, submitted, submitMessage,
        doneAction, previousAction, nextAction,
        showCloseIconOnTopRight, showCardHeader,
        isItemSelected, handleSelectItemClick,
    } = props;

	// 'control' is only used by boolean fields, you can remove it if this form doesn't have it
	// 'setValue' is only used by Dropdown List fields and DatePicker fields, you can remove it if this form doesn't have it
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: item,
        resolver: yupResolver(productFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;




    const [productCategory_ProductCategoryIDCodeList, setProductCategory_ProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productCategory_Name, value: item.productCategoryID, selected: false }]);

    const [productModel_ProductModelIDCodeList, setProductModel_ProductModelIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.productModel_Name, value: item.productModelID, selected: false }]);
    const [sellStartDate, setSellStartDate] = useState<string>();
    const [sellEndDate, setSellEndDate] = useState<string>();
    const [discontinuedDate, setDiscontinuedDate] = useState<string>();
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ProductCategoryIDCodeList(res.responseBody);
            }
        });

        codeListsApi.getProductModelCodeList({ ...defaultIProductModelAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductModel_ProductModelIDCodeList(res.responseBody);
            }
        });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: IProductDataModel) => {
		const dataWithProperDateTimeFormat = {...data, sellStartDate: dayjs(sellStartDate).toString(), sellEndDate: dayjs(sellEndDate).toString(), discontinuedDate: dayjs(discontinuedDate).toString(), modifiedDate: dayjs(modifiedDate).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const name_Watched = watch("name");
    const sellStartDate_Watched = watch("sellStartDate");
    const avatar = getAvatar([name_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([name_Watched]);
    const subheader = !!sellStartDate_Watched
        ? t(i18nFormats.dateTime.format, { val: new Date(sellStartDate_Watched), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;


    const renderMainButtonGroup = () => {
        const extraButtonGroups = (
            <>

            </>
        );

        if (mainButtonType === ButtonTypes.IconAndText) {
            return (
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ProductEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ProductEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="ProductEditForm" sx={{ ...card100PercentHeighFlex }} >
            {showCardHeader && <CardHeader
                avatar={
                    <Avatar
                        sx={avatarStyle}>
                            {avatar}
                    </Avatar>
                }
                action={
                    <>
                        {mainButtonContainer === CardButtonGroupPosition.CardHeader && <>
                            {renderMainButtonGroup()}
                        </>}
                        {(showCloseIconOnTopRight && !!doneAction) && <IconButton aria-label="edit" color="primary" onClick={() => { doneAction() }}>
                            <CloseIcon />
                        </IconButton>}
                    </>}
                title={title}
                subheader={subheader}
            />}
            {mainButtonContainer === CardButtonGroupPosition.BelowCardHeader && <CardActions disableSpacing>
                {renderMainButtonGroup()}
            </CardActions>}
            {!!submitMessage && <CardContent sx={{ paddingBottom: 0, paddingTop: 0 }}>
                <Typography variant="body1" component="span">
                    {submitMessage + " "}
                    <Typography variant="h6" component="span">
                        {item.name}
                    </Typography>
                </Typography>
            </CardContent>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
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
                        {...register("name")}
                        autoComplete='name'
                        error={!!errors.name}
                        fullWidth
                        helperText={!!errors.name ? t(errors.name.message) : ''}
                    />
                    <TextField
                        name='productNumber'
                        label={t('ProductNumber')}
                        defaultValue={item.productNumber}
                        variant='outlined'
                        margin='normal'
                        {...register("productNumber")}
                        autoComplete='productNumber'
                        error={!!errors.productNumber}
                        fullWidth
                        helperText={!!errors.productNumber ? t(errors.productNumber.message) : ''}
                    />
                    <TextField
                        name='color'
                        label={t('Color')}
                        defaultValue={item.color}
                        variant='outlined'
                        margin='normal'
                        {...register("color")}
                        autoComplete='color'
                        error={!!errors.color}
                        fullWidth
                        helperText={!!errors.color ? t(errors.color.message) : ''}
                    />
                    <TextField
                        name='standardCost'
                        label={t('StandardCost')}
                        defaultValue={item.standardCost}
                        variant='outlined'
                        margin='normal'
                        {...register("standardCost")}
                        autoComplete='standardCost'
                        error={!!errors.standardCost}
                        fullWidth
                        helperText={!!errors.standardCost ? t(errors.standardCost.message) : ''}
                    />
                    <TextField
                        name='listPrice'
                        label={t('ListPrice')}
                        defaultValue={item.listPrice}
                        variant='outlined'
                        margin='normal'
                        {...register("listPrice")}
                        autoComplete='listPrice'
                        error={!!errors.listPrice}
                        fullWidth
                        helperText={!!errors.listPrice ? t(errors.listPrice.message) : ''}
                    />
                    <TextField
                        name='size'
                        label={t('Size')}
                        defaultValue={item.size}
                        variant='outlined'
                        margin='normal'
                        {...register("size")}
                        autoComplete='size'
                        error={!!errors.size}
                        fullWidth
                        helperText={!!errors.size ? t(errors.size.message) : ''}
                    />
                    <TextField
                        name='weight'
                        label={t('Weight')}
                        defaultValue={item.weight}
                        variant='outlined'
                        margin='normal'
                        {...register("weight")}
                        autoComplete='weight'
                        error={!!errors.weight}
                        fullWidth
                        //helperText={!!errors.weight ? t(errors.weight.message) : ''}
                    />
                    <TextField
                    	sx={{marginTop: 2}}
                        label={t("ProductCategoryID")}
                        id="productCategoryIDSelect"
                        select
                        name='productCategoryID'
                        {...register("productCategoryID")}
                        autoComplete='productCategoryID'
                        variant="outlined"
                        fullWidth
                        defaultValue={item.productCategoryID}
                    >
                        {productCategory_ProductCategoryIDCodeList && productCategory_ProductCategoryIDCodeList.map((v, index) => {
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
                    <TextField
                    	sx={{marginTop: 2}}
                        label={t("ProductModelID")}
                        id="productModelIDSelect"
                        select
                        name='productModelID'
                        {...register("productModelID")}
                        autoComplete='productModelID'
                        variant="outlined"
                        fullWidth
                        defaultValue={item.productModelID}
                    >
                        {productModel_ProductModelIDCodeList && productModel_ProductModelIDCodeList.map((v, index) => {
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={sellStartDate}
                        // value={sellStartDate}
                        label={t('SellStartDate')}
                        onChange={(event: string) => { setSellStartDate(event); setValue('sellStartDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={sellEndDate}
                        // value={sellEndDate}
                        label={t('SellEndDate')}
                        onChange={(event: string) => { setSellEndDate(event); setValue('sellEndDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <DatePicker
                        sx={{ minWidth: "100%" }}
                        defaultValue={discontinuedDate}
                        // value={discontinuedDate}
                        label={t('DiscontinuedDate')}
                        onChange={(event: string) => { setDiscontinuedDate(event); setValue('discontinuedDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                    <TextField
                        name='thumbNailPhoto'
                        label={t('ThumbNailPhoto')}
                        defaultValue={item.thumbNailPhoto}
                        variant='outlined'
                        margin='normal'
                        {...register("thumbNailPhoto")}
                        autoComplete='thumbNailPhoto'
                        error={!!errors.thumbNailPhoto}
                        fullWidth
                        //helperText={!!errors.thumbNailPhoto ? t(errors.thumbNailPhoto.message) : ''}
                    />
                    <TextField
                        name='thumbnailPhotoFileName'
                        label={t('ThumbnailPhotoFileName')}
                        defaultValue={item.thumbnailPhotoFileName}
                        variant='outlined'
                        margin='normal'
                        {...register("thumbnailPhotoFileName")}
                        autoComplete='thumbnailPhotoFileName'
                        error={!!errors.thumbnailPhotoFileName}
                        fullWidth
                        helperText={!!errors.thumbnailPhotoFileName ? t(errors.thumbnailPhotoFileName.message) : ''}
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
                        sx={{ minWidth: "100%" }}
                        defaultValue={modifiedDate}
                        // value={modifiedDate}
                        label={t('ModifiedDate')}
                        onChange={(event: string) => { setModifiedDate(event); setValue('modifiedDate', event, { shouldValidate: false, shouldDirty: true }); }}
                        slotProps={{
                            textField: {
                                sx: { minWidth: "100%", marginTop: 1 },
                            },
                        }}
                    />
                </Box>
            </CardContent>
            {mainButtonContainer === CardButtonGroupPosition.Bottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderMainButtonGroup()}
            </CardActions>}
        </Card >
    );
}

