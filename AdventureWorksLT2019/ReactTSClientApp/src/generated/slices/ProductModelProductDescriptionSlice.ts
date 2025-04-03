import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IListResponse } from "src/shared/apis/IListResponse";
import { IBulkUpdateRequest } from "src/shared/apis/IBulkUpdateRequest";
import { IMultiItemsCUDRequest } from "src/shared/apis/IMultiItemsCUDRequest";
import { ItemUIStatus } from "src/shared/dataModels/ItemUIStatus";
import { defaultPaginationResponse } from "src/shared/dataModels/IPaginationResponse";
import { PaginationOptions } from "src/shared/dataModels/PaginationOptions";
import { RootState } from "src/store/CombinedReducers";

import { IProductModelProductDescriptionDataModel } from 'src/dataModels/IProductModelProductDescriptionDataModel';
import { defaultIProductModelProductDescriptionAdvancedQuery, getRouteParamsOfIProductModelProductDescriptionIdentifier, IProductModelProductDescriptionAdvancedQuery, IProductModelProductDescriptionIdentifier } from 'src/dataModels/IProductModelProductDescriptionQueries';
import { productModelProductDescriptionApi } from "src/generated/apiClients/ProductModelProductDescriptionApi";

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

const ProductModelProductDescriptionSlice = createSlice({
    name: "productModelProductDescriptionSlice",
    initialState: entityAdapter.getInitialState({
        pagination: defaultPaginationResponse(),
        advancedQuery: defaultIProductModelProductDescriptionAdvancedQuery(),
	}),
    reducers: {
        /* any other state updates here */
        setIProductModelProductDescriptionAdvancedQuery: (state, action: PayloadAction<IProductModelProductDescriptionAdvancedQuery>) =>{
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
    }
});

export const productModelProductDescriptionSelectors = entityAdapter.getSelectors<RootState>(
    state => state.productModelProductDescriptionList
)

export const { setIProductModelProductDescriptionAdvancedQuery } = ProductModelProductDescriptionSlice.actions;

export default ProductModelProductDescriptionSlice.reducer; // should import as productModelProductDescriptionSlice

