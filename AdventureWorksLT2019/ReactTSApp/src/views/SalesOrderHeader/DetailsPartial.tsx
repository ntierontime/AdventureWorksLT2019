import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, FormControlLabel, Grid, IconButton, Stack, TextField, Typography, useTheme } from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import CloseIcon from '@mui/icons-material/Close';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import MoreVertIcon from '@mui/icons-material/MoreVert';

import { useNavigate } from "react-router-dom";
import { useTranslation } from 'react-i18next';

import { DatePicker } from '@mui/x-date-pickers';
import { Link } from 'react-router-dom';


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getSalesOrderHeaderAvatar, ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { getRouteParamsOfISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';

export default function DetailsPartial(props: ItemPartialViewProps<ISalesOrderHeaderDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getSalesOrderHeaderAvatar(item);
    const avatarStyle = getAvatarStyle(item.itemUIStatus______, theme);


    const renderButtonGroup_IconButtons = () => {
        return (
            <>
				{(!!previousAction || !!nextAction) && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                >
                    {!!previousAction && <Button
                        color="secondary"
                        variant='outlined'
                        startIcon={<NavigateBeforeIcon />}
                        onClick={() => { previousAction() }}
                    />}
                    {!!nextAction && <Button
                        color="secondary"
                        variant='outlined'
                        endIcon={<NavigateNextIcon />}
                        onClick={() => { nextAction() }}
                    />}
                </ButtonGroup>}
                {!!handleSelectItemClick && <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                ><Checkbox
                    color="primary"
                    checked={isItemSelected}
                    onChange={() => { handleSelectItemClick(item) }}
                /></ButtonGroup>}
                <ButtonGroup
                    disableElevation
                    variant="contained"
                    aria-label="navigation buttons"
                    >
                    {(crudViewContainer !== CrudViewContainers.Card) &&
                        <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/salesOrderHeader/dashboard/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                            <AccountTreeIcon />
                        </IconButton>
                    }
                    {(crudViewContainer === CrudViewContainers.Inline) &&
                        <>
                            <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, null) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, null) }}>
                                <EditIcon />
                            </IconButton>
                        </>
                    }
                    {(crudViewContainer === CrudViewContainers.StandaloneView) &&
                        <>
                            <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/salesOrderHeader/delete/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/salesOrderHeader/edit/" + getRouteParamsOfISalesOrderHeaderIdentifier(item)) }}>
                                <EditIcon />
                            </IconButton>
                        </>
                    }
                    {(crudViewContainer === CrudViewContainers.Dialog || crudViewContainer === CrudViewContainers.Card) &&
                        <>
                            <IconButton aria-label="delete" color="primary" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Delete) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { changeViewItemTemplate(ViewItemTemplates.Edit) }}>
                                <EditIcon />
                            </IconButton>
                        </>
                    }
                </ButtonGroup>
                <ButtonGroup>
                    {!!doneAction && (crudViewContainer === CrudViewContainers.Dialog) && <IconButton aria-label="close" onClick={() => { doneAction() }}>
                        <CloseIcon />
                    </IconButton>}
                    {!!doneAction && (crudViewContainer === CrudViewContainers.Card) && <IconButton aria-label="close" onClick={() => { doneAction() }}>
                        <ArrowBackIcon />
                    </IconButton>}
                    {!!doneAction && (crudViewContainer === CrudViewContainers.StandaloneView) && <Button
                        color="secondary"
                        autoFocus
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
        <Card sx={{ minHeight: '100%' }}>
            <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.salesOrderNumber}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && renderButtonGroup_IconButtons()}
                title={item.salesOrderNumber}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                label={t('OrderDate')}
                                value={t(i18nFormats.dateTime.format, { val: new Date(item.orderDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='orderDate'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                label={t('DueDate')}
                                value={t(i18nFormats.dateTime.format, { val: new Date(item.dueDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='dueDate'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                label={t('ShipDate')}
                                value={t(i18nFormats.dateTime.format, { val: new Date(item.shipDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='shipDate'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <FormControlLabel control={<Checkbox checked={item.onlineOrderFlag} readOnly />} label={t('OnlineOrderFlag')} />
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("CustomerID")}</Typography>
                                <Link to={"/customer/Details/" + item.customerID}>{item.customer_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("ShipToAddressID")}</Typography>
                                <Link to={"/address/Details/" + item.shipToAddressID}>{item.shipTo_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("BillToAddressID")}</Typography>
                                <Link to={"/address/Details/" + item.billToAddressID}>{item.billTo_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                        </Grid>
                        <Grid item {...gridColumns}>
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
                            <DatePicker
                                label={t('ModifiedDate')}
                                value={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='modifiedDate'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                    </Grid>
				</Box>
            </CardContent>
            {/* {buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>} */}
        </Card >
    );
}

