import { useEffect, useState } from "react";
import { IArticle } from "../models/articles";
import { ArticleList } from "../components/ArticleList";
import axios from "axios";
import ArticlePage from "../components/ArticlePage";
export function Home() {
    return (
        <ArticlePage url={"http://localhost:5000/api/Articles"}/>
    )
}