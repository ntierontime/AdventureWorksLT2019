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

import { getCustomerAvatar, getCustomerTitle, ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { getRouteParamsOfICustomerIdentifier } from 'src/dataModels/ICustomerQueries';

export default function DetailsPartial(props: ItemCardProps<ICustomerDataModel>): JSX.Element {
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
    const avatar = getCustomerAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getCustomerTitle(item);

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
                        name='customerID'
                        label={t('CustomerID')}
                        value={item.customerID}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <FormControlLabel control={<Checkbox checked={item.nameStyle} readOnly />} label={t('NameStyle')} />
                    <TextField
                        name='title'
                        label={t('Title')}
                        defaultValue={item.title}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='firstName'
                        label={t('FirstName')}
                        defaultValue={item.firstName}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='middleName'
                        label={t('MiddleName')}
                        defaultValue={item.middleName}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='lastName'
                        label={t('LastName')}
                        defaultValue={item.lastName}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='suffix'
                        label={t('Suffix')}
                        defaultValue={item.suffix}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='companyName'
                        label={t('CompanyName')}
                        defaultValue={item.companyName}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='salesPerson'
                        label={t('SalesPerson')}
                        defaultValue={item.salesPerson}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='emailAddress'
                        label={t('EmailAddress')}
                        defaultValue={item.emailAddress}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='phone'
                        label={t('Phone')}
                        defaultValue={item.phone}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='passwordHash'
                        label={t('PasswordHash')}
                        defaultValue={item.passwordHash}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='passwordSalt'
                        label={t('PasswordSalt')}
                        defaultValue={item.passwordSalt}
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

