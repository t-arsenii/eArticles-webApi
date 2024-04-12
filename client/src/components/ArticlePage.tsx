import { useEffect, useState } from "react";
import { IArticle } from "../models/articles";
import { ArticleList } from "./ArticleList";
import axios from "axios";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Pagination, Box } from "@mui/material";
interface IArticleProps {
    url: string,
    isToken?: boolean,
    order?: string,
    articleType?: string,
    tags?: string[]
}
export default function ArticlePage({ url, isToken = false, order, articleType, tags }: IArticleProps) {
    const [articles, setArticles] = useState<IArticle[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const token = useSelector((state: RootState) => state.user.token);
    const ItemsPerPage = 6;
    const handlePageChange = (event: unknown, page: number) => {
        setCurrentPage(page)
    };
    const fetchData = async () => {
        try {
            const config = {
                headers: {
                    Authorization: isToken ? `Bearer ${token}` : undefined
                }
            };
            let finalUrl = `${url}?pageNumber=${currentPage}&pageSize=${ItemsPerPage}`;
            if (order) {
                finalUrl += `&order=${order}`;
            }
            if (articleType) {
                finalUrl += `&articleType=${articleType}`;
            }
            if (tags) {
                tags.forEach(tag => {
                    finalUrl += `&tags=${tag}`;
                })
            }

            const res = await axios.get<{ items: IArticle[], totalCount: number }>(finalUrl, config)
            const { items, totalCount } = res.data;
            setArticles(items)
            setTotalPages(Math.ceil(totalCount / ItemsPerPage));

        } catch (err) {
            console.log("Error fetching data", err)
        }
    }
    useEffect(() => {
        fetchData()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [currentPage])
    return (
        <>
            <ArticleList articles={articles} />
            <Box>
                <Pagination sx={{ display: "flex", justifyContent: "center" }} count={totalPages} color="primary" onChange={handlePageChange} />
            </Box>
        </>
    )
}

