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

import { customerApi } from "src/apiClients/CustomerApi";
import { ICustomerCompositeModel, ICustomerCompositeModel_DataOptions__ } from "src/dataModels/ICustomerCompositeModel";
import ItemViewsPartial from "src/views/Customer/ItemViewsPartial";

import { ICustomerDataModel } from "src/dataModels/ICustomerDataModel";
import { ICustomerAddressDataModel } from "src/dataModels/ICustomerAddressDataModel";
import { ISalesOrderHeaderDataModel } from "src/dataModels/ISalesOrderHeaderDataModel";

import { customerAddressSelectors, upsertMany as upsertManyCustomerAddress } from "src/slices/CustomerAddressSlice";
import { defaultICustomerAddressAdvancedQuery, ICustomerAddressAdvancedQuery } from "src/dataModels/ICustomerAddressQueries";
import CustomerAddressListsPartial from "../CustomerAddress/ListsPartial";
import { salesOrderHeaderSelectors, upsertMany as upsertManySalesOrderHeader } from "src/slices/SalesOrderHeaderSlice";
import { defaultISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderAdvancedQuery } from "src/dataModels/ISalesOrderHeaderQueries";
import SalesOrderHeaderListsPartial from "../SalesOrderHeader/ListsPartial";

export default function DashboardPage(): JSX.Element {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();


    const [tabValue, setTabValue] = useState('1');
    const handleTabChange = (event: React.SyntheticEvent, newValue: string) => {
        setTabValue(newValue);
    };

    const params = useParams()
    const customerID = parseInt(params.customerID, 10);
    const [compositeModel, setCompositeModel] = useState<ICustomerCompositeModel>(null);


    const listItems_CustomerAddresses_Via_CustomerID = useSelector(
        (state: RootState) => customerAddressSelectors.selectAll(state).filter(t => t.customerID === compositeModel?.__Master__?.customerID)
    );

    const defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_CustomerID = { ...defaultICustomerAddressAdvancedQuery(), customerID: compositeModel?.__Master__?.customerID };
    const [advancedQuery_CustomerAddresses_Via_CustomerID, setAdvancedQuery_CustomerAddresses_Via_CustomerID] = useState<ICustomerAddressAdvancedQuery>(defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_CustomerID);

    const listItems_SalesOrderHeaders_Via_CustomerID = useSelector(
        (state: RootState) => salesOrderHeaderSelectors.selectAll(state).filter(t => t.customerID === compositeModel?.__Master__?.customerID)
    );

    const defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_CustomerID = { ...defaultISalesOrderHeaderAdvancedQuery(), customerID: compositeModel?.__Master__?.customerID };
    const [advancedQuery_SalesOrderHeaders_Via_CustomerID, setAdvancedQuery_SalesOrderHeaders_Via_CustomerID] = useState<ISalesOrderHeaderAdvancedQuery>(defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_CustomerID);

    useEffect(() => {

        customerApi.GetCompositeModel({ customerID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);
                // // if you want to change page title <html><head><title>...</title></head></html>
                // document.title = res.__Master__.


                const CustomerAddresses_Via_CustomerIDListResponseA = res.responses[ICustomerCompositeModel_DataOptions__.CustomerAddresses_Via_CustomerID];
                console.log(CustomerAddresses_Via_CustomerIDListResponseA);
                const CustomerAddresses_Via_CustomerIDListResponse = {
                    status: CustomerAddresses_Via_CustomerIDListResponseA.status,
                    StatusMessage: CustomerAddresses_Via_CustomerIDListResponseA.StatusMessage,
                    pagination: CustomerAddresses_Via_CustomerIDListResponseA.responseBody,
                    responseBody: res.customerAddresses_Via_CustomerID
                };
                dispatch(upsertManyCustomerAddress(CustomerAddresses_Via_CustomerIDListResponse));

                const SalesOrderHeaders_Via_CustomerIDListResponseA = res.responses[ICustomerCompositeModel_DataOptions__.SalesOrderHeaders_Via_CustomerID];
                console.log(SalesOrderHeaders_Via_CustomerIDListResponseA);
                const SalesOrderHeaders_Via_CustomerIDListResponse = {
                    status: SalesOrderHeaders_Via_CustomerIDListResponseA.status,
                    StatusMessage: SalesOrderHeaders_Via_CustomerIDListResponseA.StatusMessage,
                    pagination: SalesOrderHeaders_Via_CustomerIDListResponseA.responseBody,
                    responseBody: res.salesOrderHeaders_Via_CustomerID
                };
                dispatch(upsertManySalesOrderHeader(SalesOrderHeaders_Via_CustomerIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<ICustomerDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_CustomerAddresses_Via_CustomerID = {
        advancedQuery: advancedQuery_CustomerAddresses_Via_CustomerID, setAdvancedQuery: setAdvancedQuery_CustomerAddresses_Via_CustomerID,
        defaultAdvancedQuery: defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_CustomerID,
        listItems: listItems_CustomerAddresses_Via_CustomerID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("CustomerAddress"),
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
    } as ListsPartialViewProps<ICustomerAddressAdvancedQuery, ICustomerAddressDataModel>;

    const listsPartialViewProps_SalesOrderHeaders_Via_CustomerID = {
        advancedQuery: advancedQuery_SalesOrderHeaders_Via_CustomerID, setAdvancedQuery: setAdvancedQuery_SalesOrderHeaders_Via_CustomerID,
        defaultAdvancedQuery: defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_CustomerID,
        listItems: listItems_SalesOrderHeaders_Via_CustomerID,
        initialLoadFromServer: true,
        hasListToolBar: true,
        hasAdvancedSearch : true,
        addNewButtonContainer: ContainerOptions.ListToolBar,
        listToolBarSetting: {
            textSearchPlaceHolder: t("SalesOrderHeader"),
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
    } as ListsPartialViewProps<ISalesOrderHeaderAdvancedQuery, ISalesOrderHeaderDataModel>;

    return (
        <Grid container spacing={gridItemSpacing}>
            <Grid item xs={6} sm={6} md={8} lg={9} xl={10}>
                <TabContext value={tabValue}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleTabChange} aria-label="Customer Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-CustomerAddresses_Via_CustomerID-content"
                                id="panel1a-CustomerAddresses_Via_CustomerID-header"
                            >
                                <Link to="/CustomerAddress">
                                    <Typography variant="h5" component="h5">{t("CustomerAddress")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <CustomerAddressListsPartial {...listsPartialViewProps_CustomerAddresses_Via_CustomerID} />
                            </AccordionDetails>
                        </Accordion>
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-SalesOrderHeaders_Via_CustomerID-content"
                                id="panel1a-SalesOrderHeaders_Via_CustomerID-header"
                            >
                                <Link to="/SalesOrderHeader">
                                    <Typography variant="h5" component="h5">{t("SalesOrderHeader")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <SalesOrderHeaderListsPartial {...listsPartialViewProps_SalesOrderHeaders_Via_CustomerID} />
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

