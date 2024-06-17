import { Grid, Pagination } from "@mui/material";
import { Article } from "./Article";
import { Container } from '@mui/material';
import { ChangeEvent } from "react";
import { IArticle } from "../contracts/article/IArticle";
interface ArticleProps {
    articles: IArticle[]
}
export function ArticleList({ articles }: ArticleProps) {
    return (
        <>
            {articles.length > 0 ? (
                <Container>
                    <Grid my={"5px"} container spacing={2}>
                        {articles.map(article => (
                            <Grid key={article.id} item xs={4}>
                                <Article article={article} />
                            </Grid>
                        ))}
                    </Grid>
                </Container>
            ) : (
                <div>No items</div>
            )}
        </>
    );
}