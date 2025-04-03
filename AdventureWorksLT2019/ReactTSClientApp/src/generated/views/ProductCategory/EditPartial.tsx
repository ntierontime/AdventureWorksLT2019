import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, Dialog, FormControlLabel, FormLabel, Grid, IconButton, MenuItem, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { yupResolver } from '@hookform/resolvers/yup';
import dayjs from 'dayjs';
import { Controller } from 'react-hook-form';
import { INameValuePair } from 'src/shared/dataModels/INameValuePair';
import { codeListsApi } from 'src/apiClients/CodeListsApi';
import { defaultIProductCategoryAdvancedQuery } from 'src/dataModels/IProductCategoryQueries';
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

import { IProductCategoryDataModel, productCategoryFormValidation } from 'src/dataModels/IProductCategoryDataModel';


export default function EditPartial(props: ItemCardProps<IProductCategoryDataModel>): JSX.Element {
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
        resolver: yupResolver(productCategoryFormValidation)
    });
    const { register, control, setValue, handleSubmit, reset, trigger, watch, getValues, formState: { isValid, errors, isDirty } } = methods;




    const [productCategory_ParentProductCategoryIDCodeList, setProductCategory_ParentProductCategoryIDCodeList] = useState<readonly INameValuePair[]>([{ name: item.parent_Name, value: item.parentProductCategoryID, selected: false }]);
    const [modifiedDate, setModifiedDate] = useState<string>();
    useEffect(() => {


        codeListsApi.getProductCategoryCodeList({ ...defaultIProductCategoryAdvancedQuery(), pageSize: 10000 }).then((res) => {
            if (res.status === "OK") {
                setProductCategory_ParentProductCategoryIDCodeList(res.responseBody);
            }
        });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);








    const onSubmit = (data: IProductCategoryDataModel) => {
		const dataWithProperDateTimeFormat = {...data, modifiedDate: dayjs(modifiedDate).toString()};
        if (!!submitAction) {
            submitAction(dataWithProperDateTimeFormat, itemIndex);
            // console.log("execute Submit", dataWithProperDateTimeFormat);
        }
    }

    const theme = useTheme();
    const name_Watched = watch("name");
    const modifiedDate_Watched = watch("modifiedDate");
    const avatar = getAvatar([name_Watched]);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getTitle([name_Watched]);
    const subheader = !!modifiedDate_Watched
        ? t(i18nFormats.dateTime.format, { val: new Date(modifiedDate_Watched), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;


    const renderMainButtonGroup = () => {
        const extraButtonGroups = (
            <>

            </>
        );

        if (mainButtonType === ButtonTypes.IconAndText) {
            return (
                <EditItemActionIconAndTextButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ProductCategoryEditForm" />
            );
        }

        // Default to Icon only buttons

        return (
            <EditItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} submitted={submitted} submitting={submitting} isValid={isValid} extraButtonGroups={extraButtonGroups} formId="ProductCategoryEditForm" />
        );
    }

    return (
        <Card component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }}  id="ProductCategoryEditForm" sx={{ ...card100PercentHeighFlex }} >
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
                    <TextField
                    	sx={{marginTop: 2}}
                        label={t("ParentProductCategoryID")}
                        id="parentProductCategoryIDSelect"
                        select
                        name='parentProductCategoryID'
                        {...register("parentProductCategoryID")}
                        autoComplete='parentProductCategoryID'
                        variant="outlined"
                        fullWidth
                        defaultValue={item.parentProductCategoryID}
                    >
                        {productCategory_ParentProductCategoryIDCodeList && productCategory_ParentProductCategoryIDCodeList.map((v, index) => {
                            return (<MenuItem key={v.value} value={v.value}>{t(v.name)}</MenuItem>)
                        })}
                    </TextField>
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

