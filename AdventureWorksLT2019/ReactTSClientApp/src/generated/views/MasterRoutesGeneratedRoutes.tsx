import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

//import PrivateRouteTestPage from './PrivateRouteTestPage';




import { AddressesRoutesPartial } from 'src/generated/routePartials/AddressRoutesPartial'
import { CustomersRoutesPartial } from 'src/generated/routePartials/CustomerRoutesPartial'
import { ErrorLogsRoutesPartial } from 'src/generated/routePartials/ErrorLogRoutesPartial'
import { ProductCategoriesRoutesPartial } from 'src/generated/routePartials/ProductCategoryRoutesPartial'
import { ProductDescriptionsRoutesPartial } from 'src/generated/routePartials/ProductDescriptionRoutesPartial'
import { ProductModelsRoutesPartial } from 'src/generated/routePartials/ProductModelRoutesPartial'
import { ProductsRoutesPartial } from 'src/generated/routePartials/ProductRoutesPartial'
import { SalesOrderHeadersRoutesPartial } from 'src/generated/routePartials/SalesOrderHeaderRoutesPartial'

export default function MasterRoutesGeneratedRoutes(): JSX.Element {
    return (
        <Routes>
            {/* <Route path="PrivateRouteTestPage" element={
                <PrivateRoute>
                    <PrivateRouteTestPage />
                </PrivateRoute>} /> */}





            <Route path="ErrorLogs/*" element={<ErrorLogsRoutesPartial />} />
            <Route path="Addresses/*" element={<AddressesRoutesPartial />} />
            <Route path="Customers/*" element={<CustomersRoutesPartial />} />
            <Route path="Products/*" element={<ProductsRoutesPartial />} />
            <Route path="ProductCategories/*" element={<ProductCategoriesRoutesPartial />} />
            <Route path="ProductDescriptions/*" element={<ProductDescriptionsRoutesPartial />} />
            <Route path="ProductModels/*" element={<ProductModelsRoutesPartial />} />
            <Route path="SalesOrderHeaders/*" element={<SalesOrderHeadersRoutesPartial />} />

        </Routes>);
}

