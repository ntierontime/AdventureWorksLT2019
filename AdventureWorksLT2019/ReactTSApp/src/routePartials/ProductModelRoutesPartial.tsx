import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import DashboardPage from 'src/views/ProductModel/DashboardPage'
import IndexPage from 'src/views/ProductModel/IndexPage'

export default function ProductModelRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Dashboard/:productModelID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

