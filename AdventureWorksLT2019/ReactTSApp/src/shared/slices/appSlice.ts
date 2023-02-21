import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IGeoLocation } from "../dataModels/IGeoLocation";

const appSlice = createSlice({
    name: "appSlice",
    initialState: {
        IPv4: "",
        city: "",
        country_code: "",
        country_name: "",
        latitude: 43.6555,
        longitude: -79.3626,
        postal:"M5A",
        state:"Ontario"
    },
    reducers: {
        /* any other state updates here */
        setGeoLocation: (state, action: PayloadAction<IGeoLocation>) => {
            state.IPv4 = action.payload.IPv4;
            state.city = action.payload.city;
            state.country_code = action.payload.country_code;
            state.country_name = action.payload.country_name;
            state.latitude = action.payload.latitude;
            state.longitude = action.payload.longitude;
            state.postal = action.payload.postal;
            state.state =action.payload.state;
        }
    },
    extraReducers: builder => {
        
    }
});

export const { setGeoLocation } = appSlice.actions;

export default appSlice.reducer;
