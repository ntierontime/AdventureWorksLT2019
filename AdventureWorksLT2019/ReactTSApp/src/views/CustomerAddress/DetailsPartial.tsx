import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, Link, Stack, TextField, Typography, useTheme } from '@mui/material';
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


// 1. DateTime/Integer/Decimal fields are using 'i18nFormats.??' when display
// 2. un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { ContainerOptions } from 'src/shared/viewModels/ContainerOptions';
import { CrudViewContainers } from 'src/shared/viewModels/CrudViewContainers';
import { ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import { getAvatarStyle } from 'src/shared/views/ThemeRelated';

import { getCustomerAddressAvatar, ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { getRouteParamsOfICustomerAddressIdentifier } from 'src/dataModels/ICustomerAddressQueries';

export default function DetailsPartial(props: ItemPartialViewProps<ICustomerAddressDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getCustomerAddressAvatar(item);
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
                        <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/customerAddress/dashboard/" + getRouteParamsOfICustomerAddressIdentifier(item)) }}>
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
                            <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/customerAddress/delete/" + getRouteParamsOfICustomerAddressIdentifier(item)) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/customerAddress/edit/" + getRouteParamsOfICustomerAddressIdentifier(item)) }}>
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
            {crudViewContainer !== CrudViewContainers.Wizard && <CardHeader
                avatar={
                    <Avatar sx={avatarStyle} aria-label={item.addressType}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && renderButtonGroup_IconButtons()}
                title={item.addressType}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.modifiedDate), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />}
            {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={1}>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("CustomerID")}</Typography>
                                <Link href={"/customer/Details/" + item.customerID}>{item.customer_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <Stack sx={{ p: 2 }}
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                spacing={2}
                            >
                                <Typography>{t("AddressID")}</Typography>
                                <Link href={"/address/Details/" + item.addressID}>{item.address_Name}</Link>
                            </Stack>
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='addressType'
                                label={t('AddressType')}
                                defaultValue={item.addressType}
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
                            			sx={{marginTop: 2}}
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
            {/* {crudViewContainer !== CrudViewContainers.Wizard && buttonContainer === ContainerOptions.ItemCardBottom && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>} */}
        </Card >
    );
}

