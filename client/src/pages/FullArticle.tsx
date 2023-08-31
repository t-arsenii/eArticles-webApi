import { Box, Stack, Typography } from "@mui/material"
import { IArticle, IArticleRes } from "../models/articles"
import { useEffect, useState } from "react"
import axios from "axios"
import { useParams } from "react-router-dom"

interface IFullArticle {
    article: IArticle
}
export default function FullArticle() {
    const [article, setArticle] = useState<IArticle>()
    const { id } = useParams()
    useEffect(() => {
        const fetchArticle = async () => {
            const resArticle = await axios.get<IArticleRes>(`http://localhost:5000/api/Articles/${id}`)
            const resArticleData: IArticle = { ...resArticle.data }
            setArticle(resArticleData)
        }
        fetchArticle()
    }, [])
    return (
        <>
            {!article ?
                <>loading...</>
                :
                < Stack >
                    <Box><Typography variant="h3">{article.title}</Typography></Box>
                    <Box>{article.description}</Box>
                    <Box>{article.publishedDate}</Box>
                    <Box>{article.articleType}</Box>
                    <Box><Typography variant="body1">{article.content}</Typography></Box>
                </Stack >
            }
        </>
    )
}
