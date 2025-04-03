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

import { getProductCategoryAvatar, getProductCategoryTitle, IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { getRouteParamsOfIProductCategoryIdentifier } from 'src/dataModels/IProductCategoryQueries';

export default function DetailsPartial(props: ItemCardProps<IProductCategoryDataModel>): JSX.Element {
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
    const avatar = getProductCategoryAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getProductCategoryTitle(item);

    const subheader = !!item?.modifiedDate
        ? t(i18nFormats.dateTime.format, { val: new Date(item?.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
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
                    <Stack sx={{ p: 2 }}
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                    >
                        <Typography>{t("ParentProductCategoryID")}</Typography>
                        <Link href={"/productCategory/Details/" + item.parentProductCategoryID}>{item.parent_Name}</Link>
                    </Stack>
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

