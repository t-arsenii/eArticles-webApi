export interface IArticle {
    id: string
    publishedDate: string
    title: string
    description: string
    content: string
    articleType: string
    imgUrl: string
    articleTags: string[] | null
}
export interface IArticleReq {
    title: string
    description: string
    content: string
    articleType: string
    imgUrl: string
    articleTags: string[] | null
}
export interface IArticleRes {
    id: string
    publishedDate: string
    title: string
    description: string
    content: string
    articleType: string
    imgUrl: string
    articleTags: string[] | null
}