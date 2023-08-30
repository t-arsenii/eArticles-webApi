import { Grid, Pagination } from "@mui/material";
import { IArticle } from "../models/articles";
import { Article } from "./Article";
import { Container } from '@mui/material';
import { ChangeEvent } from "react";
interface ArticleProps {
    articles: IArticle[],
    totalPages: number,
    handlePageChange: (event: ChangeEvent<unknown>, page: number) => void
}
export function ArticleList({articles, totalPages, handlePageChange}: ArticleProps) {
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