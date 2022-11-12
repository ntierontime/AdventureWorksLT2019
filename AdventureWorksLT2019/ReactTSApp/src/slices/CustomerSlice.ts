import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { ICustomerDataModel } from 'src/dataModels/ICustomerDataModel';
import { getRouteParamsOfICustomerIdentifier, ICustomerAdvancedQuery, ICustomerIdentifier } from 'src/dataModels/ICustomerQueries';
import { customerApi } from "src/apiClients/CustomerApi";

const entityAdapter = createEntityAdapter<ICustomerDataModel>({
    selectId: (item: ICustomerDataModel) => getRouteParamsOfICustomerIdentifier(item),
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})

export const upsertMany = createAsyncThunk(
    'upsertManyCustomer',
    async (listResponse: IListResponse<ICustomerDataModel[]>, { dispatch }) => {
        return listResponse;
    }
)


export const search = createAsyncThunk(
    'searchCustomer',
    async (advancedQuery: ICustomerAdvancedQuery, { dispatch }) => {
        const response = await customerApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const getCompositeModel = createAsyncThunk(
    'getCustomerCompositeModel',
    async (identifier: ICustomerIdentifier, { dispatch }) => {
        const response = await customerApi.GetCompositeModel(identifier);
        return response;
    }
)

export const bulkUpdate = createAsyncThunk(
    'bulkUpdateCustomer',
    async (params: IBulkUpdateRequest<ICustomerIdentifier, ICustomerDataModel>, { dispatch }) => {
        const response = await customerApi.BulkUpdate(params);
        return response;
    }
)

export const put = createAsyncThunk(
    'putCustomer',
    async (params: { identifier: ICustomerIdentifier, data: ICustomerDataModel }, { dispatch }) => {
        const response = await customerApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'getCustomer',
    async (identifier: ICustomerIdentifier, { dispatch }) => {
        const response = await customerApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'postCustomer',
    async (data: ICustomerDataModel, { dispatch }) => {
        const response = await customerApi.Post(data);
        return response;
    }
)

const CustomerSlice = createSlice({
    name: "customerSlice",
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

        builder.addCase(getCompositeModel.pending, (state) => {

            // console.log("getCompositeModel.pending");
        });
        builder.addCase(getCompositeModel.fulfilled, (state, { payload }) => {
            // TODO: how?
            // console.log("getCompositeModel.fulfilled");
        });
        builder.addCase(getCompositeModel.rejected, (state, action) => {

            // console.log("getCompositeModel.rejected");
        });

        builder.addCase(bulkUpdate.pending, (state) => {

            // console.log("bulkUpdate.pending");
        });
        builder.addCase(bulkUpdate.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                entityAdapter.upsertMany(state, payload.responseBody.map(item => { return { ...item, itemUIStatus: ItemUIStatus.Updated }; }));
            }
            // console.log("bulkUpdate.fulfilled");
        });
        builder.addCase(bulkUpdate.rejected, (state, action) => {

            // console.log("bulkUpdate.rejected");
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

export const customerSelectors = entityAdapter.getSelectors<RootState>(
    state => state.customerList
)
export default CustomerSlice.reducer; // should import as customerSlice

