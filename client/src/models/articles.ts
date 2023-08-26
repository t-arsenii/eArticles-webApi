export interface IArticle {
    id: number
    publishedDate: string
    title: string
    description: string
    content: string
    articleType: string
    imgUrl: string
    articleTags: [string]
}