import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/SalesOrderHeader/CreatePage'
import DashboardPage from 'src/views/SalesOrderHeader/DashboardPage'
import IndexPage from 'src/views/SalesOrderHeader/IndexPage'
import ItemPage from 'src/views/SalesOrderHeader/ItemPage'

export default function SalesOrderHeaderRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:salesOrderID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:salesOrderID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:salesOrderID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:salesOrderID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

