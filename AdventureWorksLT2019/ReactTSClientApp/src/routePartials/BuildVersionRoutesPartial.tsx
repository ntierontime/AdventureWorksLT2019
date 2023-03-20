import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";
import CreatePage from 'src/views/BuildVersion/CreatePage'
import CreateWizardPage from 'src/views/BuildVersion/CreateWizardPage'
import DashboardPage from 'src/views/BuildVersion/DashboardPage'
import IndexPage from 'src/views/BuildVersion/IndexPage'
import IndexPage1 from 'src/views/BuildVersion/IndexPage1'
import ItemPage from 'src/views/BuildVersion/ItemPage'

export default function BuildVersionRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
            <Route path="a1" element={<PrivateRoute> <IndexPage1 /> </PrivateRoute>} />
			<Route path="Create" element={<PrivateRoute> <CreatePage /> </PrivateRoute>} />
			<Route path="Delete/:systemInformationID/:versionDate/:modifiedDate" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Delete} /> </PrivateRoute>} />
			<Route path="Details/:systemInformationID/:versionDate/:modifiedDate" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> </PrivateRoute>} />
			<Route path="Edit/:systemInformationID/:versionDate/:modifiedDate" element={<PrivateRoute> <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> </PrivateRoute>} />
			<Route path="Dashboard/:systemInformationID/:versionDate/:modifiedDate" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
			<Route path="CreateWizard" element={<PrivateRoute> <CreateWizardPage /> </PrivateRoute>} />
        </Routes>);
}

