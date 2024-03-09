import { useEffect, useState } from "react";
import { IArticle } from "../models/articles";
import { ArticleList } from "../components/ArticleList";
import axios from "axios";
import ArticlePage from "../components/ArticlePage";
import { Typography, Box } from "@mui/material";
import { Article } from "../components/Article";
export function Home() {
    const fetchData = () => {

    }
    return (
        <>
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>LATEST</Typography>
            </Box>
            <ArticleList articles={[]} />
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>GUIDES</Typography>
            </Box>
            <ArticleList articles={[]} />
            <Box display={"flex"} justifyContent={"center"} marginTop={"20px"}>
                <Typography variant="h2" component={"h1"}>NEWS</Typography>
            </Box>
            <ArticleList articles={[]} />
        </>
    )
}