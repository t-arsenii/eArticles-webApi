export interface IArticleCreateForm {
    title: string,
    description: string,
    content: string,
    contentTypeId: string,
    categoryId: string,
    tagIds: string[] | null,
    image: FileList
}