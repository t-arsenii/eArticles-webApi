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
    imgName: string,
    tags: string[] | null,
    averageRating: number
}
export interface IArticleCreateForm {
    title: string,
    description: string,
    content: string,
    contentTypeId: string,
    categoryId: string,
    tagIds: string[] | null,
    image: FileList
}
export interface IArticleCreateReq {
    title: string,
    description: string,
    content: string,
    contentTypeId: string,
    categoryId: string,
    tagIds: string[] | null
}
export interface IArticleGetPageRes {
    items: IArticle[],
    totalCount: number
}