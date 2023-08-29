import { AppBar, IconButton, Toolbar, Typography, Stack, Button } from '@mui/material'
import { Link } from 'react-router-dom'
import AbcIcon from '@mui/icons-material/Abc';
import { styled } from '@mui/system'; // Import styled from @mui/system
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useSelector } from 'react-redux';
import { RootState } from '../redux/store';

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
                    <StyledLink to='/about'><Typography>About</Typography></StyledLink>
                    <StyledLink to='/faq'><Typography>FAQ</Typography></StyledLink>
                </Stack>
                {token ?
                    <Link to='/'>
                        <IconButton size='large' edge='end' color='inherit' aria-label='logo'>
                            {userInfo.userName}
                        </IconButton>
                    </Link>
                    :
                    <Link to='/signup'>
                        <IconButton size='large' edge='end' color='inherit' aria-label='logo'>
                            <AccountCircleIcon />
                        </IconButton>
                    </Link>

                }
            </Toolbar>
        </AppBar>
    )
}