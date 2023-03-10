import { Routes, Route } from "react-router-dom";
import PrivateRoute from "src/shared/views/PrivateRoute";
import AutoLogIn from "./AutoLogIn";
import Home from "./Home";
import LoginPage from "../shared/views/Account/Login";
import RegisterPage from "../shared/views/Account/Register";
import NotFoundPage from "./NotFoundPage";
import PrivateRouteTestPage from "./PrivateRouteTestPage";

import AboutUs from "src/shared/views/Common/AboutUs";
import ContactUs from "src/shared/views/Common/ContactUs";
import PrivacyPolicy from "src/shared/views/Common/PrivacyPolicy";
import UserAgreement from "src/shared/views/Common/UserAgreement";

import BuildVersionRoutesPartial from 'src/routePartials/BuildVersionRoutesPartial'
import ErrorLogRoutesPartial from 'src/routePartials/ErrorLogRoutesPartial'
import AddressRoutesPartial from 'src/routePartials/AddressRoutesPartial'
import CustomerRoutesPartial from 'src/routePartials/CustomerRoutesPartial'
import CustomerAddressRoutesPartial from 'src/routePartials/CustomerAddressRoutesPartial'
import ProductRoutesPartial from 'src/routePartials/ProductRoutesPartial'
import ProductCategoryRoutesPartial from 'src/routePartials/ProductCategoryRoutesPartial'
import ProductDescriptionRoutesPartial from 'src/routePartials/ProductDescriptionRoutesPartial'
import ProductModelRoutesPartial from 'src/routePartials/ProductModelRoutesPartial'
import ProductModelProductDescriptionRoutesPartial from 'src/routePartials/ProductModelProductDescriptionRoutesPartial'
import SalesOrderDetailRoutesPartial from 'src/routePartials/SalesOrderDetailRoutesPartial'
import SalesOrderHeaderRoutesPartial from 'src/routePartials/SalesOrderHeaderRoutesPartial'
import CompareHtmlTablePage from "./ProductModel/CompareHtmlTablePage";

export default function MasterRoutes(): JSX.Element {
    return (
        <Routes>
            <Route index element={<Home />} />
            <Route path="autologin" element={<AutoLogIn />} />
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegisterPage />} />
            <Route path="aboutus" element={<AboutUs />} />
            <Route path="contactus" element={<ContactUs />} />
            <Route path="privacypolicy" element={<PrivacyPolicy />} />
            <Route path="useragreement" element={<UserAgreement />} />
            <Route path="PrivateRouteTestPage" element={
                <PrivateRoute>
                    <PrivateRouteTestPage />
                </PrivateRoute>} />

            <Route path="BuildVersion/*" element={<BuildVersionRoutesPartial />} />
            <Route path="ErrorLog/*" element={<ErrorLogRoutesPartial />} />
            <Route path="Address/*" element={<AddressRoutesPartial />} />
            <Route path="Customer/*" element={<CustomerRoutesPartial />} />
            <Route path="CustomerAddress/*" element={<CustomerAddressRoutesPartial />} />
            <Route path="Product/*" element={<ProductRoutesPartial />} />
            <Route path="ProductCategory/*" element={<ProductCategoryRoutesPartial />} />
            <Route path="ProductDescription/*" element={<ProductDescriptionRoutesPartial />} />
            <Route path="ProductModel/*" element={<ProductModelRoutesPartial />} />
            <Route path="ProductModelProductDescription/*" element={<ProductModelProductDescriptionRoutesPartial />} />
            <Route path="SalesOrderDetail/*" element={<SalesOrderDetailRoutesPartial />} />
            <Route path="SalesOrderHeader/*" element={<SalesOrderHeaderRoutesPartial />} />

            <Route path="*" element={<NotFoundPage />} />
        </Routes>);
}

