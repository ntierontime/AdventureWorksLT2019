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

import { getBuildVersionAvatar, IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { getRouteParamsOfIBuildVersionIdentifier } from 'src/dataModels/IBuildVersionQueries';

export default function DetailsPartial(props: ItemPartialViewProps<IBuildVersionDataModel>): JSX.Element {
    const navigate = useNavigate();
    const { gridColumns, scrollableCardContent, crudViewContainer, buttonContainer, item, itemIndex, isItemSelected, handleSelectItemClick, changeViewItemTemplate, handleItemDialogOpen } = props; // item
    const { doneAction, previousAction, nextAction } = props; // dialog
    const { t } = useTranslation();

    const theme = useTheme();
    const avatar = getBuildVersionAvatar(item);
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
                        <IconButton aria-label="dashboard" color="primary" onClick={() => { navigate("/buildVersion/dashboard/" + getRouteParamsOfIBuildVersionIdentifier(item)) }}>
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
                            <IconButton aria-label="delete" color="primary" onClick={() => { navigate("/buildVersion/delete/" + getRouteParamsOfIBuildVersionIdentifier(item)) }}>
                                <DeleteIcon />
                            </IconButton>
                            <IconButton aria-label="edit" color="primary" onClick={() => { navigate("/buildVersion/edit/" + getRouteParamsOfIBuildVersionIdentifier(item)) }}>
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
                    <Avatar sx={avatarStyle} aria-label={item.database_Version}>
                        {avatar}
                    </Avatar>
                }
                action={buttonContainer === ContainerOptions.ItemCardHead && renderButtonGroup_IconButtons()}
                title={item.database_Version}
                subheader={item.versionDate}
            />
            {buttonContainer === ContainerOptions.ItemCardToolbar && <CardActions disableSpacing>
                {renderButtonGroup_IconButtons()}
            </CardActions>}
            <CardContent>
                <Box sx={{ ...scrollableCardContent }}>
                    <Grid container spacing={2}>
                        <Grid item {...gridColumns}>
                            <TextField
                                name='systemInformationID'
                                label={t('SystemInformationID')}
                                value={item.systemInformationID}
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
                                name='database_Version'
                                label={t('Database_Version')}
                                defaultValue={item.database_Version}
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
                                label={t('VersionDate')}
                                value={item.versionDate}
                                onChange={() => { }}
                                renderInput={(params) =>
                                    <TextField
                                        fullWidth
                                        autoComplete='versionDate'
                                        {...params}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />}
                            />
                        </Grid>
                        <Grid item {...gridColumns}>
                            <DatePicker
                                label={t('ModifiedDate')}
                                value={item.modifiedDate}
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

