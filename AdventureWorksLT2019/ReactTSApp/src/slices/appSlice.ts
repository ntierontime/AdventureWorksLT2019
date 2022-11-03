import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { initialState } from '../shared/viewModels/IAppState';

const appSlice = createSlice({
    name: 'app',
    initialState,
    reducers: {
        openAppDrawer(state) {
            state.drawerOpen = true;
        },
        closeAppDrawer(state) {
            state.drawerOpen = false;
        },

        setAppBartitle(state, action: PayloadAction<string>) {
            state.appBarTitle = action.payload;
        },

        showSpinner(state) {
            state.loading = true;
        },
        closeSpinner(state) {
            state.loading = false;
        }
    }
});

export const { openAppDrawer, closeAppDrawer, showSpinner, closeSpinner } = appSlice.actions;
export default appSlice.reducer;
