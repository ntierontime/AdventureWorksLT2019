import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import DashboardPage from 'src/views/SalesOrderHeader/DashboardPage'
import IndexPage from 'src/views/SalesOrderHeader/IndexPage'
import ItemPage from 'src/views/SalesOrderHeader/ItemPage'

export default function SalesOrderHeaderRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Edit/:salesOrderID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:salesOrderID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

