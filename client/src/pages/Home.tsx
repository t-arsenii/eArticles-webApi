import { useEffect, useState } from "react";
import { IArticle, IArticleResPage } from "../models/articles";
import { ArticleList } from "../components/ArticleList";
import axios from "axios";
import ArticlePage from "../components/ArticlePage";
import { Typography, Box } from "@mui/material";
import { Article } from "../components/Article";
export function Home() {

    const [latestArticles, setLatestArticles] = useState<IArticle[]>([]);
    const [guideArticles, setGuideArticles] = useState<IArticle[]>([]);
    const [newsArticles, setNewsArticles] = useState<IArticle[]>([]);
    useEffect(() => {
        try {
            fetchLatestArticles();
        } catch (error: any) {
            console.log(error);
        }
        try {
            fetchGuideArticles();
        } catch (error: any) {
            console.log(error);
        }
        try {
            fetchNewsArticles();
        } catch (error: any) {
            console.log(error);
        }
    }, []);
    useEffect(() => {
        console.log(latestArticles);
    }, [latestArticles]);

    const fetchLatestArticles = async () => {
        const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date`);
        setLatestArticles(res.data.items);
    }
    const fetchGuideArticles = async () => {
        const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date&articleType=guide`);
        setGuideArticles(res.data.items);
    }
    const fetchNewsArticles = async () => {
        const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date&articleType=news`);
        setNewsArticles(res.data.items);
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