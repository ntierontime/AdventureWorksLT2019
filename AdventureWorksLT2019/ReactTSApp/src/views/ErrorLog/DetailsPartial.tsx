import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, Checkbox, Grid, IconButton, TextField, useTheme } from '@mui/material';
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

import { getErrorLogAvatar, IErrorLogDataModel } from 'src/dataModels/IErrorLogDataModel';
import { getRouteParamsOfIErrorLogIdentifier } from 'src/dataModels/IErrorLogQueries';

export default function DetailsPartial(props: ItemPartialViewProps<IErrorLogDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getErrorLogAvatar(item);
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
                        <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/errorLog/dashboard/" + getRouteParamsOfIErrorLogIdentifier(item)) }}>
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
                            <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/errorLog/delete/" + getRouteParamsOfIErrorLogIdentifier(item)) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/errorLog/edit/" + getRouteParamsOfIErrorLogIdentifier(item)) }}>
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
                    <Avatar sx={avatarStyle} aria-label={item.userName}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && renderButtonGroup_IconButtons()}
                title={item.userName}
                subheader={t(i18nFormats.dateTime.format, { val: new Date(item.errorTime), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='errorLogID'
                                label={t('ErrorLogID')}
                                value={item.errorLogID}
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
                                label={t('ErrorTime')}
                                value={t(i18nFormats.dateTime.format, { val: new Date(item.errorTime), formatParams: { val: i18nFormats.dateTime.dateTimeShort, } })}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='errorTime'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='userName'
                                label={t('UserName')}
                                defaultValue={item.userName}
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
                                name='errorNumber'
                                label={t('ErrorNumber')}
                                value={t(i18nFormats.number.format, { val: item.errorNumber })}
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
                                name='errorSeverity'
                                label={t('ErrorSeverity')}
                                value={t(i18nFormats.number.format, { val: item.errorSeverity })}
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
                                name='errorState'
                                label={t('ErrorState')}
                                value={t(i18nFormats.number.format, { val: item.errorState })}
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
                                name='errorProcedure'
                                label={t('ErrorProcedure')}
                                defaultValue={item.errorProcedure}
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
                                name='errorLine'
                                label={t('ErrorLine')}
                                value={t(i18nFormats.number.format, { val: item.errorLine })}
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
                                name='errorMessage'
                                label={t('ErrorMessage')}
                                defaultValue={item.errorMessage}
                                variant='outlined'
                                margin='normal'
                                fullWidth
                                InputProps={{
                                    readOnly: true
                                }}
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

