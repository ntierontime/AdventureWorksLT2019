import { AnyAction, createSlice, Dispatch, PayloadAction, ThunkDispatch } from "@reduxjs/toolkit";
import { availabilityFilteredByIndexes } from "src/shared/availabilityUtility";
import { siteDataApi } from 'src/apiClients/SiteDataApi';

//import { IBuildVersionDataModel } from 'src/dataModels/IBuildVersionDataModel';

export interface ISiteDataItem<TData>
{
    refreshDataTime: string; // Non-Serializable issue when use Date
    data: TData;
}

export interface ISiteData {

    // buildVersions: ISiteDataItem<IBuildVersionDataModel[]>;
}

const defaultSiteData: ISiteData = {

    // buildVersions: null,
} 



// save static data or slow change data
const siteDataSlice = createSlice({
    name: "siteDataSlice",
    initialState: defaultSiteData,
    reducers: {
        clearSiteData: (state) => {
            state = defaultSiteData;
        },

        /* any other state updates here */
        //setBuildVersionsSiteData: (state, action: PayloadAction<IBuildVersionDataModel[]>) => {
        //    let expiredDate = dayjs();
        //    expiredDate.add(24, "hours"); 
        //    state.buildVersions = { key: 'buildVersions', refreshDataTime: expiredDate.toString(), data: action.payload };
        //},
    },
    // extraReducers: builder => {
    // }
});

export const {
    clearSiteData,

} = siteDataSlice.actions;

export default siteDataSlice.reducer;

