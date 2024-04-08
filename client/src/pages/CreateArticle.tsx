import { Avatar, Box, Button, Checkbox, FormControlLabel, Grid, TextField, Typography, InputLabel, Select, MenuItem, SelectChangeEvent, Paper, Chip, styled, Stack, FormControl } from "@mui/material";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom"
import { IArticleReq, IArticleRes } from "../models/articles";
import { useSelector } from "react-redux";
import { RootState } from '../store/store';
import SendIcon from '@mui/icons-material/Send';
import axios from "axios";
import { ITag } from "../models/tag";
interface ChipData {
    key: number;
    label: string;
}
const ListItem = styled('li')(({ theme }) => ({
    margin: theme.spacing(0.5),
}));

export default function CreateArticle() {
    const fetchTags = async () => {
        const res = await axios.get<ITag[]>("http://localhost:5000/api/tags");
        setAllTags(res.data);
    }
    useEffect(() => {
        try {
            fetchTags();
        } catch (err) {
            console.log(err);
        }
    }, []);
    const [selectedTag, setSelectedTag] = useState<string>('');
    const [tagsData, setTagsData] = useState<string[]>([]);
    const [allTags, setAllTags] = useState<ITag[]>();
    const [articleType, setArticleType] = useState("Review")
    const token = useSelector((state: RootState) => state.user.token)
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    const navigate = useNavigate();
    const handleAddTag = () => {
        if (selectedTag !== '' && !tagsData.includes(selectedTag)) {
            setTagsData([...tagsData, selectedTag]);
        }
        setSelectedTag('');
    }
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
    const { register, handleSubmit, formState, getValues } = form;
    const { errors } = formState;
    const OnSubmit = async (data: IArticleReq) => {
        try {
            data.articleType = articleType;
            data.articleTags = tagsData;
            console.log(data);
            const resArticle = await axios.post<IArticleRes>("http://localhost:5000/api/articles", data, {
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
    const handleDelete = (chipToDelete: string) => () => {
        setTagsData((chips) => chips.filter((chip) => chip !== chipToDelete));
    };
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
                        <Grid item xs={4}>
                            <Stack direction={"row"}>
                                <FormControl sx={{ m: 1, minWidth: 80 }}>
                                    <InputLabel id="demo-simple-select-autowidth-label">Tag</InputLabel>
                                    <Select
                                        labelId="demo-simple-select-autowidth-label"
                                        id="demo-simple-select-autowidth"
                                        value={selectedTag}
                                        onChange={(event) => {
                                            setSelectedTag(event.target.value);
                                        }}
                                        autoWidth
                                        label="Age"
                                    >
                                        {allTags && allTags.map(tag => (
                                            <MenuItem key={tag.id} value={tag.title}>
                                                {tag.title}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>

                                <Button onClick={handleAddTag} variant="contained" endIcon={<SendIcon />} />
                            </Stack>
                        </Grid>
                        <Grid item xs={4}>
                            <Paper
                                sx={{
                                    display: 'flex',
                                    justifyContent: 'center',
                                    flexWrap: 'wrap',
                                    listStyle: 'none',
                                    p: 0.5,
                                    m: 0,
                                }}
                                component="ul"
                            >
                                {tagsData.map((data, key) => {
                                    return (
                                        <ListItem key={key}>
                                            <Chip
                                                // icon={icon}
                                                label={data}
                                                onDelete={handleDelete(data)}
                                            />
                                        </ListItem>
                                    );
                                })}
                            </Paper>
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

