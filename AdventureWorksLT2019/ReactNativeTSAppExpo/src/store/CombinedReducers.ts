import { combineReducers } from "@reduxjs/toolkit";

import buildVersionList from '../slices/BuildVersionSlice'

export const reducers = combineReducers({
    buildVersionList,
});

export type RootState = ReturnType<typeof reducers>

