import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/ProductModelProductDescription/CreatePage'
import CreateWizardPage from 'src/views/ProductModelProductDescription/CreateWizardPage'
import DashboardPage from 'src/views/ProductModelProductDescription/DashboardPage'
import IndexPage from 'src/views/ProductModelProductDescription/IndexPage'
import ItemPage from 'src/views/ProductModelProductDescription/ItemPage'

export default function ProductModelProductDescriptionRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:productModelID/:productDescriptionID/:culture" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:productModelID/:productDescriptionID/:culture" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:productModelID/:productDescriptionID/:culture" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:productModelID/:productDescriptionID/:culture" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
			<Route path="CreateWizard" element={<PrivateRoute> <CreateWizardPage /> </PrivateRoute>} />
        </Routes>);
}

