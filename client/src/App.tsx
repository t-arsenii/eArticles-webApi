import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Article } from './components/Article'
import axios from 'axios';
import { IArticle } from './models/articles';
import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Home } from './views/Home'
import { Navbar } from './components/Navbar'
import { RegForm } from './views/RegForm'
import { Container, CssBaseline } from '@mui/material';
import { Login } from './views/Login'
import { useDispatch, useSelector } from 'react-redux';
import { updateToken, updateUser } from './redux/userStore';
import { IUserInfo } from './models/user';
import { UserProfile } from './views/UserProfile';
import { RootState } from './redux/store';
import CreateArticle from './views/CreateArticle';
import FullArticle from './views/FullArticle';
import EditArticle from './views/EditArticle';
import AppRouter from './routes/AppRouter';
function App() {
  const defaultTheme = createTheme()
  const dispatch = useDispatch()
  const [isDarkTheme, setIsDarkTheme] = useState(false)
  useEffect(() => {
    const fetchUserInfo = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        return
      }
      dispatch(updateToken(token))
      const userInfoRes = await axios.get<IUserInfo>("http://localhost:5000/api/users", {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
      dispatch(updateUser(userInfoRes.data))
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
    getPrefferedTheme()
    checkTokenExpiration()
    fetchUserInfo()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])
  const darkTheme = createTheme({
    palette: {
      mode: 'dark',
    },
  });
  const theme = isDarkTheme ? darkTheme : defaultTheme
  return (
    <>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Navbar />
        <Container component="main">
          <AppRouter />
        </Container>
      </ThemeProvider>
    </>
  )
}

export default App;
