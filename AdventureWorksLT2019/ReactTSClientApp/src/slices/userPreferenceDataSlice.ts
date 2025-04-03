import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { PaletteMode } from '@mui/material';
import { UIRouteLinkSetting } from 'src/shared/dataModels/UIRouteLinkSetting';

export enum AppDrawerOptions {
    None = 'None',

}

export interface AppDrawerSelection { option: AppDrawerOptions; searchMethodName?: string; uiRouteLinkSetting: UIRouteLinkSetting; }

export interface IUserPreference
{
    appDrawerOpen: boolean;
    theme: PaletteMode;
    language: string;
    currentAppDrawer: AppDrawerSelection;
}
const defaultUserPreference: IUserPreference = {
    appDrawerOpen: false,
    theme: 'light',
    language: '',
    currentAppDrawer: { option: AppDrawerOptions.None, uiRouteLinkSetting: null},
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
        setAppDrawerOpen: (state, action: PayloadAction<boolean>) =>{
            state.appDrawerOpen = action.payload;
        },
        setCurrentAppDrawer: (state, action: PayloadAction<AppDrawerSelection>) =>{
            state.currentAppDrawer = action.payload;
            state.appDrawerOpen = action.payload.option !== AppDrawerOptions.None;
        },
    },
    extraReducers: builder => {
        
    }
});

export const { setTheme, setLanguage, setAppDrawerOpen, setCurrentAppDrawer, } = userPreferenceDataSlice.actions;

export default userPreferenceDataSlice.reducer;

