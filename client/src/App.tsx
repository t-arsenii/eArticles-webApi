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
import { useDispatch, useSelector } from 'react-redux';
import { updateToken, updateUser } from './store/userStore';
import { IUser } from './models/user';
import { UserProfile } from './pages/UserProfile';
import { RootState } from './store/store';
import CreateArticle from './pages/CreateArticle';
import FullArticle from './pages/FullArticle';
import EditArticle from './pages/EditArticle';
import AppRouter from './routes/AppRouter';
import Footer from './components/Footer';
function App() {
  const dispatch = useDispatch()
  const [isDarkTheme, setIsDarkTheme] = useState(false)
  useEffect(() => {
    const fetchUserInfo = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        return
      }
      dispatch(updateToken(token))
      try {
        const userInfoRes = await axios.get<IUser>("http://localhost:5000/api/users", {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        });
        dispatch(updateUser(userInfoRes.data))
      } catch (err: any) {
        console.log(err.message);
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
    const getPrefferedTheme = () => {
      const prefferedTheme = localStorage.getItem("preferredTheme")
      if (prefferedTheme) {
        setIsDarkTheme(prefferedTheme === 'dark' ? true : false)
      }
    }
    // getPrefferedTheme()
    checkTokenExpiration()
    fetchUserInfo()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])
  const defaultTheme = createTheme()
  const darkTheme = createTheme({
    palette: {
      mode: 'dark',
    },
  });
  const theme = isDarkTheme ? defaultTheme : darkTheme
  return (
    <>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Navbar />
        <Container component="main">
          <AppRouter />
        </Container>
        <Footer/>
      </ThemeProvider>
    </>
  )
}

export default App;
