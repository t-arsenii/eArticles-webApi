import { Avatar, Box, Button, Checkbox, FormControlLabel, Grid, TextField, Typography, InputLabel, Select, MenuItem, SelectChangeEvent } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom"

export default function CreateArticle() {
    const [articleType, setArticleType] = useState("News")
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
                <Box component="form" noValidate sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={4}>
                            <TextField
                                autoComplete="given-title"
                                required
                                fullWidth
                                id="Title"
                                label="Title"
                                autoFocus
                            // {...register("firstName", {
                            //     required: "first name is required"
                            // })}
                            // error={!!errors.firstName}
                            // helperText={errors.firstName?.message}
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <TextField
                                required
                                fullWidth
                                id="ImgUrl"
                                label="Img Url"
                                autoComplete="ImgUrl"
                            // {...register("email", {
                            //     required: "Email Address is required"
                            // })}
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
                            // {...register("userName", {
                            //     required: "user name is required"
                            // })}
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
                            // {...register("email", {
                            //     required: "Email Address is required"
                            // })}
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
