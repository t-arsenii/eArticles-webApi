import { Avatar, Box, Button, Checkbox, FormControlLabel, Grid, TextField, Typography, InputLabel, Select, MenuItem, SelectChangeEvent } from "@mui/material";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate, useParams } from "react-router-dom"
import { IArticle, IArticleReq, IArticleRes } from "../models/articles";
import { useSelector } from "react-redux";
import { RootState } from '../redux/store';
import axios from "axios";

export default function EditArticle() {
    const [articleType, setArticleType] = useState("Review")
    // const [initArticle, setInitArticle] = useState<IArticle>()
    const token = useSelector((state: RootState) => state.user.token)
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    const navigate = useNavigate()
    const {id} = useParams()
    useEffect(() => {
        const fetchArticle = async () => {
            const resArticle = await axios.get<IArticleRes>(`http://localhost:5000/api/Articles/${id}`)
            const resArticleData: IArticle = { ...resArticle.data }
            reset(resArticleData)
            setArticleType(resArticleData.articleType)
        }
        fetchArticle()
    }, [])
    const form = useForm<IArticleReq>({
        defaultValues: {
            title: '',
            description: '',
            content: '',
            articleType: '',
            imgUrl: '',
            articleTags: null
        }
    })
    const {reset, register, handleSubmit, formState, getValues } = form
    const { errors } = formState
    const OnSubmit = async (data: IArticleReq) => {
        try {
            data.articleType = articleType
            console.log(data)
            const resArticle = await axios.put<IArticleRes>("http://localhost:5000/api/articles", data, {
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
            <Typography component="h1" variant="h5">
                Create article
            </Typography>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Box component="form" noValidate onSubmit={handleSubmit(OnSubmit)} sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={4}>
                            <TextField
                                autoComplete="given-title"
                                required
                                fullWidth
                                id="title"
                                label="Title"
                                autoFocus
                                {...register("title", {
                                    required: "first name is required"
                                })}
                            // error={!!errors.firstName}
                            // helperText={errors.firstName?.message}
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <TextField
                                required
                                fullWidth
                                id="imgUrl"
                                label="Img Url"
                                autoComplete="ImgUrl"
                                {...register("imgUrl", {
                                    required: "Email Address is required"
                                })}
                            // error={!!errors.email}
                            // helperText={errors.email?.message}

                            />
                        </Grid>
                        <Grid item xs={12} sm={2}>
                            <Select
                                id="ArticlType"
                                value={articleType}
                                onChange={(event: SelectChangeEvent) => setArticleType(event.target.value)}
                            >
                                <MenuItem value="Review">Review</MenuItem>
                                <MenuItem value="Guide">Guide</MenuItem>
                                <MenuItem value="News">News</MenuItem>
                                <MenuItem value="Opinion">Opinion</MenuItem>
                            </Select>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                multiline
                                required
                                fullWidth
                                id="Description"
                                label="Description"
                                autoComplete="Description"
                                autoFocus
                                {...register("description", {
                                    required: "user name is required"
                                })}
                            // error={!!errors.userName}
                            // helperText={errors.userName?.message}

                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                multiline
                                required
                                fullWidth
                                id="Content"
                                label="Content"
                                autoComplete="content"
                                rows={20}
                                {...register("content", {
                                    required: "Email Address is required"
                                })}
                            // error={!!errors.email}
                            // helperText={errors.email?.message}

                            />
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Publish
                    </Button>
                </Box>
            </Box>
        </>
    )
}

