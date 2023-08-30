import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IUserInfo, IUserState } from '../models/user'

const initialState: IUserState = {
    userInfo: {
        firstName: '',
        lastName: '',
        userName: '',
        email: '',
        phoneNumber: ''
    },
    token: localStorage.getItem('token') || null,
}

export const counterSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        updateUser: (state, action: PayloadAction<IUserInfo>) => {
            state.userInfo = { ...state, ...action.payload };
        },
        updateToken: (state, action: PayloadAction<string | null>) => {
            state.token = action.payload;
        },
    },
})

export const { updateUser, updateToken } = counterSlice.actions

export default counterSlice.reducer