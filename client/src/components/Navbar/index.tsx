import { AppBar, IconButton, Toolbar, Typography, Stack, Button, Switch, Box } from '@mui/material'
import { Link } from 'react-router-dom'
import AbcIcon from '@mui/icons-material/Abc';
import { styled } from '@mui/system';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import ArticleIcon from '@mui/icons-material/Article';
import { useEffect, useState } from 'react';
import "./Navbar.css";
export function Navbar() {
    const token = useSelector((state: RootState) => state.user.token);
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    const [isDarkTheme, setIsDarkTheme] = useState(false);
    const pages = [
        { name: "HOME", link: "/" },
        { name: "LATEST", link: "/latest" },
        { name: "NEWS", link: "/news" },
        { name: "SCIENCE", link: "/science" },
        { name: "GAMES", link: "/games" },
        { name: "ANIME", link: "/anime" },
        { name: "OTHER", link: "/other" }
    ];
    useEffect(() => {
        const isDarkThemeLC: string | null = localStorage.getItem("preferredTheme");
        if (isDarkThemeLC) {
            localStorage.setItem("preferredTheme", isDarkThemeLC);
            if (isDarkThemeLC === "dark") {
                setIsDarkTheme(true);
            }
        }
    }, [])
    const handleThemeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newTheme = event.target.checked;
        setIsDarkTheme((prev) => !prev);
        localStorage.setItem('preferredTheme', newTheme ? 'dark' : 'light');
    }
    const StyledLink = styled(Link)({
        textDecoration: 'none',
        color: 'inherit',
        '&:hover': {
            textDecoration: 'underline',
        },
    });
    return (
        <AppBar position="static">
            <Toolbar variant="dense" sx={{ justifyContent: "space-between" }}>
                <Typography variant="h6" component="div">
                    <Link to="/" className='titleText'>
                        eArticles
                    </Link>
                </Typography>
                <Stack direction='row' spacing={2} marginRight={5}>
                    {pages.map(page => (
                        <StyledLink to={page.link}><Button color='inherit'>{page.name}</Button></StyledLink>
                    )
                    )}
                    {token &&
                        <StyledLink to='/article/create'><Button color='inherit'>Make article</Button></StyledLink>
                    }
                </Stack>
                <Stack direction="row">
                    <Stack direction="row" sx={{ alignItems: 'center' }}>
                        <Typography>Switch theme</Typography>
                        <Switch
                            checked={isDarkTheme}
                            onChange={handleThemeChange}
                            inputProps={{ 'aria-label': 'controlled' }}
                        />
                    </Stack>
                    {token ?
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            <Link to={`/profile/${userInfo.userName}`} className='username'>
                                {userInfo.userName}
                            </Link>
                        </Box>
                        :
                        <Box>
                            <Link to='/login' className='loginIcon'>
                                <IconButton size='large' edge='end' color='inherit' aria-label='logo'>
                                    <AccountCircleIcon />
                                </IconButton>
                            </Link>
                        </Box>
                    }
                </Stack>
            </Toolbar>
        </AppBar>
    )
}