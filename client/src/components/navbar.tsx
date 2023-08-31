import { AppBar, IconButton, Toolbar, Typography, Stack, Button } from '@mui/material'
import { Link } from 'react-router-dom'
import AbcIcon from '@mui/icons-material/Abc';
import { styled } from '@mui/system'; // Import styled from @mui/system
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useSelector } from 'react-redux';
import { RootState } from '../redux/store';
import ArticleIcon from '@mui/icons-material/Article';
export function Navbar() {
    const token = useSelector((state: RootState) => state.user.token);
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    const StyledLink = styled(Link)({
        textDecoration: 'none',
        color: 'inherit',
        '&:hover': {
            textDecoration: 'underline',
        },
    });
    return (
        <AppBar position="static">
            <Toolbar variant="dense">
                <IconButton size='large' edge='start' color='inherit' aria-label='logo'>
                    <AbcIcon />
                </IconButton>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    Gaming Blog
                </Typography>
                <Stack direction='row' spacing={2} marginRight={5}>
                    <StyledLink to='/'><Typography>Home</Typography></StyledLink>
                    {token &&
                        <StyledLink to='/article/create'><Typography>Make article</Typography></StyledLink>
                    }
                </Stack>
                {token ?
                    <Link to={`/profile/${userInfo.userName}`} style={{ color: "white", textDecoration: "none", fontWeight: "bold" }}>
                        {userInfo.userName}
                    </Link>
                    :
                    <Link to='/login'>
                        <IconButton size='large' edge='end' color='inherit' aria-label='logo'>
                            <AccountCircleIcon />
                        </IconButton>
                    </Link>
                }
            </Toolbar>
        </AppBar>
    )
}