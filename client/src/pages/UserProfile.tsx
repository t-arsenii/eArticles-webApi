import { Avatar, Box, Card, CardContent, Stack, Typography } from '@mui/material'
import { useSelector } from 'react-redux'
import { RootState } from '../store/store'
import EmailIcon from '@mui/icons-material/Email';
import PhoneIcon from '@mui/icons-material/Phone';
import ArticlePage from '../components/ArticlePage'
export function UserProfile() {
    const { user: userInfo } = useSelector((state: RootState) => state.user)
    return (
        <Box sx={{ padding: 3, backgroundColor: '#f5f5f5', minHeight: '100vh' }}>
            <Card sx={{ maxWidth: 600, margin: 'auto', padding: 2 }}>
                <CardContent>
                    <Stack direction="row" spacing={2} alignItems="center">
                        <Avatar sx={{ width: 64, height: 64 }}>
                            {userInfo.userName.charAt(0)}
                        </Avatar>
                        <Box>
                            <Typography variant="h2" component="div">
                                {userInfo.userName}
                            </Typography>
                            <Typography variant="body1" color="text.secondary">
                                {userInfo.firstName} {userInfo.lastName}
                            </Typography>
                            <Stack direction="row" spacing={1} alignItems="center" mt={1}>
                                <EmailIcon color="action" />
                                <Typography variant="body1" color="text.secondary">
                                    {userInfo.email}
                                </Typography>
                            </Stack>
                            <Stack direction="row" spacing={1} alignItems="center" mt={1}>
                                <PhoneIcon color="action" />
                                <Typography variant="body1" color="text.secondary">
                                    {userInfo.phoneNumber}
                                </Typography>
                            </Stack>
                        </Box>
                    </Stack>
                </CardContent>
            </Card>
            <Box sx={{ marginTop: 4 }}>
                <Typography variant="h4" component="div" sx={{ marginBottom: 2 }}>
                    Published articles:
                </Typography>
                <ArticlePage url='http://localhost:5000/api/articles/my' order='date' isToken={true} />
            </Box>
        </Box>
    );
}