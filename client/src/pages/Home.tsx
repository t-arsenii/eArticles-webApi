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
        fetchLatestArticles();
        fetchGuideArticles();
        fetchNewsArticles();
    }, []);
    useEffect(() => {
        console.log(latestArticles);
    }, [latestArticles]);

    const fetchLatestArticles = async () => {
        try {
            const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date`);
            setLatestArticles(res.data.items);

        } catch (error) {
            console.log(error);
        }
    }
    const fetchGuideArticles = async () => {
        try {
            const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date&articleType=guide`);
            setGuideArticles(res.data.items);
        } catch (error) {
            console.log(error);
        }
    }
    const fetchNewsArticles = async () => {
        try {
            const res = await axios.get<IArticleResPage>(`http://localhost:5000/api/articles?pageNumber=1&pageSize=3&order=date&articleType=news`);
            setNewsArticles(res.data.items);
        } catch (error) {
            console.log(error);
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