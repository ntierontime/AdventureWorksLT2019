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

import { productModelApi } from "src/apiClients/ProductModelApi";
import { IProductModelCompositeModel, IProductModelCompositeModel_DataOptions__ } from "src/dataModels/IProductModelCompositeModel";
import ItemViewsPartial from "src/views/ProductModel/ItemViewsPartial";

import { IProductDataModel } from "src/dataModels/IProductDataModel";
import { IProductModelDataModel } from "src/dataModels/IProductModelDataModel";
import { IProductModelProductDescriptionDataModel } from "src/dataModels/IProductModelProductDescriptionDataModel";

import { productSelectors, upsertMany as upsertManyProduct } from "src/slices/ProductSlice";
import { defaultIProductAdvancedQuery, IProductAdvancedQuery } from "src/dataModels/IProductQueries";
import ProductListsPartial from "../Product/ListsPartial";
import { productModelProductDescriptionSelectors, upsertMany as upsertManyProductModelProductDescription } from "src/slices/ProductModelProductDescriptionSlice";
import { defaultIProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionAdvancedQuery } from "src/dataModels/IProductModelProductDescriptionQueries";
import ProductModelProductDescriptionListsPartial from "../ProductModelProductDescription/ListsPartial";

export default function DashboardPage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();


    const [tabValue, setTabValue] = useState('1');
    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    const params = useParams()
    const productModelID = parseInt(params.productModelID, 10);
    const [compositeModel, setCompositeModel] = useState<IProductModelCompositeModel>(null);


    const listItems_Products_Via_ProductModelID = useSelector(
        (state: RootState) => productSelectors.selectAll(state).filter(t => t.productModelID === compositeModel?.__Master__?.productModelID)
    );

    const defaultIProductAdvancedQuery_Products_Via_ProductModelID = { ...defaultIProductAdvancedQuery(), productModelID: compositeModel?.__Master__?.productModelID };
    const [advancedQuery_Products_Via_ProductModelID, setAdvancedQuery_Products_Via_ProductModelID] = useState<IProductAdvancedQuery>(defaultIProductAdvancedQuery_Products_Via_ProductModelID);

    const listItems_ProductModelProductDescriptions_Via_ProductModelID = useSelector(
        (state: RootState) => productModelProductDescriptionSelectors.selectAll(state).filter(t => t.productModelID === compositeModel?.__Master__?.productModelID)
    );

    const defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductModelID = { ...defaultIProductModelProductDescriptionAdvancedQuery(), productModelID: compositeModel?.__Master__?.productModelID };
    const [advancedQuery_ProductModelProductDescriptions_Via_ProductModelID, setAdvancedQuery_ProductModelProductDescriptions_Via_ProductModelID] = useState<IProductModelProductDescriptionAdvancedQuery>(defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductModelID);

    useEffect(() => {

        productModelApi.GetCompositeModel({ productModelID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);
                // // if you want to change page title <html><head><title>...</title></head></html>
                // document.title = res.__Master__.


                const Products_Via_ProductModelIDListResponseA = res.responses[IProductModelCompositeModel_DataOptions__.Products_Via_ProductModelID];
                console.log(Products_Via_ProductModelIDListResponseA);
                const Products_Via_ProductModelIDListResponse = {
                    status: Products_Via_ProductModelIDListResponseA.status,
                    StatusMessage: Products_Via_ProductModelIDListResponseA.StatusMessage,
                    pagination: Products_Via_ProductModelIDListResponseA.responseBody,
                    responseBody: res.products_Via_ProductModelID
                };
                dispatch(upsertManyProduct(Products_Via_ProductModelIDListResponse));

                const ProductModelProductDescriptions_Via_ProductModelIDListResponseA = res.responses[IProductModelCompositeModel_DataOptions__.ProductModelProductDescriptions_Via_ProductModelID];
                console.log(ProductModelProductDescriptions_Via_ProductModelIDListResponseA);
                const ProductModelProductDescriptions_Via_ProductModelIDListResponse = {
                    status: ProductModelProductDescriptions_Via_ProductModelIDListResponseA.status,
                    StatusMessage: ProductModelProductDescriptions_Via_ProductModelIDListResponseA.StatusMessage,
                    pagination: ProductModelProductDescriptions_Via_ProductModelIDListResponseA.responseBody,
                    responseBody: res.productModelProductDescriptions_Via_ProductModelID
                };
                dispatch(upsertManyProductModelProductDescription(ProductModelProductDescriptions_Via_ProductModelIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<IProductModelDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_Products_Via_ProductModelID = {
        advancedQuery: advancedQuery_Products_Via_ProductModelID, setAdvancedQuery: setAdvancedQuery_Products_Via_ProductModelID,
        defaultAdvancedQuery: defaultIProductAdvancedQuery_Products_Via_ProductModelID,
        listItems: listItems_Products_Via_ProductModelID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("Product"),
            hasListViewOptionsSelect: false,
            availableListViewOptions: [ListViewOptions.Table],
            hasItemsSelect: false,
            hasBulkDelete: false,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: true,
            hasAdvancedSearchDialog: false,
        }
    } as ListsPartialViewProps<IProductAdvancedQuery, IProductDataModel>;

    const listsPartialViewProps_ProductModelProductDescriptions_Via_ProductModelID = {
        advancedQuery: advancedQuery_ProductModelProductDescriptions_Via_ProductModelID, setAdvancedQuery: setAdvancedQuery_ProductModelProductDescriptions_Via_ProductModelID,
        defaultAdvancedQuery: defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductModelID,
        listItems: listItems_ProductModelProductDescriptions_Via_ProductModelID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("ProductModelProductDescription"),
            hasListViewOptionsSelect: false,
            availableListViewOptions: [ListViewOptions.Table],
            hasItemsSelect: false,
            hasBulkDelete: false,
            hasBulkUpdate: false,
            hasItemsPerRowSelect: true, // When "Tiles"
            hasPageSizeSelect: true,    // When "Table"
            hasOrderBySelect: true,
            hasSearch: true,			// Text Search
            hasAdvancedSearchAccordion: true,
            hasAdvancedSearchDialog: false,
        }
    } as ListsPartialViewProps<IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionDataModel>;

    return (
        <Grid container spacing={gridItemSpacing}>
            <Grid item xs={6} sm={6} md={8} lg={9} xl={10}>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="ProductModel Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-Products_Via_ProductModelID-content"
                                id="panel1a-Products_Via_ProductModelID-header"
                            >
                                <Link to="/Product">
                                    <Typography variant="h5" component="h5">{t("Product")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <ProductListsPartial {...listsPartialViewProps_Products_Via_ProductModelID} />
                            </AccordionDetails>
                        </Accordion>
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-ProductModelProductDescriptions_Via_ProductModelID-content"
                                id="panel1a-ProductModelProductDescriptions_Via_ProductModelID-header"
                            >
                                <Link to="/ProductModelProductDescription">
                                    <Typography variant="h5" component="h5">{t("ProductModelProductDescription")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <ProductModelProductDescriptionListsPartial {...listsPartialViewProps_ProductModelProductDescriptions_Via_ProductModelID} />
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

