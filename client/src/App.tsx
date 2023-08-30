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
import { updateToken, updateUser } from './redux/userStore';
import { IUserInfo } from './models/user';
import { UserProfile } from './pages/UserProfile';
import { RootState } from './redux/store';
import CreateArticle from './pages/CreateArticle';
function App() {
  const defaultTheme = createTheme()
  const dispatch = useDispatch()
  const token = useSelector((state: RootState) => state.user.token)
  const userInfo = useSelector((state: RootState) => state.user.userInfo)
  const [article, setArticle] = useState<IArticle>()
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
      }
    }
    checkTokenExpiration()
    fetchUserInfo()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <>
      <ThemeProvider theme={defaultTheme}>
        <CssBaseline />
        <Navbar />
        <Container component="main">
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/signup' element={<RegForm />} />
            <Route path='/login' element={<Login />} />
            <Route path='/article/create' element={<CreateArticle />} />
            {token && <Route path={`/profile/${userInfo.userName}`} element={<UserProfile />} />}
          </Routes>
        </Container>
      </ThemeProvider>
    </>
  )
}

export default App;
