import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { IProductDescriptionDataModel } from 'src/dataModels/IProductDescriptionDataModel';
import { IProductDescriptionAdvancedQuery, IProductDescriptionIdentifier } from 'src/dataModels/IProductDescriptionQueries';
import { productDescriptionApi } from "src/apiClients/ProductDescriptionApi";

const entityAdapter = createEntityAdapter<IProductDescriptionDataModel>({
    selectId: (item: IProductDescriptionDataModel) => item.productDescriptionID,
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})


export const search = createAsyncThunk(
    'search',
    async (advancedQuery: IProductDescriptionAdvancedQuery, { dispatch }) => {
        const response = await productDescriptionApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const getCompositeModel = createAsyncThunk(
    'getCompositeModel',
    async (identifier: IProductDescriptionIdentifier, { dispatch }) => {
        const response = await productDescriptionApi.GetCompositeModel(identifier);
        return response;
    }
)

export const bulkDelete = createAsyncThunk(
    'bulkDelete',
    async (identifiers: IProductDescriptionIdentifier[], { dispatch }) => {
        const response = await productDescriptionApi.BulkDelete(identifiers);
        return { response, identifiers };
        // return { response: {status: 'OK'}, identifiers }; // for testing
    }
)

export const multiItemsCUD = createAsyncThunk(
    'multiItemsCUD',
    async (params: IMultiItemsCUDRequest<IProductDescriptionIdentifier, IProductDescriptionDataModel>, { dispatch }) => {
        const response = await productDescriptionApi.MultiItemsCUD(params);
        return response;
    }
)

export const put = createAsyncThunk(
    'put',
    async (params: { identifier: IProductDescriptionIdentifier, data: IProductDescriptionDataModel }, { dispatch }) => {
        const response = await productDescriptionApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'get',
    async (identifier: IProductDescriptionIdentifier, { dispatch }) => {
        const response = await productDescriptionApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'post',
    async (data: IProductDescriptionDataModel, { dispatch }) => {
        const response = await productDescriptionApi.Post(data);
        return response;
    }
)

export const delete1 = createAsyncThunk(
    'delete',
    async (identifier: IProductDescriptionIdentifier, { dispatch }) => {
        const response = await productDescriptionApi.Delete(identifier);
        return { response, identifier };
    }
)

const ProductDescriptionSlice = createSlice({
    name: "productDescriptionSlice",
    initialState: entityAdapter.getInitialState({
        pagination: defaultPaginationResponse(),
    }), // createEntityAdapter Usage #1
    reducers: {
        /* any other state updates here */
    },
    extraReducers: builder => {

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
                entityAdapter.removeMany(state, payload.identifiers.map(identifier => identifier.productDescriptionID));
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
                        if (state.ids.find(oId => { return oId === item.productDescriptionID }) !== -1) {
                            return { ...item, itemUIStatus______: ItemUIStatus.Updated }
                        }
                        return { ...item, itemUIStatus______: ItemUIStatus.New }
                    }));
                }
                if (!!payload.responseBody.deleteItems) {
                    // TODO: how to remove many: 
                    entityAdapter.removeMany(state, payload.responseBody.deleteItems.map(item => { return item.productDescriptionID; }));
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
                entityAdapter.removeOne(state, payload.identifier.productDescriptionID);
            }
            // console.log("delete.fulfilled");
        });
        builder.addCase(delete1.rejected, (state, action) => {

            // console.log("delete.rejected");
        });
    }
});

// createEntityAdapter Usage #4
export const productDescriptionSelectors = entityAdapter.getSelectors<RootState>(
    state => state.productDescriptionList
)
export default ProductDescriptionSlice.reducer; // should import as productDescriptionSlice

