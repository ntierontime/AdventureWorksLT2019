import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/ProductCategory/CreatePage'
import DashboardPage from 'src/views/ProductCategory/DashboardPage'
import IndexPage from 'src/views/ProductCategory/IndexPage'
import ItemPage from 'src/views/ProductCategory/ItemPage'

export default function ProductCategoryRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:productCategoryID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:productCategoryID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:productCategoryID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:productCategoryID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

