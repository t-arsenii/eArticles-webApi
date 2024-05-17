import { Avatar, Box, Button, Checkbox, FormControlLabel, Grid, TextField, Typography, InputLabel, Select, MenuItem, SelectChangeEvent, Paper, Chip, styled, Stack, FormControl, Input, Theme, OutlinedInput, useTheme } from "@mui/material";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom"
import { IArticle, IArticleCreateForm, IArticleCreateReq } from "../models/articles";
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
import DeleteIcon from '@mui/icons-material/Delete';
import toast from "react-hot-toast";

const DEFAULT_IMAGE_PREVIEW_URL = "https://placehold.co/1920x1080/png?text=16x9"
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

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};
function getStyles(name: string, personName: readonly string[], theme: Theme) {
    return {
        fontWeight:
            personName.indexOf(name) === -1
                ? theme.typography.fontWeightRegular
                : theme.typography.fontWeightMedium,
    };
}
export default function CreateArticle() {
    const handleDeleteImage = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        setImageSrc(DEFAULT_IMAGE_PREVIEW_URL);
    }
    const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (!e.target.files) {
            return;
        }
        setImageSrc(URL.createObjectURL(e.target.files[0]));
    }
    const handleChange = (event: SelectChangeEvent<typeof selectedTagIds>) => {
        const {
            target: { value },
        } = event;
        setSelectedTagIds(
            typeof value === 'string' ? value.split(',') : value,
        );
    };
    const fetchTags = async () => {
        try {
            const { data } = await axios.get<ITag[]>("http://localhost:5000/api/tags");
            setAllTags(data);
        } catch (err: any) {
            console.error(err.message);
        }
    }
    const fetchCategories = async () => {
        try {
            const { data } = await axios.get<ICategory[]>("http://localhost:5000/api/categories");
            setAllCategories(data);
            setCategoryId(data[0].id);
        } catch (err: any) {
            console.error(err.message);
        }
    }
    const fetchContentTypes = async () => {
        try {
            const { data } = await axios.get<IContentType[]>("http://localhost:5000/api/contentTypes");
            setAllContentTypes(data);
            setContentTypeId(data[0].id);
        } catch (err: any) {
            console.error(err.message)
        }
    }
    useEffect(() => {
        fetchTags();
        fetchCategories();
        fetchContentTypes();
    }, []);
    const theme = useTheme();
    const [allTags, setAllTags] = useState<ITag[]>();
    const [allCategories, setAllCategories] = useState<ICategory[]>();
    const [allContentTypes, setAllContentTypes] = useState<IContentType[]>();

    const [selectedTagIds, setSelectedTagIds] = useState<string[]>([]);

    const [contentValue, setContentValue] = useState<string>('');
    const [contentTypeId, setContentTypeId] = useState<string>('');
    const [categoryId, setCategoryId] = useState<string>('');

    const [imageSrc, setImageSrc] = useState<string>(DEFAULT_IMAGE_PREVIEW_URL);
    const [tagsData, setTagsData] = useState<string[]>([]);
    const token = useSelector((state: RootState) => state.user.token);
    const userInfo = useSelector((state: RootState) => state.user.user);
    const navigate = useNavigate();
    const form = useForm<IArticleCreateForm>({
        defaultValues: {
            title: '',
            description: '',
            content: '',
            contentTypeId: '',
            tagIds: null
        }
    })
    const { register, handleSubmit, formState, getValues } = form;
    const { errors } = formState;
    const OnSubmit = async (data: IArticleCreateForm) => {
        var formData = new FormData();
        var dataRequest: IArticleCreateReq = {
            title: "",
            description: "",
            content: "",
            contentTypeId: "",
            categoryId: "",
            tagIds: null
        };
        if (data.title) {
            dataRequest.title = data.title;
        }
        if (data.description) {
            dataRequest.description = data.description;
        }
        if (selectedTagIds) {
            dataRequest.tagIds = selectedTagIds;
        }
        if (data.contentTypeId) {
            dataRequest.contentTypeId = data.contentTypeId;
        } else {
            dataRequest.contentTypeId = contentTypeId;
        }
        if (data.categoryId) {
            dataRequest.categoryId = data.categoryId;
        } else {
            dataRequest.categoryId = categoryId;
        }
        if (contentValue) {
            dataRequest.content = contentValue;
        }
        if (imageSrc !== DEFAULT_IMAGE_PREVIEW_URL) {
            formData.append("image", data.image[0])
        }
        formData.append("json", JSON.stringify(dataRequest));
        try {
            const resArticle = await axios.post<IArticle>("http://localhost:5000/api/articles", formData, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'multipart/form-data'
                }
            });
            toast.success("Articles published successfully!")
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
            toast.error("Something went wrong!");
        }
    }
    return (
        <>
            <Box>
                <Box mt="15px">
                    <Typography component="h1" variant="h4">
                        Create article
                    </Typography>
                </Box>
                <Box component="form" noValidate onSubmit={handleSubmit(OnSubmit)} sx={{ mt: 3, width: "100%" }}>
                    <Stack spacing={2}>
                        <Box>
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
                            />
                        </Box>
                        <Box>
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

                            />
                        </Box>
                        <Box>
                            <Stack direction="row" spacing="10px">
                                <Box>
                                    <FormControl sx={{ width: 300 }}>
                                        <InputLabel id="demo-multiple-chip-label">Tags</InputLabel>
                                        <Select
                                            labelId="demo-multiple-chip-label"
                                            id="demo-multiple-chip"
                                            multiple
                                            value={selectedTagIds}
                                            onChange={handleChange}
                                            input={<OutlinedInput id="select-multiple-chip" label="Chip" />}
                                            renderValue={(selected) => (
                                                <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                                                    {selected.map((value) => (
                                                        <Chip key={value} label={allTags?.find(t => t.id === value)?.title} />
                                                    ))}
                                                </Box>
                                            )}
                                            MenuProps={MenuProps}>
                                            {allTags && allTags.map((tag) => (
                                                <MenuItem
                                                    key={tag.id}
                                                    value={tag.id}
                                                    style={getStyles(tag.title, selectedTagIds, theme)}
                                                >
                                                    {tag.title}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Box>
                                <Box>
                                    <Select
                                        {...register("contentTypeId")}
                                        id="ContentType"
                                        value={contentTypeId}
                                        onChange={(event: SelectChangeEvent) => setContentTypeId(event.target.value)}
                                    >
                                        {allContentTypes && allContentTypes.map(conentType => (
                                            <MenuItem id={conentType.id} value={conentType.id}>{conentType.title}</MenuItem>
                                        ))}
                                    </Select>
                                </Box>
                                <Box>
                                    <Select
                                        {...register("categoryId")}
                                        id="Category"
                                        value={categoryId}
                                        onChange={(event: SelectChangeEvent) => setCategoryId(event.target.value)}>
                                        {allCategories && allCategories.map(category => (
                                            <MenuItem id={category.id} value={category.id}>{category.title}</MenuItem>
                                        ))}
                                    </Select>
                                </Box>
                            </Stack>
                        </Box>
                        <Box>
                            <img style={{ height: "316px", width: "562px", display: "block", objectFit: "cover" }} src={imageSrc} alt="" />
                            <Stack sx={{ mt: "10px" }} direction="row" alignItems="center">
                                <Input {...register("image")} type="file" hidden onChange={handleImageChange} inputProps={{ accept: "image/png, image/jpeg, image/jpg" }} />
                                <Box sx={{ cursor: "pointer" }} onClick={handleDeleteImage}>
                                    {(imageSrc !== DEFAULT_IMAGE_PREVIEW_URL) && <DeleteIcon fontSize="large" />}
                                </Box>
                            </Stack>
                        </Box>
                        <Box>
                            <ReactQuill style={{ margin: "5px 0 50px 0", height: "300px" }} theme="snow" value={contentValue} onChange={setContentValue} modules={textEditorModules} />
                        </Box>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Publish
                        </Button>
                    </Stack>
                </Box >


            </Box >
        </>
    )
}

