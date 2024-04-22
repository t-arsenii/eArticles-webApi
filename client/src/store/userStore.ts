import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IUser, IUserState } from '../models/user'

const initialState: IUserState = {
    userInfo: {
        id: '',
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
        updateUser: (state, action: PayloadAction<IUser>) => {
            state.userInfo = { ...state, ...action.payload };
        },
        updateToken: (state, action: PayloadAction<string | null>) => {
            state.token = action.payload;
        },
    },
})

export const { updateUser, updateToken } = counterSlice.actions

export default counterSlice.reducer