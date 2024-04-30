import { Link } from "react-router-dom";
import { IArticle } from "../models/articles"
import { Card, CardContent, CardMedia, Typography, Chip, Box, Theme, styled, useTheme } from '@mui/material';
import { RootState } from "../store/store";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { getImagePath } from "../utils/getImagePath";
const StyledCard = styled(Card)(({ theme }: { theme: Theme}) => ({
    '&:hover': {
        boxShadow: theme.shadows[8],
        textDecoration: 'none'
    },
    height: "100%",
}));

interface ArticleProps {
    article: IArticle
}
export function Article({ article }: ArticleProps) {
    const userInfo = useSelector((state: RootState) => state.user.user)
    const theme = useTheme()
    return (
        <Link style={{ textDecoration: "none" }} to={`/article/${article.id}`}>
            <StyledCard theme={theme}>

                <CardMedia
                    component="img"
                    alt={article.title}
                    height="200px"
                    image={getImagePath(article.imgName)}
                />
                <CardContent>
                    <Typography variant="h6" component="div">
                        {article.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {article.description}
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                        Published on: {article.publishedDate}
                    </Typography>
                    {article.tags && <div>
                        {article.tags.map(tag => (
                            <Chip key={tag} label={tag} variant="outlined" sx={{ margin: '2px' }} />
                        ))}
                    </div>}
                </CardContent>
            </StyledCard>
        </Link>
    );
}