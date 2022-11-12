import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import DashboardPage from 'src/views/Product/DashboardPage'
import IndexPage from 'src/views/Product/IndexPage'
import ItemPage from 'src/views/Product/ItemPage'

export default function ProductRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Edit/:productID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:productID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

