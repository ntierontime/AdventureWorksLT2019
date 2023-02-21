import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import DashboardPage from 'src/views/Product/DashboardPage'
import IndexPage from 'src/views/Product/IndexPage'

export default function ProductRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Dashboard/:productID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

