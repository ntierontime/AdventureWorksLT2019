import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/ProductModel/CreatePage'
import CreateWizardPage from 'src/views/ProductModel/CreateWizardPage'
import DashboardPage from 'src/views/ProductModel/DashboardPage'
import IndexPage from 'src/views/ProductModel/IndexPage'
import ItemPage from 'src/views/ProductModel/ItemPage'

export default function ProductModelRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:productModelID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:productModelID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:productModelID" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:productModelID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
			<Route path="CreateWizard" element={<PrivateRoute> <CreateWizardPage /> </PrivateRoute>} />
        </Routes>);
}

