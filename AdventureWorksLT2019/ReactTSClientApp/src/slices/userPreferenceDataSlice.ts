import * as React from 'react';
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { PaletteMode } from '@mui/material';

export interface IUserPreference
{
    theme: PaletteMode;
    language: string;
}
const defaultUserPreference: IUserPreference = {
    theme: 'light',
    language: '',
}
const userPreferenceDataSlice = createSlice({
    name: "userPreferenceDataSlice",
    initialState: defaultUserPreference,
    reducers: {
        /* any other state updates here */
        setTheme: (state, action: PayloadAction<PaletteMode>) => {
            state.theme = action.payload;
        },
        setLanguage: (state, action: PayloadAction<string>) =>{
            state.language = action.payload;
        },
    },
    extraReducers: builder => {
        
    }
});

export const { setTheme, setLanguage, } = userPreferenceDataSlice.actions;

export default userPreferenceDataSlice.reducer;
