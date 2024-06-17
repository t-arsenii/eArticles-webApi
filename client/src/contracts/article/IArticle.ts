import { IUser } from "../user/IUser";

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