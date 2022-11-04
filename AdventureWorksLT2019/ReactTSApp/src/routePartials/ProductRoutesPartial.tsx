import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import CreatePage from 'src/views/Product/CreatePage'
import IndexPage from 'src/views/Product/IndexPage'
import ItemPage from 'src/views/Product/ItemPage'

export default function ProductRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:productID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:productID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:productID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
        </Routes>);
}

