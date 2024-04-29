import * as React from 'react';
import { createTheme, ThemeProvider, Container, Typography, Grid, Box, Avatar, Button, CssBaseline, TextField, FormControlLabel, Checkbox } from '@mui/material';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { useState } from 'react';
import { IUserAuthReq, IUserAuthRes, IUser, IUserRegReq, IUserRegRes } from "../models/user"
import { useForm } from "react-hook-form"
import PhoneInput from 'react-phone-number-input';
import axios from 'axios';
import { useDispatch } from 'react-redux';
import { updateToken, updateUser } from '../store/userStore'
import { Link, useNavigate } from 'react-router-dom'

export function RegForm() {
    const dispatch = useDispatch()
    const navigate = useNavigate()
    const form = useForm<IUserRegReq>({
        defaultValues: {
            firstName: '',
            lastName: '',
            userName: '',
            email: '',
            phoneNumber: '',
            password: ''
        }
    })
    const { register, handleSubmit, formState } = form;
    const { errors } = formState;
    const OnSubmit = async (data: IUserRegReq) => {
        try {
            const resReg = await axios.post<IUserRegRes>("http://localhost:5000/api/users", data);
            const resRegData = resReg.data
            const userInfo: IUser = { ...resRegData }
            dispatch(updateUser(userInfo))

            const authData: IUserAuthReq = {
                userName: resRegData.userName,
                password: resRegData.password
            }

            const resAuth = await axios.post<IUserAuthRes>("http://localhost:5000/api/users/login", authData);
            const resAuthData = resAuth.data
            localStorage.setItem('token', resAuthData.token);
            dispatch(updateToken(resAuthData.token))
            
            return navigate('/')
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
                Sign up
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit(OnSubmit)} sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            autoComplete="given-name"
                            required
                            fullWidth
                            id="firstName"
                            label="First Name"
                            autoFocus
                            {...register("firstName", {
                                required: "first name is required"
                            })}
                            error={!!errors.firstName}
                            helperText={errors.firstName?.message}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            id="lastName"
                            label="Last Name"
                            autoComplete="family-name"
                            {...register("lastName", {
                                required: "last name is required"
                            })}
                            error={!!errors.lastName}
                            helperText={errors.lastName?.message}
                        />
                    </Grid>
                    <Grid item xs={12}>
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
                            id="email"
                            label="Email Address"
                            autoComplete="email"
                            {...register("email", {
                                required: "Email Address is required"
                            })}
                            error={!!errors.email}
                            helperText={errors.email?.message}

                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            required
                            fullWidth
                            label="Phone number"
                            type="text"
                            id="phoneNumber"
                            autoComplete="phone-number"
                            {...register("phoneNumber", {
                                required: "phone number is required"
                            })}
                            error={!!errors.phoneNumber}
                            helperText={errors.phoneNumber?.message}
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
                    <Grid item xs={12}>
                        <FormControlLabel
                            control={<Checkbox value="allowExtraEmails" color="primary" />}
                            label="I want to receive inspiration, marketing promotions and updates via email."
                        />
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Sign Up
                </Button>
                <Grid container justifyContent="flex-end">
                    <Grid item>
                        <Link to="/login" >
                            Already have an account? Sign in
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}
