import { IUserInfo } from "./user"

export interface IArticle {
    id: string,
    userId: string,
    publishedDate: string,
    title: string,
    description: string,
    content: string,
    contentType: string,
    category: string,
    imgUrl: string,
    articleTags: string[] | null,
    user: IUserInfo
}
export interface IArticleReq {
    title: string,
    description: string,
    content: string,
    contentType: string,
    category: string,
    imgUrl: string,
    articleTags: string[] | null
}
export interface IArticleResPage {
    items: IArticle[],
    totalCount: number
}
export interface IArticleRes {
    id: string,
    publishedDate: string,
    userId: string,
    title: string,
    description: string,
    content: string,
    contentType: string,
    category: string,
    imgUrl: string,
    articleTags: string[] | null,
    user: IUserInfo
}