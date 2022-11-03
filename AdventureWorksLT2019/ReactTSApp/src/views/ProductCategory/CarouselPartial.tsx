import { useState } from 'react';
import { Box, Checkbox, Dialog, FormControlLabel, IconButton, Paper, Toolbar, Typography } from '@mui/material';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import Carousel from 'react-material-ui-carousel';
import { useTranslation } from 'react-i18next';

// un-comment /*getCurrency,*/ if you display money
import i18n, { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { defaultCarouselSettings } from 'src/shared/viewModels/CarouselProps';
import { getCRUDItemPartialViewPropsOnDialog, ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { IProductCategoryDataModel } from 'src/dataModels/IProductCategoryDataModel';
import { IProductCategoryIdentifier, getIProductCategoryIdentifier, compareIProductCategoryIdentifier, getRouteParamsOfIProductCategoryIdentifier } from 'src/dataModels/IProductCategoryQueries';
import ItemViewsPartial from './ItemViewsPartial';

export default function CarouselPartial(props: ListPartialViewProps<IProductCategoryDataModel, IProductCategoryIdentifier>): JSX.Element {
    const { listItems, selected, handleSelectItemClick } = props;
    const { t } = useTranslation();

    const settings = defaultCarouselSettings;
    const [currentItemIndex, setCurrentItemIndex] = useState<number>(0);
    const [autoPlay, setAutoPlay] = useState<boolean>(true);

    const [openItemDialog, setOpenItemDialog] = useState(false);
    const [crudItemPartialViewProps, setCRUDItemPartialViewProps] = useState<ItemPartialViewProps<IProductCategoryDataModel> | null>(null);

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, index: number) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IProductCategoryDataModel>(
            viewItemTemplate,
            handleItemDialogClose
        );

        setCurrentItemIndex(index);
        setAutoPlay(false);
        setCRUDItemPartialViewProps(dialogProps);
        setOpenItemDialog(true);
    };

    const handleItemDialogClose = () => {
        setOpenItemDialog(false);
        setCRUDItemPartialViewProps(null);
        setAutoPlay(true);
    };

    const currentItemOnDialog = !!listItems && listItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < listItems.length ? listItems[currentItemIndex] : null;
    const isSelected = (identifier: IProductCategoryIdentifier) => selected.findIndex(t=> { return compareIProductCategoryIdentifier(identifier, t); }) !== -1;

    const renderCarouselItem = (item: IProductCategoryDataModel, index: number) => {
        const isItemSelected = isSelected(getIProductCategoryIdentifier(item));
		const key = getRouteParamsOfIProductCategoryIdentifier(item);
        const labelId = `enhanced-table-checkbox-${key}`;

        return (
            <Paper elevation={10} key={key}>
                <FormControlLabel
                    label={item.name}
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
                <Typography>{t('{{val, datetime}}', { val: new Date(item.modifiedDate) })}</Typography>
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
            <Dialog open={openItemDialog} fullWidth={true} maxWidth={'sm'}>
                <ItemViewsPartial {...crudItemPartialViewProps} item={currentItemOnDialog} isItemSelected={!!currentItemOnDialog && isSelected(getIProductCategoryIdentifier(currentItemOnDialog))} totalCountInList={listItems.length} itemIndex={currentItemIndex} setItemIndex={setCurrentItemIndex} handleSelectItemClick={handleSelectItemClick} />
            </Dialog>
        </Box>
    );
}

