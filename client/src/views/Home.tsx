import { useEffect, useState } from "react";
import { IArticle } from "../models/articles";
import { ArticleList } from "../components/ArticleList";
import axios from "axios";
import ArticlePage from "../components/ArticlePage";
import { Typography, Box } from "@mui/material";
export function Home() {
    return (
        <>
            <Box display={"flex"} justifyContent={"center"}>
                <Typography variant="h2" component={"h1"}>Explore</Typography>
            </Box>
            <ArticlePage url={"http://localhost:5000/api/Articles"} />
        </>
    )
}