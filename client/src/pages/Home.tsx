import { useEffect, useState } from "react";
import { ArticleList } from "../components/ArticleList";
import axios from "axios";
import ArticlePage from "../components/ArticlePage";
import { Typography, Box } from "@mui/material";
import { Article } from "../components/Article";
import { IArticle } from "../contracts/article/IArticle";
import { IArticleGetPageResponse } from "../contracts/article/IArticleGetPageResponse";
import { AppDispatch, RootState, useAppDispatch } from "../store/store";
import { getArticles } from "../store/articlesStore";
import { IArticleParams } from "../contracts/article/IArticleParams";
import { useSelector } from "react-redux";
export function Home() {
    const dispatch: AppDispatch = useAppDispatch();
    // const [latestArticles, setLatestArticles] = useState<IArticle[]>([]);
    // const [guideArticles, setGuideArticles] = useState<IArticle[]>([]);
    // const [newsArticles, setNewsArticles] = useState<IArticle[]>([]);

    const latestArticles = useSelector((state: RootState) => state.articles)["latest"] ?? [];
    const guideArticles = useSelector((state: RootState) => state.articles)["guide"] ?? [];
    const newsArticles = useSelector((state: RootState) => state.articles)["news"] ?? [];
    useEffect(() => {
        fetchLatestArticles();
        fetchGuideArticles();
        fetchNewsArticles();
    }, []);

    const fetchLatestArticles = async () => {
        try {
            const params: IArticleParams = { key: "latest", pageNumber: 1, pageSize: 3, order: "date" };
            dispatch(getArticles(params));
        } catch (error) {
            console.error(error);
        }
    }
    const fetchGuideArticles = async () => {
        try {
            const params: IArticleParams = { key: "guide", pageNumber: 1, pageSize: 3, contentTypeId: "" };
            dispatch(getArticles(params));
        } catch (error) {
            console.error(error);
        }
    }
    const fetchNewsArticles = async () => {
        try {
            const params: IArticleParams = { key: "news", pageNumber: 1, pageSize: 3, contentTypeId: "" };
            dispatch(getArticles(params));
        } catch (error) {
            console.error(error);
        }
    }
    return (
        <>
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>LATEST</Typography>
            </Box>
            <ArticleList articles={latestArticles} />
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>GUIDES</Typography>
            </Box>
            <ArticleList articles={guideArticles} />
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>NEWS</Typography>
            </Box>
            <ArticleList articles={newsArticles} />
        </>
    )
}