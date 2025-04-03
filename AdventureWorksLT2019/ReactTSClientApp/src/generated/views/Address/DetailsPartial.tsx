import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Chip, FormControlLabel, FormLabel, Grid, IconButton, Stack, TextField, ToggleButton, ToggleButtonGroup, Typography, useTheme } from '@mui/material';
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

import { getAddressAvatar, getAddressTitle, IAddressDataModel } from 'src/dataModels/IAddressDataModel';
import { getRouteParamsOfIAddressIdentifier } from 'src/dataModels/IAddressQueries';

export default function DetailsPartial(props: ItemCardProps<IAddressDataModel>): JSX.Element {
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
    const avatar = getAddressAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getAddressTitle(item);

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
                    <TextField
                        name='addressLine1'
                        label={t('AddressLine1')}
                        defaultValue={item.addressLine1}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='addressLine2'
                        label={t('AddressLine2')}
                        defaultValue={item.addressLine2}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='city'
                        label={t('City')}
                        defaultValue={item.city}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='stateProvince'
                        label={t('StateProvince')}
                        defaultValue={item.stateProvince}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='countryRegion'
                        label={t('CountryRegion')}
                        defaultValue={item.countryRegion}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='postalCode'
                        label={t('PostalCode')}
                        defaultValue={item.postalCode}
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

