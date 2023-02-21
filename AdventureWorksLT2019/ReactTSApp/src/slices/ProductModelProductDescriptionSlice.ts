import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { getRouteParamsOfIProductModelProductDescriptionIdentifier, IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionIdentifier } from 'src/dataModels/IProductModelProductDescriptionQueries';
import { productModelProductDescriptionApi } from "src/apiClients/ProductModelProductDescriptionApi";

const entityAdapter = createEntityAdapter<IProductModelProductDescriptionDataModel>({
    selectId: (item: IProductModelProductDescriptionDataModel) => getRouteParamsOfIProductModelProductDescriptionIdentifier(item),
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})

export const upsertMany = createAsyncThunk(
    'upsertManyProductModelProductDescription',
    async (listResponse: IListResponse<IProductModelProductDescriptionDataModel[]>, { dispatch }) => {
        return listResponse;
    }
)


export const search = createAsyncThunk(
    'searchProductModelProductDescription',
    async (advancedQuery: IProductModelProductDescriptionAdvancedQuery, { dispatch }) => {
        const response = await productModelProductDescriptionApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const put = createAsyncThunk(
    'putProductModelProductDescription',
    async (params: { identifier: IProductModelProductDescriptionIdentifier, data: IProductModelProductDescriptionDataModel }, { dispatch }) => {
        const response = await productModelProductDescriptionApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'getProductModelProductDescription',
    async (identifier: IProductModelProductDescriptionIdentifier, { dispatch }) => {
        const response = await productModelProductDescriptionApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'postProductModelProductDescription',
    async (data: IProductModelProductDescriptionDataModel, { dispatch }) => {
        const response = await productModelProductDescriptionApi.Post(data);
        return response;
    }
)

const ProductModelProductDescriptionSlice = createSlice({
    name: "productModelProductDescriptionSlice",
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

export const productModelProductDescriptionSelectors = entityAdapter.getSelectors<RootState>(
    state => state.productModelProductDescriptionList
)
export default ProductModelProductDescriptionSlice.reducer; // should import as productModelProductDescriptionSlice

