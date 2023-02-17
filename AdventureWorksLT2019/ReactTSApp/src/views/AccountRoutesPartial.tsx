import { Routes, Route } from "react-router-dom";
import LoginPage from "./Account/Login";
import RegisterPage from "./Account/Register";

export default function AccountRoutesPartial(): JSX.Element {
    return (
        <Routes>
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegisterPage />} />
        </Routes>);
}
