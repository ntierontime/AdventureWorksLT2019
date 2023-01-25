import { useState } from 'react';
import { Box, Checkbox, FormControlLabel, IconButton, Paper, Toolbar, Typography } from '@mui/material';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import Carousel from 'react-material-ui-carousel';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { defaultCarouselSettings } from 'src/shared/viewModels/CarouselProps';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import { IBuildVersionIdentifier, getIBuildVersionIdentifier, getRouteParamsOfIBuildVersionIdentifier } from 'src/dataModels/IBuildVersionQueries';

export default function CarouselPartial(props: ListPartialViewProps<IBuildVersionDataModel, IBuildVersionIdentifier>): JSX.Element {
    const { listItems, isSelected, handleSelectItemClick, handleItemDialogOpen, currentItemIndex } = props;
    const { t } = useTranslation();

    const settings = defaultCarouselSettings;
    const [autoPlay, setAutoPlay] = useState<boolean>(true);

    const renderCarouselItem = (item: IBuildVersionDataModel, index: number) => {
        const isItemSelected = isSelected(getIBuildVersionIdentifier(item));
		const key = getRouteParamsOfIBuildVersionIdentifier(item);
        const labelId = `enhanced-table-checkbox-${key}`;

        return (
            <Paper elevation={10} key={key}>
                <FormControlLabel
                    label={item.database_Version}
                    control={<Checkbox
                        color="primary"
                        checked={isItemSelected}
                        onChange={() => { handleSelectItemClick(item) }}
                        inputProps={{
                            'aria-labelledby': labelId,
                        }}
                    />}
                />
                <br />
                <Typography>{t('{{val, datetime}}', { val: new Date(item.versionDate) })}</Typography>
                <br />
                <Toolbar sx={{ display: "flex", justifyContent: "center" }}>
                    <IconButton aria-label="delete" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Delete, index) }}>
                        <DeleteIcon />
                    </IconButton>
                    <IconButton aria-label="details" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Details, index) }}>
                        <BusinessCenterIcon />
                    </IconButton>
                    <IconButton aria-label="edit" color="primary" onClick={() => { handleItemDialogOpen(ViewItemTemplates.Edit, index) }}>
                        <EditIcon />
                    </IconButton>
                </Toolbar >
            </Paper>
        )
    }

    return (
        <Box>
            <Carousel {...settings} index={currentItemIndex} autoPlay={autoPlay}>
                {!!listItems && listItems.map((item, index) => {
                    return renderCarouselItem(item, index)
                })}
            </Carousel>
        </Box>
    );
}

