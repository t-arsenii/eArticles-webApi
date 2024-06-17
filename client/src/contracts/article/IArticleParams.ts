export interface IArticleParams {
    key: string
    pageNumber: number,
    pageSize: number,
    contentTypeId?: string,
    categoryId?: string,
    order?: string,
    tagIds?: string[]
}