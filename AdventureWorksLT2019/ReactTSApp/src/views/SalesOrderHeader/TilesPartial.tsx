import { useEffect } from 'react';
import { CircularProgress, Grid, Paper } from '@mui/material';
import { useSelector } from 'react-redux';

import InfiniteScroll from "react-infinite-scroll-component";

// un-comment /*getCurrency,*/ if you display money
import { /*getCurrency,*/ i18nFormats } from 'src/i18n';
import { RootState } from 'src/store/CombinedReducers';
import { getCRUDItemPartialViewPropsInline } from 'src/shared/viewModels/ItemPartialViewProps';
import { ListPartialViewProps } from 'src/shared/viewModels/ListPartialViewProps';
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';

import { ISalesOrderHeaderDataModel } from 'src/dataModels/ISalesOrderHeaderDataModel';
import { ISalesOrderHeaderIdentifier, getISalesOrderHeaderIdentifier, getRouteParamsOfISalesOrderHeaderIdentifier } from 'src/dataModels/ISalesOrderHeaderQueries';
import ItemViewsPartial from './ItemViewsPartial';

export default function TilesPartial(props: ListPartialViewProps<ISalesOrderHeaderDataModel, ISalesOrderHeaderIdentifier>): JSX.Element {
    const { listItems, numSelected, itemsPerRow, isSelected, handleChangePage, handleSelectItemClick, handleItemDialogOpen, setCurrentItemOnDialog, currentItemIndex } = props;

    // const { t } = useTranslation();
    // pagination
    const crudItemPartialViewPropsInline = getCRUDItemPartialViewPropsInline<ISalesOrderHeaderDataModel>(
        ViewItemTemplates.Details,
        null
    );

    const gridItemSpacing = 0.5;
    const gridItemWidth = 12 / itemsPerRow;

    const { pagination } = useSelector((state: RootState) => state.salesOrderHeaderList);
    const hasMoreItems = pagination.pageIndex !== pagination.lastPageIndex;

    useEffect(() => {
        setCurrentItemOnDialog(!!listItems && listItems.length > 0 && currentItemIndex >= 0 && currentItemIndex < listItems.length ? listItems[currentItemIndex] : null);
    }, [currentItemIndex, listItems, setCurrentItemOnDialog]);

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
                        const isItemSelected = isSelected(getISalesOrderHeaderIdentifier(row));

                        return (
                            <Grid item xs={gridItemWidth} key={getRouteParamsOfISalesOrderHeaderIdentifier(row)}>
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
        </>
    );
}

