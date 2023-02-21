import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import DashboardPage from 'src/views/SalesOrderHeader/DashboardPage'
import IndexPage from 'src/views/SalesOrderHeader/IndexPage'

export default function SalesOrderHeaderRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Dashboard/:salesOrderID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

