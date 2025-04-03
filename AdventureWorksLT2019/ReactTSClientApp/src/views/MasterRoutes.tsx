import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";

import AutoLogIn from './Identity/AutoLogIn';
import AutoLogout from "./Identity/AutoLogout";
import ConfirmEmail from "./Identity/ConfirmEmail";
import ForgotYourPassword from "./Identity/ForgotYourPassword";
import Login from './Identity/Login';
import Register from './Identity/Register';
import ResetPassword from "./Identity/ResetPassword";

import AboutUs from './AboutUs';
import ContactUs from './ContactUs';
import Home from './Home';
import NotFoundPage from './NotFoundPage';
import PrivacyPolicy from './PrivacyPolicy';
import UserAgreement from './UserAgreement';

import MasterRoutesGeneratedRoutes from "src/generated/views/MasterRoutesGeneratedRoutes";

export default function MasterRoutes(): JSX.Element {
    return (
        <Routes>
            <Route index element={<Home />} />

            <Route path="autologin" element={<AutoLogIn />} />
            <Route path="autoLogout" element={<AutoLogout />} />
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} />
            <Route path="confirmEmail" element={<ConfirmEmail />} />
            <Route path="forgotyourpassword" element={<ForgotYourPassword />} />
            <Route path="resetpassword" element={<ResetPassword />} />

            <Route path="aboutus" element={<AboutUs />} />
            <Route path="contactus" element={<ContactUs />} />
            <Route path="privacypolicy" element={<PrivacyPolicy />} />
            <Route path="useragreement" element={<UserAgreement />} />
            
            <Route path="*" element={<MasterRoutesGeneratedRoutes />} />

            <Route path="*" element={<NotFoundPage />} />
        </Routes>);
}

