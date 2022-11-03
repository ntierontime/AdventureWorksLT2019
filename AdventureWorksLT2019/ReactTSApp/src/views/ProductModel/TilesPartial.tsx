import { useState } from 'react';
import { ButtonGroup, CircularProgress, Dialog, Grid, IconButton, Paper, Snackbar } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

import InfiniteScroll from "react-infinite-scroll-component";

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { RootState } from 'src/store/CombinedReducers';
import { getCRUDItemPartialViewPropsInline, getCRUDItemPartialViewPropsOnDialog, ItemPartialViewProps } from 'src/shared/viewModels/ItemPartialViewProps';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { IProductModelIdentifier, getIProductModelIdentifier, compareIProductModelIdentifier, getRouteParamsOfIProductModelIdentifier } from 'src/dataModels/IProductModelQueries';
import ItemViewsPartial from './ItemViewsPartial';

export default function TilesPartial(props: ListPartialViewProps<IProductModelDataModel, IProductModelIdentifier>): JSX.Element {
    const { listItems, itemsPerRow, selected, handleSelectItemClick, handleChangePage } = props;
	const navigate = useNavigate();
    // const { t } = useTranslation();
    // pagination
    const crudItemPartialViewPropsInline = getCRUDItemPartialViewPropsInline<IProductModelDataModel>(
        ViewItemTemplates.Details,
        null
    );

    const [currentItemIndex, setCurrentItemIndex] = useState<number>();
    const [openItemDialog, setOpenItemDialog] = useState(false);
    const [crudItemPartialViewPropsOnDialog, setCRUDItemPartialViewPropsOnDialog] = useState<ItemPartialViewProps<IProductModelDataModel> | null>(null);

    const handleItemDialogOpen = (viewItemTemplate: ViewItemTemplates, itemIndex: number) => {
        const dialogProps = getCRUDItemPartialViewPropsOnDialog<IProductModelDataModel>(
            viewItemTemplate,
            handleItemDialogClose
        );
        setCurrentItemIndex(itemIndex);
        setCRUDItemPartialViewPropsOnDialog(dialogProps);
        setOpenItemDialog(true);
    };

    const handleItemDialogClose = () => {
        setOpenItemDialog(false);
        setCRUDItemPartialViewPropsOnDialog(null);
        setCurrentItemIndex(-1);
    };

    const gridItemSpacing = 0.5;
    const isSelected = (identifier: IProductModelIdentifier) => selected.findIndex(t=> { return compareIProductModelIdentifier(identifier, t); }) !== -1;
    const gridItemWidth = 12 / itemsPerRow;
    const currentItemOnDialog = !!listItems && listItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < listItems.length ? listItems[currentItemIndex] : null;

    const { pagination } = useSelector((state: RootState) => state.productModelList);
    const hasMoreItems = pagination.pageIndex !== pagination.lastPageIndex;

    return (
        <>
            <InfiniteScroll
                dataLength={listItems.length}
                next={() => { handleChangePage(null, 1); }}
                hasMore={hasMoreItems}
                scrollThreshold={1}
                loader={<CircularProgress />}
                // Let's get rid of second scroll bar
                style={{ overflow: "unset" }}
            >
                <Grid container spacing={gridItemSpacing}>
                    {listItems && listItems.map((row, index) => {
                        const isItemSelected = isSelected(getIProductModelIdentifier(row));

                        return (
                            <Grid item xs={gridItemWidth} key={getRouteParamsOfIProductModelIdentifier(row)}>
                                <Paper elevation={3} sx={{ p: 1, height: "100%" }}>
                                    <ItemViewsPartial {...crudItemPartialViewPropsInline} item={row} totalCountInList={listItems.length} itemIndex={index} isItemSelected={isItemSelected} handleSelectItemClick={handleSelectItemClick} handleItemDialogOpen={handleItemDialogOpen} />
                                </Paper>
                            </Grid>
                        );
                    })}
                    {/* <Grid item xs={12}>
                        <Button sx={{ marginLeft: 'auto' }}
                            variant='contained'
                            startIcon={<ArrowCircleDownIcon />}
                            onClick={() => { handleChangePage(null, 1) }}
                        >
                            {t('More')}
                        </Button>
                    </Grid> */}
                </Grid>
            </InfiniteScroll>
            <Snackbar
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                open={true}
            >
				<ButtonGroup orientation='horizontal'>
                    <IconButton onClick={() => { handleItemDialogOpen(ViewItemTemplates.Create, -1); }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
                        <AddIcon />
                    </IconButton>
                    <IconButton onClick={() => { navigate("/productModel/create") }} aria-label="create" component="label" size="large" color='primary' sx={{ backgroundColor: 'gray' }}>
                        <AddIcon />
                    </IconButton>
                </ButtonGroup>
            </Snackbar>
            <Dialog open={openItemDialog} fullWidth={true} maxWidth={'sm'}>
                {!!crudItemPartialViewPropsOnDialog && <ItemViewsPartial {...crudItemPartialViewPropsOnDialog} item={currentItemOnDialog} isItemSelected={!!currentItemOnDialog && isSelected(getIProductModelIdentifier(currentItemOnDialog))} totalCountInList={listItems.length} itemIndex={currentItemIndex} setItemIndex={setCurrentItemIndex} handleSelectItemClick={handleSelectItemClick} />}
            </Dialog>
        </>
    );
}

