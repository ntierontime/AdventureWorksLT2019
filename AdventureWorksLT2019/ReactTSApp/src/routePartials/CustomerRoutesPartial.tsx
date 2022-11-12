import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import DashboardPage from 'src/views/Customer/DashboardPage'
import IndexPage from 'src/views/Customer/IndexPage'
import ItemPage from 'src/views/Customer/ItemPage'

export default function CustomerRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Edit/:customerID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:customerID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

