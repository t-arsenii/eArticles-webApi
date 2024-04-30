import { PayloadAction, createSlice } from "@reduxjs/toolkit";

const initialState = {
    colorTheme: "bright"
}

export const counterSlice = createSlice({
    name: "localData",
    initialState,
    reducers: {
        updateColorTheme(state, action: PayloadAction<string>) {
            state.colorTheme = action.payload;
        }
    }
})

export const { updateColorTheme } = counterSlice.actions;

export default counterSlice.reducer;