import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { Accordion, AccordionDetails, AccordionSummary, Box, Grid, LinearProgress, Tab, Typography } from "@mui/material";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import { useTranslation } from "react-i18next";

import { AppDispatch } from "src/store/Store";
import { RootState } from "src/store/CombinedReducers";

import { ContainerOptions } from "src/shared/viewModels/ContainerOptions";
import { getCRUDItemPartialViewPropsCard } from "src/shared/viewModels/ItemPartialViewProps";
import { ListsPartialViewProps } from "src/shared/viewModels/ListsPartialViewProps";
import { ListViewOptions } from "src/shared/views/ListViewOptions";
import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import { productApi } from "src/apiClients/ProductApi";
import { IProductCompositeModel, IProductCompositeModel_DataOptions__ } from "src/dataModels/IProductCompositeModel";
import ItemViewsPartial from "src/views/Product/ItemViewsPartial";

import { IProductDataModel } from "src/dataModels/IProductDataModel";
import { ISalesOrderDetailDataModel } from "src/dataModels/ISalesOrderDetailDataModel";

import { salesOrderDetailSelectors, upsertMany as upsertManySalesOrderDetail } from "src/slices/SalesOrderDetailSlice";
import { defaultISalesOrderDetailAdvancedQuery, ISalesOrderDetailAdvancedQuery } from "src/dataModels/ISalesOrderDetailQueries";
import SalesOrderDetailListsPartial from "../SalesOrderDetail/ListsPartial";

export default function DashboardPage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();


    const [tabValue, setTabValue] = useState('1');
    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    const params = useParams()
    const productID = parseInt(params.productID, 10);
    const [compositeModel, setCompositeModel] = useState<IProductCompositeModel>(null);


    const listItems_SalesOrderDetails_Via_ProductID = useSelector(
        (state: RootState) => salesOrderDetailSelectors.selectAll(state).filter(t => t.productID === compositeModel?.__Master__?.productID)
    );

    const defaultISalesOrderDetailAdvancedQuery_SalesOrderDetails_Via_ProductID = { ...defaultISalesOrderDetailAdvancedQuery(), productID: compositeModel?.__Master__?.productID };
    const [advancedQuery_SalesOrderDetails_Via_ProductID, setAdvancedQuery_SalesOrderDetails_Via_ProductID] = useState<ISalesOrderDetailAdvancedQuery>(defaultISalesOrderDetailAdvancedQuery_SalesOrderDetails_Via_ProductID);

    useEffect(() => {

        productApi.GetCompositeModel({ productID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);
                // // if you want to change page title <html><head><title>...</title></head></html>
                // document.title = res.__Master__.


                const SalesOrderDetails_Via_ProductIDListResponseA = res.responses[IProductCompositeModel_DataOptions__.SalesOrderDetails_Via_ProductID];
                console.log(SalesOrderDetails_Via_ProductIDListResponseA);
                const SalesOrderDetails_Via_ProductIDListResponse = {
                    status: SalesOrderDetails_Via_ProductIDListResponseA.status,
                    StatusMessage: SalesOrderDetails_Via_ProductIDListResponseA.StatusMessage,
                    pagination: SalesOrderDetails_Via_ProductIDListResponseA.responseBody,
                    responseBody: res.salesOrderDetails_Via_ProductID
                };
                dispatch(upsertManySalesOrderDetail(SalesOrderDetails_Via_ProductIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<IProductDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_SalesOrderDetails_Via_ProductID = {
        advancedQuery: advancedQuery_SalesOrderDetails_Via_ProductID, setAdvancedQuery: setAdvancedQuery_SalesOrderDetails_Via_ProductID,
        defaultAdvancedQuery: defaultISalesOrderDetailAdvancedQuery_SalesOrderDetails_Via_ProductID,
        listItems: listItems_SalesOrderDetails_Via_ProductID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("SalesOrderDetail"),
            hasListViewOptionsSelect: true,
            availableListViewOptions: [ListViewOptions.SlideShow, ListViewOptions.Table, ListViewOptions.Tiles],
            hasItemsSelect: true,
            hasBulkDelete: true,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: true,
            hasAdvancedSearchDialog: false,
        }
    } as ListsPartialViewProps<ISalesOrderDetailAdvancedQuery, ISalesOrderDetailDataModel>;

    return (
        <Grid container spacing={gridItemSpacing}>
            <Grid item xs={6} sm={6} md={8} lg={9} xl={10}>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="Product Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-SalesOrderDetails_Via_ProductID-content"
                                id="panel1a-SalesOrderDetails_Via_ProductID-header"
                            >
                                <Link to="/SalesOrderDetail">
                                    <Typography variant="h5" component="h5">{t("SalesOrderDetail")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <SalesOrderDetailListsPartial {...listsPartialViewProps_SalesOrderDetails_Via_ProductID} />
                            </AccordionDetails>
                        </Accordion>
                        </TabPanel>
                </TabContext>
            </Grid>
            <Grid item xs={6} sm={6} md={4} lg={3} xl={2}>
                <ItemViewsPartial {...crudItemPartialViewProps} item={__Master__} />
            </Grid>
        </Grid>
    );
}

