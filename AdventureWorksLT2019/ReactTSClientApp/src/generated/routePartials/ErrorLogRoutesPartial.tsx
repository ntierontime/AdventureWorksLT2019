import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import { AppDrawerProps } from "src/shared/views/appDrawer/Drawer";
import { ListViewOptions } from "src/shared/views/ListViewOptions";
import { ViewItemTemplates } from 'src/shared/viewModels/ViewItemTemplates';
import IndexPage from 'src/generated/views/ErrorLog/IndexPage'


export function ErrorLogsRoutesPartial(): JSX.Element {
    return (
        <Routes>
            <Route index element={<PrivateRoute> <IndexPage /> </PrivateRoute>} />
        </Routes>);
}

