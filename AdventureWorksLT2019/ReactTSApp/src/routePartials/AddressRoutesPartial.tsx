import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import CreatePage from 'src/views/Address/CreatePage'
import IndexPage from 'src/views/Address/IndexPage'
import ItemPage from 'src/views/Address/ItemPage'

export default function AddressRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
        </Routes>);
}

