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

import { addressApi } from "src/apiClients/AddressApi";
import { IAddressCompositeModel, IAddressCompositeModel_DataOptions__ } from "src/dataModels/IAddressCompositeModel";
import ItemViewsPartial from "src/views/Address/ItemViewsPartial";

import { IAddressDataModel } from "src/dataModels/IAddressDataModel";
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
    const addressID = parseInt(params.addressID, 10);
    const [compositeModel, setCompositeModel] = useState<IAddressCompositeModel>(null);


    const listItems_CustomerAddresses_Via_AddressID = useSelector(
        (state: RootState) => customerAddressSelectors.selectAll(state).filter(t => t.addressID === addressID)
    );

    const defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_AddressID = { ...defaultICustomerAddressAdvancedQuery(), addressID: addressID };
    const [advancedQuery_CustomerAddresses_Via_AddressID, setAdvancedQuery_CustomerAddresses_Via_AddressID] = useState<ICustomerAddressAdvancedQuery>(defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_AddressID);

    const listItems_SalesOrderHeaders_Via_BillToAddressID = useSelector(
        (state: RootState) => salesOrderHeaderSelectors.selectAll(state).filter(t => t.billToAddressID === addressID)
    );

    const defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_BillToAddressID = { ...defaultISalesOrderHeaderAdvancedQuery(), billToAddressID: addressID };
    const [advancedQuery_SalesOrderHeaders_Via_BillToAddressID, setAdvancedQuery_SalesOrderHeaders_Via_BillToAddressID] = useState<ISalesOrderHeaderAdvancedQuery>(defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_BillToAddressID);

    const listItems_SalesOrderHeaders_Via_ShipToAddressID = useSelector(
        (state: RootState) => salesOrderHeaderSelectors.selectAll(state).filter(t => t.shipToAddressID === addressID)
    );

    const defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_ShipToAddressID = { ...defaultISalesOrderHeaderAdvancedQuery(), shipToAddressID: addressID };
    const [advancedQuery_SalesOrderHeaders_Via_ShipToAddressID, setAdvancedQuery_SalesOrderHeaders_Via_ShipToAddressID] = useState<ISalesOrderHeaderAdvancedQuery>(defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_ShipToAddressID);

    useEffect(() => {

        addressApi.GetCompositeModel({ addressID })
            .then((res) => {
                //console.log(res);
                setCompositeModel(res);

                const CustomerAddresses_Via_AddressIDListResponseA = res.responses[IAddressCompositeModel_DataOptions__.CustomerAddresses_Via_AddressID];
                console.log(CustomerAddresses_Via_AddressIDListResponseA);
                const CustomerAddresses_Via_AddressIDListResponse = {
                    status: CustomerAddresses_Via_AddressIDListResponseA.status,
                    StatusMessage: CustomerAddresses_Via_AddressIDListResponseA.StatusMessage,
                    pagination: CustomerAddresses_Via_AddressIDListResponseA.responseBody,
                    responseBody: res.customerAddresses_Via_AddressID
                };
                dispatch(upsertManyCustomerAddress(CustomerAddresses_Via_AddressIDListResponse));

                const SalesOrderHeaders_Via_BillToAddressIDListResponseA = res.responses[IAddressCompositeModel_DataOptions__.SalesOrderHeaders_Via_BillToAddressID];
                console.log(SalesOrderHeaders_Via_BillToAddressIDListResponseA);
                const SalesOrderHeaders_Via_BillToAddressIDListResponse = {
                    status: SalesOrderHeaders_Via_BillToAddressIDListResponseA.status,
                    StatusMessage: SalesOrderHeaders_Via_BillToAddressIDListResponseA.StatusMessage,
                    pagination: SalesOrderHeaders_Via_BillToAddressIDListResponseA.responseBody,
                    responseBody: res.salesOrderHeaders_Via_BillToAddressID
                };
                dispatch(upsertManySalesOrderHeader(SalesOrderHeaders_Via_BillToAddressIDListResponse));

                const SalesOrderHeaders_Via_ShipToAddressIDListResponseA = res.responses[IAddressCompositeModel_DataOptions__.SalesOrderHeaders_Via_ShipToAddressID];
                console.log(SalesOrderHeaders_Via_ShipToAddressIDListResponseA);
                const SalesOrderHeaders_Via_ShipToAddressIDListResponse = {
                    status: SalesOrderHeaders_Via_ShipToAddressIDListResponseA.status,
                    StatusMessage: SalesOrderHeaders_Via_ShipToAddressIDListResponseA.StatusMessage,
                    pagination: SalesOrderHeaders_Via_ShipToAddressIDListResponseA.responseBody,
                    responseBody: res.salesOrderHeaders_Via_ShipToAddressID
                };
                dispatch(upsertManySalesOrderHeader(SalesOrderHeaders_Via_ShipToAddressIDListResponse));
            })
            .finally(() => { });
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    if (compositeModel === null) {
        return (<LinearProgress />);
    }
    const gridItemSpacing = 0.5;

    const { __Master__ } = compositeModel;

    const crudItemPartialViewProps = getCRUDItemPartialViewPropsCard<IAddressDataModel>(
        ViewItemTemplates.Details,
        () => {
            navigate(-1);
        } // go back to previous page
    );

    const listsPartialViewProps_CustomerAddresses_Via_AddressID = {
        advancedQuery: advancedQuery_CustomerAddresses_Via_AddressID, setAdvancedQuery: setAdvancedQuery_CustomerAddresses_Via_AddressID,
        defaultAdvancedQuery: defaultICustomerAddressAdvancedQuery_CustomerAddresses_Via_AddressID,
        listItems: listItems_CustomerAddresses_Via_AddressID,
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

    const listsPartialViewProps_SalesOrderHeaders_Via_BillToAddressID = {
        advancedQuery: advancedQuery_SalesOrderHeaders_Via_BillToAddressID, setAdvancedQuery: setAdvancedQuery_SalesOrderHeaders_Via_BillToAddressID,
        defaultAdvancedQuery: defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_BillToAddressID,
        listItems: listItems_SalesOrderHeaders_Via_BillToAddressID,
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

    const listsPartialViewProps_SalesOrderHeaders_Via_ShipToAddressID = {
        advancedQuery: advancedQuery_SalesOrderHeaders_Via_ShipToAddressID, setAdvancedQuery: setAdvancedQuery_SalesOrderHeaders_Via_ShipToAddressID,
        defaultAdvancedQuery: defaultISalesOrderHeaderAdvancedQuery_SalesOrderHeaders_Via_ShipToAddressID,
        listItems: listItems_SalesOrderHeaders_Via_ShipToAddressID,
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
                        <TabList onChange={handleTabChange} aria-label="Address Dashboard tabs">
                            <Tab label={t('Lists')} value='1' />
                        </TabList>
                    </Box>
                        <TabPanel value="1">
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-CustomerAddresses_Via_AddressID-content"
                                id="panel1a-CustomerAddresses_Via_AddressID-header"
                            >
                                <Link to="/CustomerAddress">
                                    <Typography variant="h5" component="h5">{t("CustomerAddress")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <CustomerAddressListsPartial {...listsPartialViewProps_CustomerAddresses_Via_AddressID} />
                            </AccordionDetails>
                        </Accordion>
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-SalesOrderHeaders_Via_BillToAddressID-content"
                                id="panel1a-SalesOrderHeaders_Via_BillToAddressID-header"
                            >
                                <Link to="/SalesOrderHeader">
                                    <Typography variant="h5" component="h5">{t("SalesOrderHeader")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <SalesOrderHeaderListsPartial {...listsPartialViewProps_SalesOrderHeaders_Via_BillToAddressID} />
                            </AccordionDetails>
                        </Accordion>
                        <Accordion defaultExpanded={true}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="panel1a-SalesOrderHeaders_Via_ShipToAddressID-content"
                                id="panel1a-SalesOrderHeaders_Via_ShipToAddressID-header"
                            >
                                <Link to="/SalesOrderHeader">
                                    <Typography variant="h5" component="h5">{t("SalesOrderHeader")}</Typography>
                                </Link>
                            </AccordionSummary>
                            <AccordionDetails>
                                <SalesOrderHeaderListsPartial {...listsPartialViewProps_SalesOrderHeaders_Via_ShipToAddressID} />
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

