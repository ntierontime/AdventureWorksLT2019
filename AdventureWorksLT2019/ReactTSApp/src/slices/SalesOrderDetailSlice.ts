import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { ISalesOrderDetailDataModel } from 'src/dataModels/ISalesOrderDetailDataModel';
import { ISalesOrderDetailCompositeModel } from "src/dataModels/ISalesOrderDetailCompositeModel";
import { getRouteParamsOfISalesOrderDetailIdentifier, ISalesOrderDetailAdvancedQuery, ISalesOrderDetailIdentifier } from 'src/dataModels/ISalesOrderDetailQueries';
import { salesOrderDetailApi } from "src/apiClients/SalesOrderDetailApi";

const entityAdapter = createEntityAdapter<ISalesOrderDetailDataModel>({
    selectId: (item: ISalesOrderDetailDataModel) => getRouteParamsOfISalesOrderDetailIdentifier(item),
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})

export const upsertMany = createAsyncThunk(
    'upsertManySalesOrderDetail',
    async (listResponse: IListResponse<ISalesOrderDetailDataModel[]>, { dispatch }) => {
        return listResponse;
    }
)


export const search = createAsyncThunk(
    'searchSalesOrderDetail',
    async (advancedQuery: ISalesOrderDetailAdvancedQuery, { dispatch }) => {
        const response = await salesOrderDetailApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const getCompositeModel = createAsyncThunk(
    'getSalesOrderDetailCompositeModel',
    async (identifier: ISalesOrderDetailIdentifier, { dispatch }) => {
        const response = await salesOrderDetailApi.GetCompositeModel(identifier);
        return response;
    }
)

export const bulkDelete = createAsyncThunk(
    'bulkDeleteSalesOrderDetail',
    async (identifiers: ISalesOrderDetailIdentifier[], { dispatch }) => {
        const response = await salesOrderDetailApi.BulkDelete(identifiers);
        return { response, identifiers };
        // return { response: {status: 'OK'}, identifiers }; // for testing
    }
)

export const multiItemsCUD = createAsyncThunk(
    'multiItemsCUDSalesOrderDetail',
    async (params: IMultiItemsCUDRequest<ISalesOrderDetailIdentifier, ISalesOrderDetailDataModel>, { dispatch }) => {
        const response = await salesOrderDetailApi.MultiItemsCUD(params);
        return response;
    }
)

export const put = createAsyncThunk(
    'putSalesOrderDetail',
    async (params: { identifier: ISalesOrderDetailIdentifier, data: ISalesOrderDetailDataModel }, { dispatch }) => {
        const response = await salesOrderDetailApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'getSalesOrderDetail',
    async (identifier: ISalesOrderDetailIdentifier, { dispatch }) => {
        const response = await salesOrderDetailApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'postSalesOrderDetail',
    async (data: ISalesOrderDetailDataModel, { dispatch }) => {
        const response = await salesOrderDetailApi.Post(data);
        return response;
    }
)

export const delete1 = createAsyncThunk(
    'deleteSalesOrderDetail',
    async (identifier: ISalesOrderDetailIdentifier, { dispatch }) => {
        const response = await salesOrderDetailApi.Delete(identifier);
        return { response, identifier };
    }
)

export const createComposite = createAsyncThunk(
    'createCompositeSalesOrderDetail',
    async (params: ISalesOrderDetailCompositeModel, { dispatch }) => {
        const response = await salesOrderDetailApi.CreateComposite(params);
        return response;
    }
)

const SalesOrderDetailSlice = createSlice({
    name: "salesOrderDetailSlice",
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

        builder.addCase(bulkDelete.pending, (state) => {

            // console.log("bulkDelete.pending");
        });
        builder.addCase(bulkDelete.fulfilled, (state, { payload }) => {
            if (!!payload && !!payload.response && payload.response.status === 'OK') {
                // TODO: how to remove multiple
                entityAdapter.removeMany(state, payload.identifiers.map(identifier => identifier.salesOrderID));
            }
            // console.log("bulkDelete.fulfilled");
        });
        builder.addCase(bulkDelete.rejected, (state, action) => {

            // console.log("bulkDelete.rejected");
        });

        builder.addCase(multiItemsCUD.pending, (state) => {

            // console.log("multiItemsCUD.pending");
        });
        builder.addCase(multiItemsCUD.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                if (!!payload.responseBody.updateItems) {
                    entityAdapter.upsertMany(state, payload.responseBody.updateItems.map(item => { return { ...item, itemUIStatus: ItemUIStatus.Updated }; }));
                }
                if (!!payload.responseBody.newItems) {
                    entityAdapter.upsertMany(state, payload.responseBody.newItems.map(item => { return { ...item, itemUIStatus: ItemUIStatus.New }; }));
                }
                if (!!payload.responseBody.mergeItems) {
                    entityAdapter.upsertMany(state, payload.responseBody.mergeItems.map(item => {
                        if (state.ids.find(oId => { return oId === item.salesOrderID }) !== -1) {
                            return { ...item, itemUIStatus______: ItemUIStatus.Updated }
                        }
                        return { ...item, itemUIStatus______: ItemUIStatus.New }
                    }));
                }
                if (!!payload.responseBody.deleteItems) {
                    // TODO: how to remove many: 
                    entityAdapter.removeMany(state, payload.responseBody.deleteItems.map(item => { return item.salesOrderID; }));
                }
            }
            // console.log("multiItemsCUD.fulfilled");
        });
        builder.addCase(multiItemsCUD.rejected, (state, action) => {

            // console.log("multiItemsCUD.rejected");
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

        builder.addCase(delete1.pending, (state) => {

            // console.log("delete.pending");
        });
        builder.addCase(delete1.fulfilled, (state, { payload }) => {
            if (!!payload && !!payload.response && payload.response.status === 'OK') {
                // TODO: how to remove one
                entityAdapter.removeOne(state, payload.identifier.salesOrderID);
            }
            // console.log("delete.fulfilled");
        });
        builder.addCase(delete1.rejected, (state, action) => {

            // console.log("delete.rejected");
        });

        builder.addCase(createComposite.pending, (state) => {

            // console.log("createComposite.pending");
        });
        builder.addCase(createComposite.fulfilled, (state, { payload }) => {
            if (!!payload && payload.status === 'OK') {
                entityAdapter.upsertOne(state, { ...payload.responseBody, itemUIStatus______: ItemUIStatus.New });
            }
            // console.log("createComposite.fulfilled");
        });
        builder.addCase(createComposite.rejected, (state, action) => {

            // console.log("createComposite.rejected");
        });
    }
});

export const salesOrderDetailSelectors = entityAdapter.getSelectors<RootState>(
    state => state.salesOrderDetailList
)
export default SalesOrderDetailSlice.reducer; // should import as salesOrderDetailSlice

