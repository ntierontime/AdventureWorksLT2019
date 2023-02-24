import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/CustomerAddress/CreatePage'
import CreateWizardPage from 'src/views/CustomerAddress/CreateWizardPage'
import DashboardPage from 'src/views/CustomerAddress/DashboardPage'
import IndexPage from 'src/views/CustomerAddress/IndexPage'
import ItemPage from 'src/views/CustomerAddress/ItemPage'

export default function CustomerAddressRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:customerID/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:customerID/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:customerID/:addressID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:customerID/:addressID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
			<Route path="CreateWizard" element={<PrivateRoute> <CreateWizardPage /> </PrivateRoute>} />
        </Routes>);
}

