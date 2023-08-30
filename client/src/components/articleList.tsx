import { Grid, Pagination } from "@mui/material";
import { IArticle } from "../models/articles";
import { Article } from "./article";
import { Container } from '@mui/material';
import { useEffect, useState } from 'react'
import axios from "axios";
interface ArticleProps {
    articles: IArticle[]
}
export function ArticleList() {
    const [articles, setArticles] = useState<IArticle[]>([])
    const [currentPage, setCurrentPage] = useState(1)
    const [totalPages, setTotalPages] = useState(0)
    const ItemsPerPage = 5
    const handlePageChange = (event: unknown, page: number) => {
        setCurrentPage(page)
    };
    const fetchData = async () => {
        try {
            console.log("making request")
            const res = await axios.get<{ items: IArticle[]; totalCount: number }>(`http://localhost:5000/api/Articles?pageNumber=${currentPage}&pageSize=${ItemsPerPage}`)
            const { items, totalCount } = res.data;
            setArticles(items)
            setTotalPages(Math.ceil(totalCount / ItemsPerPage));

        } catch (err) {
            console.log("Error fetching data", err)
        }
    }
    useEffect(() => {
        fetchData()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [currentPage])
    return (
        <Container>
            <Grid my={"5px"} container>
                {articles.map(article => (
                    <Grid key={article.id} item xs={4}>
                        <Article article={article} />
                    </Grid>
                ))}
            </Grid>
            <Pagination count={totalPages} color="primary" onChange={handlePageChange} />
        </Container>
    )
}