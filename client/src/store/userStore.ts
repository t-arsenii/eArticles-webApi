import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IUser, IUserState } from '../models/user'
import axios from 'axios'

export const getMe = createAsyncThunk<IUser, string, { rejectValue: { message: string } }>(
    "users/getMe",
    async (token: string, thunkApi) => {
        try {
            const res = await axios.get<IUser>("http://localhost:5000/api/users", {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
            const { data } = res;
            return data;
        } catch (err: any) {
            return thunkApi.rejectWithValue({ message: err.message });
        }
    }
);

const initialState: IUserState = {
    user: {
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
            state.user = { ...state, ...action.payload };
        },
        updateToken: (state, action: PayloadAction<string | null>) => {
            state.token = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getMe.fulfilled, (state, action) => {
            if (action.payload) {
                state.user = action.payload;
            }
        }
        )
    }
})

export const { updateUser, updateToken } = counterSlice.actions

export default counterSlice.reducer