import { combineReducers } from "@reduxjs/toolkit";

import app from "src/slices/appSlice";
import auth from "src/slices/authenticationSlice"

import buildVersionList from 'src/slices/BuildVersionSlice'
import errorLogList from 'src/slices/ErrorLogSlice'
import addressList from 'src/slices/AddressSlice'
import customerList from 'src/slices/CustomerSlice'
import customerAddressList from 'src/slices/CustomerAddressSlice'
import productList from 'src/slices/ProductSlice'
import productCategoryList from 'src/slices/ProductCategorySlice'
import productDescriptionList from 'src/slices/ProductDescriptionSlice'
import productModelList from 'src/slices/ProductModelSlice'
import productModelProductDescriptionList from 'src/slices/ProductModelProductDescriptionSlice'
import salesOrderDetailList from 'src/slices/SalesOrderDetailSlice'
import salesOrderHeaderList from 'src/slices/SalesOrderHeaderSlice'

export const reducers = combineReducers({
    app: app,
    auth: auth,

    buildVersionList,
    errorLogList,
    addressList,
    customerList,
    customerAddressList,
    productList,
    productCategoryList,
    productDescriptionList,
    productModelList,
    productModelProductDescriptionList,
    salesOrderDetailList,
    salesOrderHeaderList,
});

export type RootState = ReturnType<typeof reducers>

