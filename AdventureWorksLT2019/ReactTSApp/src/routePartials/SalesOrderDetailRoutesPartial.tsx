import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import CreatePage from 'src/views/SalesOrderDetail/CreatePage'
import DashboardPage from 'src/views/SalesOrderDetail/DashboardPage'
import IndexPage from 'src/views/SalesOrderDetail/IndexPage'
import ItemPage from 'src/views/SalesOrderDetail/ItemPage'

export default function SalesOrderDetailRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:salesOrderID/:salesOrderDetailID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:salesOrderID/:salesOrderDetailID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:salesOrderID/:salesOrderDetailID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:salesOrderID/:salesOrderDetailID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

