export interface IArticleCreateRequest {
    title: string,
    description: string,
    content: string,
    contentTypeId: string,
    categoryId: string,
    tagIds: string[] | null
}