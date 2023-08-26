import { IArticle } from "../models/articles"

interface ArticleProps {
    article: IArticle
}
export function Article({ article }: ArticleProps) {
    return (
        <div>
            {article.id}
            {article.title}
            {article.content}
        </div>
    )
}