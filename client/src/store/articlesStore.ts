import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { IArticle } from "../models/articles";
import axios from "axios";
interface IArticleParams {
    pageNumber: number,
    pageSize: number,
    contentTypeId: string,
    categoryId: string,
    order: string,
    tagIds: string[]
}
const initialState: { [key: string]: IArticle[] } = {}
export const getArticles = createAsyncThunk<IArticle, IArticleParams, { rejectValue: { message: string } }>(
    "articles/getArticles",
    async (params: IArticleParams, thunkApi) => {
        try {
            const res = await axios.get<IArticle>("http://localhost:5000/api/users", {
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
export const counterSlice = createSlice({
    name: "aritcles",
    initialState,
    reducers: {},
    extraReducers: (builder) => {

    }
})