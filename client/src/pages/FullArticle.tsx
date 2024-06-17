import { Box, Chip, IconButton, ListItem, Menu, MenuItem, Rating, Stack, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import axios from "axios"
import { Link, useNavigate, useParams } from "react-router-dom"
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { useSelector } from "react-redux"
import { RootState } from "../store/store"
import { ConfirmationModal } from "../components/ConfirmationModal"
import { getImagePath } from "../utils/getImagePath"
import { CommentsSection } from "../components/CommentsSection"
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { IArticle } from "../contracts/article/IArticle";
export default function FullArticle() {
    const [article, setArticle] = useState<IArticle>()
    const navigate = useNavigate()
    const { id } = useParams()
    const userInfo = useSelector((state: RootState) => state.user.user)
    const token = useSelector((state: RootState) => state.user.token)
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };
    useEffect(() => {
        const fetchArticle = async () => {
            const resArticle = await axios.get<IArticle>(`http://localhost:5000/api/Articles/${id}`)
            const resArticleData: IArticle = { ...resArticle.data }
            setArticle(resArticleData)
        }
        try {
            fetchArticle()
        } catch (e) {
            console.error(e);
        }
    }, [])
    // const handleDeleteButton = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    //     setOpen(true)
    // }
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
                    <Stack>
                        <Box sx={{ display: "flex", justifyContent: "center", marginY: "20px" }}>
                            <Box sx={{ height: "2px", backgroundColor: "black", width: "100%", margin: "0 10px", alignSelf: "center" }} />
                            <Box sx={{ color: "black", padding: "5px", width: "100px", textAlign: "center" }}>
                                <Typography sx={{ fontSize: "30px", fontStyle: "italic", lineHeight: "5px", verticalAlign: "middle" }}>{article.category}</Typography>
                            </Box>
                            <Box sx={{ height: "2px", backgroundColor: "black", width: "100%", margin: "0 10px", alignSelf: "center" }} />
                        </Box>
                        {/* {article.user.id === userInfo.id &&
                            <Stack direction="row">
                                <Box onClick={handleDeleteButton}><DeleteIcon /></Box>
                                <Box><Link to={`/article/edit/${article.id}`}><EditIcon /></Link></Box>
                            </Stack>
                        } */}
                        <Box>
                            <Typography sx={{ fontWeight: "bold" }} variant="h2">{article.title}</Typography>
                        </Box>
                        <Box>
                            <Typography variant="h5">{article.description}</Typography>
                        </Box>
                        <Box sx={{ marginY: "10px" }}>
                            <Box sx={{ backgroundColor: "purple", color: "white", padding: "5px", width: "100px", textAlign: "center" }}>
                                <Typography sx={{ fontSize: "15px", fontStyle: "italic" }}>{article.contentType}</Typography>
                            </Box>
                        </Box>
                        <Box sx={{ marginTop: "10px" }}>
                            {article.tags && article.tags.map((tag, key) => (
                                <Chip
                                    label={tag}
                                    sx={{ marginRight: "5px" }}
                                />
                            ))}
                        </Box>
                        <Box sx={{
                            marginTop: "10px"
                        }}>
                            <Typography>
                                Published {article.publishedDate} By{' '}
                                <Typography component="span" fontWeight="bold">
                                    {article.user.userName}
                                </Typography>
                            </Typography>
                        </Box>
                        <Box sx={{ width: "800px" }}>
                            <Box sx={{
                                height: "450px",
                                width: "800px",
                                marginTop: "10px",
                                marginBottom: "20px",
                                backgroundImage: `url(${getImagePath(article.imgName)})`,
                                backgroundSize: "cover",
                                backgroundPosition: "center",
                                backgroundRepeat: "no-repeat"
                            }}></Box>
                            <Box>
                                <Stack direction="row">
                                    <Box><Typography>Rate this article:</Typography></Box>
                                    <Box><Rating
                                        name="simple-controlled"
                                        value={article.averageRating}
                                        onChange={(event, newValue) => {

                                        }}
                                        precision={0.5}
                                        size="medium"
                                        sx={{ marginY: 0, marginLeft: "5px", marginRight: "3px" }}
                                    /></Box>
                                    <Box><Typography fontSize="20px" sx={{ color: "#4287f5", }}>{article.averageRating}</Typography></Box>
                                    {article.user.id === userInfo.id &&
                                        <Box>
                                            <IconButton
                                                sx={{ padding: 0 }}
                                                aria-label="more"
                                                id="long-button"
                                                aria-controls={open ? 'long-menu' : undefined}
                                                aria-expanded={open ? 'true' : undefined}
                                                aria-haspopup="true"
                                                onClick={handleClick}
                                            >
                                                <MoreVertIcon />
                                            </IconButton>
                                            <Menu
                                                id="long-menu"
                                                MenuListProps={{
                                                    'aria-labelledby': 'long-button',
                                                }}
                                                anchorEl={anchorEl}
                                                open={open}
                                                onClose={handleClose}
                                                PaperProps={{
                                                    style: {
                                                        maxHeight: 48 * 4.5,
                                                        width: '20ch',
                                                    },
                                                }}
                                            >
                                                <MenuItem onClick={handleClose}>
                                                    Edit
                                                </MenuItem>
                                                <MenuItem onClick={handleClose}>
                                                    Delete
                                                </MenuItem>
                                            </Menu>
                                        </Box>
                                    }
                                </Stack>
                            </Box>
                            <Box>
                                <Typography sx={{ width: "100%", wordWrap: "break-word", fontSize: "18px", lineHeight: "29px", fontFamily: "Times New Roman" }} variant="body1" dangerouslySetInnerHTML={{ __html: article.content }} />
                            </Box>
                        </Box>
                        <Box>
                            <CommentsSection />
                        </Box>
                    </Stack >
                    {/* <ConfirmationModal open={open} onClose={() => {
                        setOpen(false)
                    }} onConfirm={confirmDelete} title={"You sure to delete?"} content={"The article will be deleted"} /> */}
                </>
            }
        </>
    )
}
