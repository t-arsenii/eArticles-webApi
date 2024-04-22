import { IUser } from "./user"

export interface IArticle {
    id: string,
    user: IUser,
    publishedDate: string,
    title: string,
    description: string,
    content: string,
    contentType: string,
    category: string,
    imgUrl: string,
    tags: string[] | null,
}
export interface IArticleCreateReq {
    title: string,
    description: string,
    content: string,
    contentTypeId: string,
    categoryId: string,
    tagIds: string[] | null
    imgUrl: string,
}
export interface IArticleGetPageRes {
    items: IArticle[],
    totalCount: number
}
export interface IArticleCreateRes {
    id: string,
    user: IUser
    publishedDate: string,
    title: string,
    description: string,
    content: string,
    contentType: string,
    category: string,
    tags: string[] | null,
    imgUrl: string,
}