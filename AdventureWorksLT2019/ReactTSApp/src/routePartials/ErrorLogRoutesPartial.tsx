import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import CreatePage from 'src/views/ErrorLog/CreatePage'
import IndexPage from 'src/views/ErrorLog/IndexPage'
import ItemPage from 'src/views/ErrorLog/ItemPage'

export default function ErrorLogRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:errorLogID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:errorLogID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:errorLogID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
        </Routes>);
}

