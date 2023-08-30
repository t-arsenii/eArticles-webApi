import { IArticle } from "../models/articles"
import { Card, CardContent, CardMedia, Typography, Chip, Box } from '@mui/material';

interface ArticleProps {
    article: IArticle
}
export function Article({ article }: ArticleProps) {
    const { title, description, content, publishedDate, imgUrl, articleTags } = article;
    return (
        <Card>
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
        </Card>
    );
}