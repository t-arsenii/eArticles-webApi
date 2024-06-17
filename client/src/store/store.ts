import { configureStore } from '@reduxjs/toolkit'
import userReducer from './userStore'
import localDataRecucer from './localDataStore'
import articlesReducer from './articlesStore'
import { useDispatch } from 'react-redux'

export const store = configureStore({
    reducer: {
        user: userReducer,
        localData: localDataRecucer,
        articles: articlesReducer
    },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>()