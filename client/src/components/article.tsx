import { Link } from "react-router-dom";
import { IArticle } from "../models/articles"
import { Card, CardContent, CardMedia, Typography, Chip, Box, Theme, styled } from '@mui/material';
const StyledCard = styled(Card)(({ theme }: { theme: Theme }) => ({
    // Add your card styles here
    '&:hover': {
        boxShadow: theme.shadows[8], 
        textDecoration: 'none', 
    },
    height:"100%"
}));
interface ArticleProps {
    article: IArticle
}
export function Article({ article }: ArticleProps) {
    const { title, description, content, publishedDate, imgUrl, articleTags } = article;
    return (
        <Link style={{ textDecoration: "none" }} to={`/article/${article.id}`}>
            <StyledCard>
                <CardMedia
                    component="img"
                    alt={title}
                    height="200px"
                    image={imgUrl}
                />
                <CardContent>
                    <Typography variant="h6" component="div">
                        {title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {description}
                    </Typography>
                    {/* <Typography variant="body1" color="text.primary">
                    {content}
                </Typography> */}
                    <Typography variant="caption" color="text.secondary">
                        Published on: {publishedDate}
                    </Typography>
                    {articleTags && <div>
                        {articleTags.map(tag => (
                            <Chip key={tag} label={tag} variant="outlined" sx={{ margin: '2px' }} />
                        ))}
                    </div>}
                </CardContent>
            </StyledCard>
        </Link>
    );
}