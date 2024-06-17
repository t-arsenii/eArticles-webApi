import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { IArticle } from "../contracts/article/IArticle";
import { IArticleParams } from "../contracts/article/IArticleParams";
import { IArticleGetPageResponse } from "../contracts/article/IArticleGetPageResponse";

interface IArticlePayload {
    key: string,
    articles: IArticle[]
}

export const getArticles = createAsyncThunk<IArticlePayload, IArticleParams, { rejectValue: { message: string } }>(
    "articles/getArticles",
    async (params: IArticleParams, thunkApi) => {
        try {
            let query = `http://localhost:5000/api/articles?pageNumber=${params.pageNumber}&pageSize=${params.pageSize}`;
            if (params.contentTypeId) {
                query += `&contentTypeId=${params.contentTypeId}`;
            }
            if (params.categoryId) {
                query += `&categoryId=${params.categoryId}`;
            }
            if (params.order) {
                query += `&order=${params.order}`;
            }
            if (params.tagIds && params.tagIds.length > 0) {
                for (const tag of params.tagIds) {
                    query += `&tagIds=${tag}`;
                }
            }
            const res = await axios.get<IArticleGetPageResponse>(query);
            const { data } = res;
            return { key: params.key, articles: data.items };
        } catch (err: any) {
            return thunkApi.rejectWithValue({ message: err.message });
        }
    }
);

const initialState: { [key: string]: IArticle[] } = {};

export const counterSlice = createSlice({
    name: "aritcles",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(getArticles.fulfilled, (state, action) => {
            if (!action.payload) {
                return;
            }
            state[action.payload.key] = action.payload.articles;
            console.log(state[action.payload.key])
        })
    }
})

export default counterSlice.reducer;