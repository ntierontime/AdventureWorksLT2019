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

import { productDescriptionApi } from "src/apiClients/ProductDescriptionApi";
import { IProductDescriptionCompositeModel, IProductDescriptionCompositeModel_DataOptions__ } from "src/dataModels/IProductDescriptionCompositeModel";
import ItemViewsPartial from "src/views/ProductDescription/ItemViewsPartial";

import { IProductDescriptionDataModel } from "src/dataModels/IProductDescriptionDataModel";
import { IProductModelProductDescriptionDataModel } from "src/dataModels/IProductModelProductDescriptionDataModel";

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
    const productDescriptionID = parseInt(params.productDescriptionID, 10);
    const [compositeModel, setCompositeModel] = useState<IProductDescriptionCompositeModel>(null);


    const listItems_ProductModelProductDescriptions_Via_ProductDescriptionID = useSelector(
        (state: RootState) => productModelProductDescriptionSelectors.selectAll(state).filter(t => t.productDescriptionID === productDescriptionID)
    );

    const defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID = { ...defaultIProductModelProductDescriptionAdvancedQuery(), productDescriptionID: productDescriptionID };
    const [advancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID, setAdvancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID] = useState<IProductModelProductDescriptionAdvancedQuery>(defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID);

    useEffect(() => {

        productDescriptionApi.GetCompositeModel({ productDescriptionID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);

                const ProductModelProductDescriptions_Via_ProductDescriptionIDListResponseA = res.responses[IProductDescriptionCompositeModel_DataOptions__.ProductModelProductDescriptions_Via_ProductDescriptionID];
                console.log(ProductModelProductDescriptions_Via_ProductDescriptionIDListResponseA);
                const ProductModelProductDescriptions_Via_ProductDescriptionIDListResponse = {
                    status: ProductModelProductDescriptions_Via_ProductDescriptionIDListResponseA.status,
                    StatusMessage: ProductModelProductDescriptions_Via_ProductDescriptionIDListResponseA.StatusMessage,
                    pagination: ProductModelProductDescriptions_Via_ProductDescriptionIDListResponseA.responseBody,
                    responseBody: res.productModelProductDescriptions_Via_ProductDescriptionID
                };
                dispatch(upsertManyProductModelProductDescription(ProductModelProductDescriptions_Via_ProductDescriptionIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<IProductDescriptionDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_ProductModelProductDescriptions_Via_ProductDescriptionID = {
        advancedQuery: advancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID, setAdvancedQuery: setAdvancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID,
        defaultAdvancedQuery: defaultIProductModelProductDescriptionAdvancedQuery_ProductModelProductDescriptions_Via_ProductDescriptionID,
        listItems: listItems_ProductModelProductDescriptions_Via_ProductDescriptionID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("ProductModelProductDescription"),
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
    } as ListsPartialViewProps<IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionDataModel>;

    return (
        <Grid container spacing={gridItemSpacing}>
            <Grid item xs={6} sm={6} md={8} lg={9} xl={10}>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="ProductDescription Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-ProductModelProductDescriptions_Via_ProductDescriptionID-content"
                                id="panel1a-ProductModelProductDescriptions_Via_ProductDescriptionID-header"
                            >
                                <Link to="/ProductModelProductDescription">
                                    <Typography variant="h5" component="h5">{t("ProductModelProductDescription")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <ProductModelProductDescriptionListsPartial {...listsPartialViewProps_ProductModelProductDescriptions_Via_ProductDescriptionID} />
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

