import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { ViewItemTemplates } from "src/shared/viewModels/ViewItemTemplates";

import IndexPage from 'src/views/Customer/IndexPage'

export default function CustomerRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
        </Routes>);
}

