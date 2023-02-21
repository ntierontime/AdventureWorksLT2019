import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import DashboardPage from 'src/views/ProductCategory/DashboardPage'
import IndexPage from 'src/views/ProductCategory/IndexPage'

export default function ProductCategoryRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Dashboard/:productCategoryID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

