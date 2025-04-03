import addressList from 'src/generated/slices/AddressSlice'
import buildVersionList from 'src/generated/slices/BuildVersionSlice'
import customerAddressList from 'src/generated/slices/CustomerAddressSlice'
import customerList from 'src/generated/slices/CustomerSlice'
import errorLogList from 'src/generated/slices/ErrorLogSlice'
import productCategoryList from 'src/generated/slices/ProductCategorySlice'
import productDescriptionList from 'src/generated/slices/ProductDescriptionSlice'
import productList from 'src/generated/slices/ProductSlice'
import productModelList from 'src/generated/slices/ProductModelSlice'
import productModelProductDescriptionList from 'src/generated/slices/ProductModelProductDescriptionSlice'
import salesOrderDetailList from 'src/generated/slices/SalesOrderDetailSlice'
import salesOrderHeaderList from 'src/generated/slices/SalesOrderHeaderSlice'

export const generatedCombinedReducers_BlackList = [
    "addressList",
    "buildVersionList",
    "customerAddressList",
    "customerList",
    "errorLogList",
    "productCategoryList",
    "productDescriptionList",
    "productList",
    "productModelList",
    "productModelProductDescriptionList",
    "salesOrderDetailList",
    "salesOrderHeaderList",
];

export const generatedCombineReducers = {
    addressList,
    buildVersionList,
    customerAddressList,
    customerList,
    errorLogList,
    productCategoryList,
    productDescriptionList,
    productList,
    productModelList,
    productModelProductDescriptionList,
    salesOrderDetailList,
    salesOrderHeaderList,
};


