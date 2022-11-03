import { Routes, Route } from "react-router-dom";
import LoginPage from "./Account/Login";

export default function AccountRoutesPartial(): JSX.Element {
    return (
        <Routes>
            <Route path="login" element={<LoginPage />} />
        </Routes>);
}
