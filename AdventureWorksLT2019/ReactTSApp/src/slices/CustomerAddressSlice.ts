import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { ICustomerAddressDataModel } from 'src/dataModels/ICustomerAddressDataModel';
import { getRouteParamsOfICustomerAddressIdentifier, ICustomerAddressAdvancedQuery, ICustomerAddressIdentifier } from 'src/dataModels/ICustomerAddressQueries';
import { customerAddressApi } from "src/apiClients/CustomerAddressApi";

const entityAdapter = createEntityAdapter<ICustomerAddressDataModel>({
    selectId: (item: ICustomerAddressDataModel) => getRouteParamsOfICustomerAddressIdentifier(item),
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})

export const upsertMany = createAsyncThunk(
    'upsertManyCustomerAddress',
    async (listResponse: IListResponse<ICustomerAddressDataModel[]>, { dispatch }) => {
        return listResponse;
    }
)


export const search = createAsyncThunk(
    'searchCustomerAddress',
    async (advancedQuery: ICustomerAddressAdvancedQuery, { dispatch }) => {
        const response = await customerAddressApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const put = createAsyncThunk(
    'putCustomerAddress',
    async (params: { identifier: ICustomerAddressIdentifier, data: ICustomerAddressDataModel }, { dispatch }) => {
        const response = await customerAddressApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'getCustomerAddress',
    async (identifier: ICustomerAddressIdentifier, { dispatch }) => {
        const response = await customerAddressApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'postCustomerAddress',
    async (data: ICustomerAddressDataModel, { dispatch }) => {
        const response = await customerAddressApi.Post(data);
        return response;
    }
)

const CustomerAddressSlice = createSlice({
    name: "customerAddressSlice",
    initialState: entityAdapter.getInitialState({
        pagination: defaultPaginationResponse(),
	}),
    reducers: {
        /* any other state updates here */
    },
    extraReducers: builder => {
        builder.addCase(upsertMany.pending, (state) => {
            // console.log("upsertMany.pending");
        });
        builder.addCase(upsertMany.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                if (payload.pagination.pageIndex === 1 ||
                    payload.pagination.paginationOption !== PaginationOptions.LoadMore) {
                    // TODO: update pagination
                    entityAdapter.removeAll(state);
                }
                entityAdapter.upsertMany(state, payload.responseBody);
                state.pagination = payload.pagination;
            }
            else {

            }
            // console.log("upsertMany.fulfilled");
        });
        builder.addCase(upsertMany.rejected, (state, action) => {
            // console.log("upsertMany.rejected");
        });


        builder.addCase(search.pending, (state) => {
            // console.log("search.pending");
        });
        builder.addCase(search.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                if (payload.pagination.pageIndex === 1 ||
                    payload.pagination.paginationOption !== PaginationOptions.LoadMore) {
                    // TODO: update pagination
                    entityAdapter.removeAll(state);
                }
                entityAdapter.upsertMany(state, payload.responseBody);
                state.pagination = payload.pagination;
            }
            else {

            }
            // console.log("search.fulfilled");
        });
        builder.addCase(search.rejected, (state, action) => {
            // console.log("search.rejected");
        });

        builder.addCase(put.pending, (state) => {

            // console.log("put.pending");
        });
        builder.addCase(put.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                entityAdapter.upsertOne(state, { ...payload.responseBody, itemUIStatus______: ItemUIStatus.Updated });
            }
            // console.log("put.fulfilled");
        });
        builder.addCase(put.rejected, (state, action) => {

            // console.log("put.rejected");
        });

        builder.addCase(get.pending, (state) => {

            // console.log("get.pending");
        });
        builder.addCase(get.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                entityAdapter.upsertOne(state, payload.responseBody);
            }
            // console.log("get.fulfilled");
        });
        builder.addCase(get.rejected, (state, action) => {

            // console.log("get.rejected");
        });

        builder.addCase(post.pending, (state) => {

            // console.log("post.pending");
        });
        builder.addCase(post.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                entityAdapter.upsertOne(state, { ...payload.responseBody, itemUIStatus______: ItemUIStatus.New });
            }
            // console.log("post.fulfilled");
        });
        builder.addCase(post.rejected, (state, action) => {

            // console.log("post.rejected");
        });
    }
});

export const customerAddressSelectors = entityAdapter.getSelectors<RootState>(
    state => state.customerAddressList
)
export default CustomerAddressSlice.reducer; // should import as customerAddressSlice

