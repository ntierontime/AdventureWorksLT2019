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

import { productCategoryApi } from "src/apiClients/ProductCategoryApi";
import { IProductCategoryCompositeModel, IProductCategoryCompositeModel_DataOptions__ } from "src/dataModels/IProductCategoryCompositeModel";
import ItemViewsPartial from "src/views/ProductCategory/ItemViewsPartial";

import { IProductDataModel } from "src/dataModels/IProductDataModel";
import { IProductCategoryDataModel } from "src/dataModels/IProductCategoryDataModel";

import { productSelectors, upsertMany as upsertManyProduct } from "src/slices/ProductSlice";
import { defaultIProductAdvancedQuery, IProductAdvancedQuery } from "src/dataModels/IProductQueries";
import ProductListsPartial from "../Product/ListsPartial";
import { productCategorySelectors, upsertMany as upsertManyProductCategory } from "src/slices/ProductCategorySlice";
import { defaultIProductCategoryAdvancedQuery, IProductCategoryAdvancedQuery } from "src/dataModels/IProductCategoryQueries";
import ProductCategoryListsPartial from "../ProductCategory/ListsPartial";

export default function DashboardPage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();


    const [tabValue, setTabValue] = useState('1');
    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    const params = useParams()
    const productCategoryID = parseInt(params.productCategoryID, 10);
    const [compositeModel, setCompositeModel] = useState<IProductCategoryCompositeModel>(null);


    const listItems_Products_Via_ProductCategoryID = useSelector(
        (state: RootState) => productSelectors.selectAll(state).filter(t => t.productCategoryID === productCategoryID)
    );

    const defaultIProductAdvancedQuery_Products_Via_ProductCategoryID = { ...defaultIProductAdvancedQuery(), productCategoryID: productCategoryID };
    const [advancedQuery_Products_Via_ProductCategoryID, setAdvancedQuery_Products_Via_ProductCategoryID] = useState<IProductAdvancedQuery>(defaultIProductAdvancedQuery_Products_Via_ProductCategoryID);

    const listItems_ProductCategories_Via_ParentProductCategoryID = useSelector(
        (state: RootState) => productCategorySelectors.selectAll(state).filter(t => t.parentProductCategoryID === productCategoryID)
    );

    const defaultIProductCategoryAdvancedQuery_ProductCategories_Via_ParentProductCategoryID = { ...defaultIProductCategoryAdvancedQuery(), parentProductCategoryID: productCategoryID };
    const [advancedQuery_ProductCategories_Via_ParentProductCategoryID, setAdvancedQuery_ProductCategories_Via_ParentProductCategoryID] = useState<IProductCategoryAdvancedQuery>(defaultIProductCategoryAdvancedQuery_ProductCategories_Via_ParentProductCategoryID);

    useEffect(() => {

        productCategoryApi.GetCompositeModel({ productCategoryID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);

                const Products_Via_ProductCategoryIDListResponseA = res.responses[IProductCategoryCompositeModel_DataOptions__.Products_Via_ProductCategoryID];
                console.log(Products_Via_ProductCategoryIDListResponseA);
                const Products_Via_ProductCategoryIDListResponse = {
                    status: Products_Via_ProductCategoryIDListResponseA.status,
                    StatusMessage: Products_Via_ProductCategoryIDListResponseA.StatusMessage,
                    pagination: Products_Via_ProductCategoryIDListResponseA.responseBody,
                    responseBody: res.products_Via_ProductCategoryID
                };
                dispatch(upsertManyProduct(Products_Via_ProductCategoryIDListResponse));

                const ProductCategories_Via_ParentProductCategoryIDListResponseA = res.responses[IProductCategoryCompositeModel_DataOptions__.ProductCategories_Via_ParentProductCategoryID];
                console.log(ProductCategories_Via_ParentProductCategoryIDListResponseA);
                const ProductCategories_Via_ParentProductCategoryIDListResponse = {
                    status: ProductCategories_Via_ParentProductCategoryIDListResponseA.status,
                    StatusMessage: ProductCategories_Via_ParentProductCategoryIDListResponseA.StatusMessage,
                    pagination: ProductCategories_Via_ParentProductCategoryIDListResponseA.responseBody,
                    responseBody: res.productCategories_Via_ParentProductCategoryID
                };
                dispatch(upsertManyProductCategory(ProductCategories_Via_ParentProductCategoryIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<IProductCategoryDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_Products_Via_ProductCategoryID = {
        advancedQuery: advancedQuery_Products_Via_ProductCategoryID, setAdvancedQuery: setAdvancedQuery_Products_Via_ProductCategoryID,
        defaultAdvancedQuery: defaultIProductAdvancedQuery_Products_Via_ProductCategoryID,
        listItems: listItems_Products_Via_ProductCategoryID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("Product"),
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
    } as ListsPartialViewProps<IProductAdvancedQuery, IProductDataModel>;

    const listsPartialViewProps_ProductCategories_Via_ParentProductCategoryID = {
        advancedQuery: advancedQuery_ProductCategories_Via_ParentProductCategoryID, setAdvancedQuery: setAdvancedQuery_ProductCategories_Via_ParentProductCategoryID,
        defaultAdvancedQuery: defaultIProductCategoryAdvancedQuery_ProductCategories_Via_ParentProductCategoryID,
        listItems: listItems_ProductCategories_Via_ParentProductCategoryID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("ProductCategory"),
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
    } as ListsPartialViewProps<IProductCategoryAdvancedQuery, IProductCategoryDataModel>;

    return (
        <Grid container spacing={gridItemSpacing}>
            <Grid item xs={6} sm={6} md={8} lg={9} xl={10}>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="ProductCategory Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-Products_Via_ProductCategoryID-content"
                                id="panel1a-Products_Via_ProductCategoryID-header"
                            >
                                <Link to="/Product">
                                    <Typography variant="h5" component="h5">{t("Product")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <ProductListsPartial {...listsPartialViewProps_Products_Via_ProductCategoryID} />
                            </AccordionDetails>
                        </Accordion>
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-ProductCategories_Via_ParentProductCategoryID-content"
                                id="panel1a-ProductCategories_Via_ParentProductCategoryID-header"
                            >
                                <Link to="/ProductCategory">
                                    <Typography variant="h5" component="h5">{t("ProductCategory")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <ProductCategoryListsPartial {...listsPartialViewProps_ProductCategories_Via_ParentProductCategoryID} />
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

