import { IArticle } from "./IArticle";

export interface IArticleGetPageResponse {
    items: IArticle[],
    totalCount: number
}