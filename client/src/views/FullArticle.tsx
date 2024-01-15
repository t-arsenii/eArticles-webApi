import { Box, Stack, Typography } from "@mui/material"
import { IArticle, IArticleRes } from "../models/articles"
import { useEffect, useState } from "react"
import axios from "axios"
import { Link, useNavigate, useParams } from "react-router-dom"
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useSelector } from "react-redux"
import { RootState } from "../redux/store"
import { ConfirmationModal } from "../components/ConfirmationModal"
interface IFullArticle {
    article: IArticle
}
export default function FullArticle() {
    const [open, setOpen] = useState(false);
    const [article, setArticle] = useState<IArticle>()
    const navigate = useNavigate()
    const { id } = useParams()
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    const token = useSelector((state: RootState) => state.user.token)
    useEffect(() => {
        const fetchArticle = async () => {
            const resArticle = await axios.get<IArticleRes>(`http://localhost:5000/api/Articles/${id}`)
            const resArticleData: IArticle = { ...resArticle.data }
            setArticle(resArticleData)
        }
        fetchArticle()
    }, [])
    const handleDeleteButton = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        setOpen(true)
    }
    const confirmDelete = async () => {
        if (!article) {
            return
        }
        try {
            const resDeleteArticle = await axios.delete<string>(`http://localhost:5000/api/articles/${article.id}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
            return navigate(`/profile/${userInfo.userName}`)
        } catch (error) {
            if (axios.isAxiosError(error)) {
                if (error.response) {
                    console.error("Error status:", error.response.status);
                    console.error("Error response data:", error.response.data);
                } else {
                    console.error("Error message:", error.message);
                }
            } else {
                console.error("Error:", error);
            }
        }
    }
    return (
        <>
            {!article ?
                <>loading...</>
                :
                <>
                    < Stack >
                        {article.userId === userInfo.id &&
                            <Stack direction="row">
                                <Box onClick={handleDeleteButton}><DeleteIcon /></Box>
                                <Box><Link to={`/article/edit/${article.id}`}><EditIcon /></Link></Box>
                            </Stack>
                        }
                        <Box><Typography variant="h3">{article.title}</Typography></Box>
                        <Box>{article.description}</Box>
                        <Box>{article.publishedDate}</Box>
                        <Box>{article.articleType}</Box>
                        <Box><Typography variant="body1">{article.content}</Typography></Box>
                    </Stack >
                    <ConfirmationModal open={open} onClose={function (): void {
                        setOpen(false)
                    }} onConfirm={confirmDelete} title={"You sure to delete?"} content={"The article will be deleted"} />
                </>
            }
        </>
    )
}
