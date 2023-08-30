import * as React from 'react';
import { createTheme, ThemeProvider, Container, Typography, Grid, Box, Avatar, Button, CssBaseline, TextField, FormControlLabel, Checkbox } from '@mui/material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { useState } from 'react';
import { IUserAuthReq, IUserAuthRes, IUserInfo } from "../models/user"
import { useForm } from "react-hook-form"
import PhoneInput from 'react-phone-number-input';
import axios from 'axios';
import { useDispatch } from 'react-redux';
import { updateToken, updateUser } from '../redux/userStore'
import { Link, useNavigate } from 'react-router-dom'

export function Login() {
    const dispatch = useDispatch()
    const navigate = useNavigate()
    const form = useForm<IUserAuthReq>({
        defaultValues: {
            userName: '',
            password: ''
        }
    })
    const { register, handleSubmit, formState } = form
    const { errors } = formState

    const OnSubmit = async (data: IUserAuthReq) => {
        try {
            const authRes = await axios.post<IUserAuthRes>("http://localhost:5000/api/users/login", data);
            const resAuthData = authRes.data
            
            localStorage.setItem('token', resAuthData.token);
            dispatch(updateToken(resAuthData.token))
            
            const userInfoRes = await axios.get<IUserInfo>("http://localhost:5000/api/users", {
                headers: {
                    Authorization: `Bearer ${resAuthData.token}`,
                    'Content-Type': 'application/json'
                }
            });
            dispatch(updateUser(userInfoRes.data))
            
            return navigate("/")
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
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}
        >
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Sign in
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit(OnSubmit)} sx={{ mt: 3 }}>
                <Grid container spacing={2}>                        <Grid item xs={12}>
                    <TextField
                        required
                        fullWidth
                        id="userName"
                        label="User Name"
                        autoComplete="userName"
                        {...register("userName", {
                            required: "user name is required"
                        })}
                        error={!!errors.userName}
                        helperText={errors.userName?.message}

                    />
                </Grid>
                    <Grid item xs={12}>
                        <TextField
                            required
                            fullWidth
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="new-password"
                            {...register("password", {
                                required: "password is required"
                            })}
                            error={!!errors.password}
                            helperText={errors.password?.message}
                        />
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Sign In
                </Button>
                <Grid container justifyContent="flex-end">
                    <Grid item>
                        <Link to="/signup" >
                            Don't have an account? Sign up
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}
