import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import DashboardPage from 'src/views/Customer/DashboardPage'
import IndexPage from 'src/views/Customer/IndexPage'

export default function CustomerRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
			<Route path="Dashboard/:customerID" element={<PrivateRoute> <DashboardPage /> </PrivateRoute>} />
        </Routes>);
}

