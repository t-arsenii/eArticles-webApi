import { Box, Stack, Typography } from '@mui/material'
import { useSelector } from 'react-redux'
import { RootState } from '../redux/store'
import { useEffect } from 'react'
import axios from 'axios'
import { IArticle } from '../models/articles'
import ArticlePage from '../components/ArticlePage'
export function UserProfile() {
    const { userInfo } = useSelector((state: RootState) => state.user)
    return (
        <>
            <Stack>
                <Typography variant='h2'>Hello {userInfo.userName}</Typography>
                <Box>{userInfo.firstName} {userInfo.lastName}</Box>
                <Box>{userInfo.email}</Box>
                <Box>{userInfo.phoneNumber}</Box>
            </Stack>
            <Typography variant='h4'>Your articles:</Typography>
            <ArticlePage url='http://localhost:5000/api/articles/my' isToken={true} />
        </>
    )
}