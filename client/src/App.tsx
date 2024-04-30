import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Article } from './components/Article'
import axios from 'axios';
import { IArticle } from './models/articles';
import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Home } from './pages/Home'
import { Navbar } from './components/Navbar'
import { RegForm } from './pages/RegForm'
import { Container, CssBaseline } from '@mui/material';
import { Login } from './pages/Login'
import { useAppDispatch, RootState, AppDispatch } from "./store/store";
import { getMe, updateToken, updateUser } from './store/userStore';
import { IUser } from './models/user';
import { UserProfile } from './pages/UserProfile';
import CreateArticle from './pages/CreateArticle';
import FullArticle from './pages/FullArticle';
import EditArticle from './pages/EditArticle';
import AppRouter from './routes/AppRouter';
import Footer from './components/Footer';
import { Toaster } from 'react-hot-toast';
import { useSelector } from 'react-redux';
import { updateColorTheme } from './store/localDataStore';
function App() {
  const dispatch: AppDispatch = useAppDispatch();
  const [isDarkTheme, setIsDarkTheme] = useState(false);
  const colorTheme = useSelector((state: RootState) => state.localData.colorTheme);
  const [theme, setTheme] = useState(() => {
    return localStorage.getItem("colorTheme") === 'dark' ? createTheme({
      palette: {
        mode: 'dark',
      },
    }) : createTheme();
  });
  useEffect(() => {
    const colorThemeLs = localStorage.getItem("colorTheme");
    if (colorThemeLs) {
      dispatch(updateColorTheme(colorThemeLs))
    }
  }, [])

  useEffect(() => {
    const fetchUser = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        return;
      }
      dispatch(updateToken(token));
      if (token) {
        dispatch(getMe(token));
      }
    }
    const checkTokenExpiration = () => {
      const token = localStorage.getItem("token");
      if (!token) {
        return;
      }
      const tokenPayload = JSON.parse(atob(token.split('.')[1]));
      const expirationTime = tokenPayload.exp;
      const currentTime = Math.floor(Date.now() / 1000);
      if (currentTime >= expirationTime) {
        localStorage.removeItem("token")
        window.location.reload();
      }
    }
    checkTokenExpiration()
    fetchUser()
  }, [dispatch])

  useEffect(() => {
    const newTheme = colorTheme === 'dark' ? createTheme({
      palette: {
        mode: 'dark',
      },
    }) : createTheme();
    setTheme(newTheme);
  }, [colorTheme]);

  return (
    <>
      <ThemeProvider theme={theme}>
        <Toaster />
        <CssBaseline />
        <Navbar />
        <Container component="main">
          <AppRouter />
        </Container>
        <Footer />
      </ThemeProvider>
    </>
  )
}

export default App;
