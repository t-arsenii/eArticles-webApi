import { Box, Stack, Typography } from '@mui/material'
import { useSelector } from 'react-redux'
import { RootState } from '../redux/store'
export function UserProfile() {
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    return (
        <Stack>
            <Typography variant='h2'>Hello {userInfo.userName}</Typography>
            <Box>{userInfo.firstName} {userInfo.lastName}</Box>
            <Box>{userInfo.email}</Box>
            <Box>{userInfo.phoneNumber}</Box>
        </Stack>
    )
}