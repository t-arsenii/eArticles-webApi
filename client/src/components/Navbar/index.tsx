import { AppBar, IconButton, Toolbar, Typography, Stack, Button, Switch, Box, Menu, MenuItem, Avatar, Divider, ListItemIcon, Tooltip } from '@mui/material'
import { Link, useNavigate } from 'react-router-dom'
import AbcIcon from '@mui/icons-material/Abc';
import { styled } from '@mui/system';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import CreateIcon from '@mui/icons-material/Create';
import { useSelector } from 'react-redux';
import { AppDispatch, RootState, useAppDispatch } from '../../store/store';
import ArticleIcon from '@mui/icons-material/Article';
import { useEffect, useState } from 'react';
import "./Navbar.css";
import { updateColorTheme } from '../../store/localDataStore';
import { Logout, PersonAdd, Settings } from '@mui/icons-material';
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
    const open = Boolean(anchorEl);
    const colorTheme = useSelector((state: RootState) => state.localData.colorTheme);
    const dispatch: AppDispatch = useAppDispatch();
    const navigate = useNavigate();
    const pages = [
        { name: "HOME", link: "/" },
        { name: "LATEST", link: "/latest" },
        { name: "NEWS", link: "/news" },
        { name: "GAMES", link: "/games" },
        { name: "GUIDES", link: "/guides" },
        { name: "ANIME", link: "/anime" },
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
    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
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
            <Toolbar variant="dense" sx={{ justifyContent: "space-between", marginY: "10px" }}>
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
                            {/* <Box sx={{ display: "flex", alignItems: "center" }}>
                                <Box sx={{ userSelect: "none", cursor: "pointer" }} onClick={handleMenu}><span>{userInfo.userName}</span></Box>
                                <Link to={`/profile/${userInfo.userName}`} className='username'>
                                    {userInfo.userName}
                                </Link>
                            </Box> */}

                            <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                                <Tooltip title="Account">
                                    <IconButton
                                        onClick={handleClick}
                                        size="small"
                                        sx={{ ml: 2 }}
                                        aria-controls={open ? 'account-menu' : undefined}
                                        aria-haspopup="true"
                                        aria-expanded={open ? 'true' : undefined}
                                    >
                                        <Avatar sx={{ width: 32, height: 32 }}>M</Avatar>
                                    </IconButton>
                                </Tooltip>
                            </Box>

                            <Menu
                                anchorEl={anchorEl}
                                id="account-menu"
                                open={open}
                                onClose={handleClose}
                                onClick={handleClose}
                                PaperProps={{
                                    elevation: 0,
                                    sx: {
                                        overflow: 'visible',
                                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                                        mt: 1.5,
                                        '& .MuiAvatar-root': {
                                            width: 32,
                                            height: 32,
                                            ml: -0.5,
                                            mr: 1,
                                        },
                                        '&::before': {
                                            content: '""',
                                            display: 'block',
                                            position: 'absolute',
                                            top: 0,
                                            right: 14,
                                            width: 10,
                                            height: 10,
                                            bgcolor: 'background.paper',
                                            transform: 'translateY(-50%) rotate(45deg)',
                                            zIndex: 0,
                                        },
                                    },
                                }}
                                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
                            >
                                <MenuItem onClick={handleClose}>
                                    <Avatar sx={{ width: "30px", marginRight: "8px" }} />
                                    <Link style={{ color: "inherit", textDecoration: "none" }} to={`/profile/${userInfo.userName}`}>Profile</Link>
                                </MenuItem>
                                <MenuItem onClick={handleClose}>
                                    <CreateIcon sx={{ width: "30px", marginRight: "8px" }} />
                                    <Link style={{ color: "inherit", textDecoration: "none" }} to='/article/create'>Create article</Link>
                                </MenuItem>
                                <Divider />
                                <MenuItem onClick={handleClose}>
                                    <ListItemIcon>
                                        <Settings fontSize="small" />
                                    </ListItemIcon>
                                    Settings
                                </MenuItem>
                                <MenuItem onClick={handleLogOut}>
                                    <ListItemIcon>
                                        <Logout fontSize="small" />
                                    </ListItemIcon>
                                    Logout
                                </MenuItem>
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