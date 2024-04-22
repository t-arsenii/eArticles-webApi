import { Avatar, Box, Button, Checkbox, FormControlLabel, Grid, TextField, Typography, InputLabel, Select, MenuItem, SelectChangeEvent, Paper, Chip, styled, Stack, FormControl, Input } from "@mui/material";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom"
import { IArticleCreateReq, IArticleCreateRes } from "../models/articles";
import { useSelector } from "react-redux";
import { RootState } from '../store/store';
import SendIcon from '@mui/icons-material/Send';
import axios from "axios";
import { ITag } from "../models/tag";
import ReactQuill from "react-quill";
import 'react-quill/dist/quill.snow.css';
import { Image } from "@mui/icons-material";
import { ICategory } from "../models/category";
import { IContentType } from "../models/contentType";

interface ChipData {
    key: number;
    label: string;
}
const ListItem = styled('li')(({ theme }) => ({
    margin: theme.spacing(0.5),
}));
const textEditorModules = {
    toolbar: [
        [{ header: [1, 2, 3, false] }],
        // [{font: []}],
        [{ size: [] }],
        ["bold", "italic", "underline", "strike", "blockquote"],
        [
            { list: "ordered" },
            { list: "bullet" },
            { indent: "-1" },
            { indent: "+1" },
        ],
        ["link", "image", "video"]
    ]
}
export default function CreateArticle() {
    const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (!e.target.files) {
            return;
        }
        setImagePreview(URL.createObjectURL(e.target.files[0]));
    }
    const fetchTags = async () => {
        const res = await axios.get<ITag[]>("http://localhost:5000/api/tags");
        setAllTags(res.data);
    }
    const fetchCategories = async () => {
        const res = await axios.get<ICategory[]>("http://localhost:5000/api/tags");
        setAllCategories(res.data);
    }
    const fetchContentTypes = async () => {
        const res = await axios.get<IContentType[]>("http://localhost:5000/api/tags");
        setAllContentTypes(res.data);
    }
    useEffect(() => {
        try {
            fetchTags();
            fetchCategories();
            fetchContentTypes();
        } catch (err) {
            console.log(err);
        }
    }, []);
    const [allTags, setAllTags] = useState<ITag[]>();
    const [allCategories, setAllCategories] = useState<ICategory[]>();
    const [allContentTypes, setAllContentTypes] = useState<IContentType[]>();

    const [imagePreview, setImagePreview] = useState<string>('');
    const [contentValue, setContentValue] = useState<string>('');
    const [selectedTag, setSelectedTag] = useState<string>('');
    const [tagsData, setTagsData] = useState<string[]>([]);
    const [articleType, setArticleType] = useState("Review");
    const token = useSelector((state: RootState) => state.user.token);
    const userInfo = useSelector((state: RootState) => state.user.userInfo);
    const navigate = useNavigate();
    const handleAddTag = () => {
        if (selectedTag !== '' && !tagsData.includes(selectedTag)) {
            setTagsData([...tagsData, selectedTag]);
        }
        setSelectedTag('');
    }
    const form = useForm<IArticleCreateReq>({
        defaultValues: {
            title: '',
            description: '',
            content: '',
            contentTypeId: '',
            imgUrl: '',
            tagIds: null
        }
    })
    const { register, handleSubmit, formState, getValues } = form;
    const { errors } = formState;
    const OnSubmit = async (data: IArticleCreateReq) => {
        data.contentTypeId = articleType;
        data.tagIds = tagsData;
        data.content = contentValue;
        console.log(data);
        try {
            const resArticle = await axios.post<IArticleCreateRes>("http://localhost:5000/api/articles", data, {
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
                        <Grid item xs={12} sm={12}>
                            <TextField
                                autoComplete="given-title"
                                required
                                fullWidth
                                id="title"
                                label="Title"
                                autoFocus
                                {...register("title", {
                                    required: "Title is required"
                                })}
                            // error={!!errors.firstName}
                            // helperText={errors.firstName?.message}
                            />
                        </Grid>
                        <Grid item xs={12} sm={12}>
                            <img src={imagePreview} alt="" />
                            <br/>
                            <Button
                                variant="contained"
                                component="label"
                            >
                                Upload File
                                <input type="file" hidden onChange={handleImageChange} accept="image/png, image/jpeg, image/jpg"/>
                            </Button>
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
                                    required: "Description is required"
                                })}
                            // error={!!errors.userName}
                            // helperText={errors.userName?.message}

                            />
                        </Grid>
                        <Grid item xs={12}>
                            <ReactQuill style={{ margin: "5px 0 50px 0", height: "300px" }} theme="snow" value={contentValue} onChange={setContentValue} modules={textEditorModules} />
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

