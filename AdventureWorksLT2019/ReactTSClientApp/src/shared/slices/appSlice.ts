import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IGeoLocation } from "../dataModels/IGeoLocation";

const appSlice = createSlice({
    name: "appSlice",
    initialState: {
        geoLocation: {
            IPv4: "",
            city: "",
            country_code: "",
            country_name: "",
            latitude: 43.6555,
            longitude: -79.3626,
            postal: "M5A",
            state: "Ontario"
        }
    },
    reducers: {
        /* any other state updates here */
        setGeoLocation: (state, action: PayloadAction<IGeoLocation>) => {
            state.geoLocation.IPv4 = action.payload.IPv4;
            state.geoLocation.city = action.payload.city;
            state.geoLocation.country_code = action.payload.country_code;
            state.geoLocation.country_name = action.payload.country_name;
            state.geoLocation.latitude = action.payload.latitude;
            state.geoLocation.longitude = action.payload.longitude;
            state.geoLocation.postal = action.payload.postal;
            state.geoLocation.state =action.payload.state;
        }
    },
    extraReducers: builder => {
        
    }
});

export const { setGeoLocation } = appSlice.actions;

export default appSlice.reducer;
