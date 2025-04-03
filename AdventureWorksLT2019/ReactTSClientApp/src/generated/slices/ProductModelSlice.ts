import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { IProductModelDataModel } from 'src/dataModels/IProductModelDataModel';
import { defaultIProductModelAdvancedQuery, getRouteParamsOfIProductModelIdentifier, IProductModelAdvancedQuery, IProductModelIdentifier } from 'src/dataModels/IProductModelQueries';
import { productModelApi } from "src/generated/apiClients/ProductModelApi";

const entityAdapter = createEntityAdapter<IProductModelDataModel>({
    selectId: (item: IProductModelDataModel) => getRouteParamsOfIProductModelIdentifier(item),
    // Keep the "all IDs" array sorted based on book titles
    // sortComparer: (a, b) => a.text.localeCompare(b.text), 
})

export const upsertMany = createAsyncThunk(
    'upsertManyProductModel',
    async (listResponse: IListResponse<IProductModelDataModel[]>, { dispatch }) => {
        return listResponse;
    }
)


export const search = createAsyncThunk(
    'searchProductModel',
    async (advancedQuery: IProductModelAdvancedQuery, { dispatch }) => {
        const response = await productModelApi.Search(advancedQuery);
        // console.log(response);
        return response;
    }
)

export const put = createAsyncThunk(
    'putProductModel',
    async (params: { identifier: IProductModelIdentifier, data: IProductModelDataModel }, { dispatch }) => {
        const response = await productModelApi.Put(params.identifier, params.data);
        return response;
    }
)

export const get = createAsyncThunk(
    'getProductModel',
    async (identifier: IProductModelIdentifier, { dispatch }) => {
        const response = await productModelApi.Get(identifier);
        return response;
    }
)

export const post = createAsyncThunk(
    'postProductModel',
    async (data: IProductModelDataModel, { dispatch }) => {
        const response = await productModelApi.Post(data);
        return response;
    }
)

const ProductModelSlice = createSlice({
    name: "productModelSlice",
    initialState: entityAdapter.getInitialState({
        pagination: defaultPaginationResponse(),
        advancedQuery: defaultIProductModelAdvancedQuery(),
	}),
    reducers: {
        /* any other state updates here */
        setIProductModelAdvancedQuery: (state, action: PayloadAction<IProductModelAdvancedQuery>) =>{
            state.advancedQuery = action.payload;
            // console.log(state.advancedQuery);
            // console.log(action.payload);
        },
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

export const productModelSelectors = entityAdapter.getSelectors<RootState>(
    state => state.productModelList
)

export const { setIProductModelAdvancedQuery } = ProductModelSlice.actions;

export default ProductModelSlice.reducer; // should import as productModelSlice

