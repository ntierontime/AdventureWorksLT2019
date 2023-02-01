import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit'

import { reducers } from './CombinedReducers'

const store = configureStore({
    reducer: reducers,
})

export type AppDispatch = typeof store.dispatch

export default store