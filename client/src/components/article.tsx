import { IArticle } from "../models/articles"

interface ArticleProps {
    article: IArticle
}
function article({article} : ArticleProps) {
    return (
        <div>article</div>
    )
}

export default article