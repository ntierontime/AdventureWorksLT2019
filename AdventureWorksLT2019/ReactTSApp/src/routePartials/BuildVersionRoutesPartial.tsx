import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import IndexPage from 'src/views/BuildVersion/IndexPage'

export default function BuildVersionRoutesPartial(): JSX.Element {
    return (
        <Routes>
			<Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
        </Routes>);
}

