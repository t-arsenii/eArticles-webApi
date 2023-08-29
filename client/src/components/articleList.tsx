import { Grid, Pagination } from "@mui/material";
import { IArticle } from "../models/articles";
import { Article } from "./article";
import { Container } from '@mui/material';

interface ArticleProps {
    articles: IArticle[]
}
export function ArticleList({ articles }: ArticleProps) {
    return (
        <div>
            <Container>
                {articles.map(article => (
                    <Grid md={4} key={article.id}>
                        <Article article={article} />
                    </Grid>
                ))}
            </Container>
            <Pagination count={10} color="primary" />
        </div>
    )
}