import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, FormControlLabel, FormLabel, Grid, IconButton, Link, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
// import AccountTreeIcon from '@mui/icons-material/AccountTree';
// import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
// import NavigateNextIcon from '@mui/icons-material/NavigateNext';
// import ArrowBackIcon from '@mui/icons-material/ArrowBack';
// import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import CloseIcon from '@mui/icons-material/Close';
// import DeleteIcon from '@mui/icons-material/Delete';
// import EditIcon from '@mui/icons-material/Edit';
// import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from "react-router-dom";
import { useTranslation } from 'react-i18next';
import dayjs from 'dayjs';

import { DatePicker } from '@mui/x-date-pickers';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { card100PercentHeighFlex, ItemCardProps } from 'src/shared/views/ItemCardProps';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';
import { DetailsItemActionIconButtonGroup } from 'src/shared/views/buttonGroups/DetailsItemActionButtonGroups';
import { CardButtonGroupPosition } from 'src/shared/views/buttonGroups/CardButtonGroupPosition';
import { RatingSummary } from 'src/shared/views/rating/RatingSummary';

import { getProductAvatar, getProductTitle, IProductDataModel } from 'src/dataModels/IProductDataModel';
import { getRouteParamsOfIProductIdentifier } from 'src/dataModels/IProductQueries';

export default function DetailsPartial(props: ItemCardProps<IProductDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { 
        extraButtonGroups,
        mainButtonContainer, mainButtonType,
        gridColumns, scrollableCardContent,
        item, itemIndex,
        doneAction, previousAction, nextAction,
        showCloseIconOnTopRight, showCardHeader, showCardContent,
        isItemSelected, handleSelectItemClick,
        } = props;

    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getProductAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getProductTitle(item);

    const subheader = !!item?.sellStartDate
        ? t(i18nFormats.dateTime.format, { val: new Date(item?.sellStartDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
        : null;

    const renderMainButtonGroup = () => {
        return (
            <DetailsItemActionIconButtonGroup previousAction={previousAction} nextAction={nextAction} doneAction={doneAction} extraButtonGroups={extraButtonGroups} />
        );
    }

    return (
        <Card sx={{ ...card100PercentHeighFlex }} >
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
            {showCardContent && <CardContent>
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
                        <Link href={"/productCategory/Details/" + item.productCategoryID}>{item.productCategory_Name}</Link>
                    </Stack>
                    <Stack sx={{ p: 2 }}
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                    >
                        <Typography>{t("ProductModelID")}</Typography>
                        <Link href={"/productModel/Details/" + item.productModelID}>{item.productModel_Name}</Link>
                    </Stack>
                    <DatePicker
                    	readOnly={true}
                        label={t('SellStartDate')}
                        defaultValue={dayjs(item.sellStartDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
                    <DatePicker
                    	readOnly={true}
                        label={t('SellEndDate')}
                        defaultValue={dayjs(item.sellEndDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
                    <DatePicker
                    	readOnly={true}
                        label={t('DiscontinuedDate')}
                        defaultValue={dayjs(item.discontinuedDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
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
                    	readOnly={true}
                        label={t('ModifiedDate')}
                        defaultValue={dayjs(item.modifiedDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
				</Box>
            </CardContent>}
            {mainButtonContainer === CardButtonGroupPosition.Bottom && <CardActions disableSpacing sx={{ mt: "auto" }}>
                {renderMainButtonGroup()}
            </CardActions>}
        </Card >
    );
}

