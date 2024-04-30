import { AppBar, IconButton, Toolbar, Typography, Stack, Button, Switch, Box, Menu, MenuItem } from '@mui/material'
import { Link, useNavigate } from 'react-router-dom'
import AbcIcon from '@mui/icons-material/Abc';
import { styled } from '@mui/system';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useSelector } from 'react-redux';
import { AppDispatch, RootState, useAppDispatch } from '../../store/store';
import ArticleIcon from '@mui/icons-material/Article';
import { useEffect, useState } from 'react';
import "./Navbar.css";
import { updateColorTheme } from '../../store/localDataStore';
export function Navbar() {
    const handleLogOut = () => {
        localStorage.removeItem('token');
        navigate('/')
        window.location.reload();
    }
    const token = useSelector((state: RootState) => state.user.token);
    const userInfo = useSelector((state: RootState) => state.user.user)
    const [isDarkTheme, setIsDarkTheme] = useState(false);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const colorTheme = useSelector((state: RootState) => state.localData.colorTheme);
    const dispatch: AppDispatch = useAppDispatch();
    const navigate = useNavigate();
    const pages = [
        { name: "HOME", link: "/" },
        { name: "LATEST", link: "/latest" },
        { name: "NEWS", link: "/news" },
        { name: "GAMES", link: "/games" },
        { name: "ANIME", link: "/anime" },
        { name: "OTHER", link: "/other" }
    ];
    useEffect(() => {
        if (colorTheme) {
            if (colorTheme === "dark") {
                setIsDarkTheme(true);
            }
        }
    }, [colorTheme])
    const handleThemeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const isChecked = event.target.checked;
        setIsDarkTheme((prev) => !prev);
        const newTheme = isChecked ? "dark" : "bright";
        localStorage.setItem('colorTheme', newTheme);
        dispatch(updateColorTheme(newTheme))
    }
    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };
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
                        <>
                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                <Box sx={{ userSelect: "none", cursor: "pointer" }} onClick={handleMenu}><span>{userInfo.userName}</span></Box>
                                {/* <Link to={`/profile/${userInfo.userName}`} className='username'>
                                    {userInfo.userName}
                                </Link> */}
                            </Box>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                // transformOrigin={{
                                //     vertical: 'top',
                                //     horizontal: 'right',
                                // }}
                                open={Boolean(anchorEl)}
                                onClose={handleClose}
                            >
                                <MenuItem onClick={handleClose}><Link style={{ color: "inherit", textDecoration: "none" }} to={`/profile/${userInfo.userName}`}>Profile</Link></MenuItem>
                                <MenuItem onClick={handleClose}><Link style={{ color: "inherit", textDecoration: "none" }} to='/article/create'>Make article</Link></MenuItem>
                                <MenuItem onClick={handleLogOut}>Log out</MenuItem>
                            </Menu>
                        </>
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