import * as React from 'react';
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { PaletteMode } from '@mui/material';
import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';
import moment from 'moment';

export interface ISiteDataItem<TData>
{
    key: string;
    refreshDataTime: string;
    data: TData;
}

export interface ISiteData {
    buildVersions: ISiteDataItem<IBuildVersionDataModel[]>;
}

const defaultSiteData: ISiteData = {
    buildVersions: null,
} 

// save static data or slow change data
const siteDataSlice = createSlice({
    name: "siteDataSlice",
    initialState: defaultSiteData,
    reducers: {
        /* any other state updates here */
        setBuildVersionsSiteData: (state, action: PayloadAction<IBuildVersionDataModel[]>) => {
            let expiredDate = moment();
            expiredDate.add(24, "hours"); 
            state.buildVersions = { key: 'buildVersions', refreshDataTime: expiredDate.toString(), data: action.payload };
        },
    },
    extraReducers: builder => {
        
    }
});

export const { setBuildVersionsSiteData, } = siteDataSlice.actions;

export default siteDataSlice.reducer;
