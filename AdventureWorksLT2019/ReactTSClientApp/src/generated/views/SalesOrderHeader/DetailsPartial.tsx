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

import { getSalesOrderHeaderAvatar, getSalesOrderHeaderTitle, ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { getRouteParamsOfISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';

export default function DetailsPartial(props: ItemCardProps<ISalesOrderHeaderDataModel>): JSX.Element {
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
    const avatar = getSalesOrderHeaderAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);
    const title = getSalesOrderHeaderTitle(item);

    const subheader = !!item?.orderDate
        ? t(i18nFormats.dateTime.format, { val: new Date(item?.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })
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
                        name='salesOrderID'
                        label={t('SalesOrderID')}
                        value={item.salesOrderID}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='revisionNumber'
                        label={t('RevisionNumber')}
                        value={t(i18nFormats.number.format, { val: item.revisionNumber })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <DatePicker
                    	readOnly={true}
                        label={t('OrderDate')}
                        defaultValue={dayjs(item.orderDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
                    <DatePicker
                    	readOnly={true}
                        label={t('DueDate')}
                        defaultValue={dayjs(item.dueDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
                    <DatePicker
                    	readOnly={true}
                        label={t('ShipDate')}
                        defaultValue={dayjs(item.shipDate)}
                        slotProps={{ textField: { sx: { minWidth: "100%", marginTop: 1 } } }}
                    />
                    <TextField
                        name='status'
                        label={t('Status')}
                        value={t(i18nFormats.number.format, { val: item.status })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <FormControlLabel control={<Checkbox checked={item.onlineOrderFlag} readOnly />} label={t('OnlineOrderFlag')} />
                    <TextField
                        name='salesOrderNumber'
                        label={t('SalesOrderNumber')}
                        defaultValue={item.salesOrderNumber}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='purchaseOrderNumber'
                        label={t('PurchaseOrderNumber')}
                        defaultValue={item.purchaseOrderNumber}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='accountNumber'
                        label={t('AccountNumber')}
                        defaultValue={item.accountNumber}
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
                        <Typography>{t("CustomerID")}</Typography>
                        <Link href={"/customer/Details/" + item.customerID}>{item.customer_Name}</Link>
                    </Stack>
                    <Stack sx={{ p: 2 }}
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                    >
                        <Typography>{t("ShipToAddressID")}</Typography>
                        <Link href={"/address/Details/" + item.shipToAddressID}>{item.shipTo_Name}</Link>
                    </Stack>
                    <Stack sx={{ p: 2 }}
                        direction="row"
                        justifyContent="space-between"
                        alignItems="center"
                        spacing={2}
                    >
                        <Typography>{t("BillToAddressID")}</Typography>
                        <Link href={"/address/Details/" + item.billToAddressID}>{item.billTo_Name}</Link>
                    </Stack>
                    <TextField
                        name='shipMethod'
                        label={t('ShipMethod')}
                        defaultValue={item.shipMethod}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='creditCardApprovalCode'
                        label={t('CreditCardApprovalCode')}
                        defaultValue={item.creditCardApprovalCode}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='subTotal'
                        label={t('SubTotal')}
                        value={t(i18nFormats.double.format, { val: item.subTotal })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='taxAmt'
                        label={t('TaxAmt')}
                        value={t(i18nFormats.double.format, { val: item.taxAmt })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='freight'
                        label={t('Freight')}
                        value={t(i18nFormats.double.format, { val: item.freight })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='totalDue'
                        label={t('TotalDue')}
                        value={t(i18nFormats.double.format, { val: item.totalDue })}
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        InputProps={{
                            readOnly: true
                        }}
                    />
                    <TextField
                        name='comment'
                        label={t('Comment')}
                        defaultValue={item.comment}
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

