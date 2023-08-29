import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Article } from './components/article'
import axios from 'axios';
import { IArticle } from './models/articles';
import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Home } from './components/home'
import { Navbar } from './components/navbar'
import { RegForm } from './components/regForm'
import { Container, CssBaseline } from '@mui/material';
import { Login } from './components/login'
import { useDispatch } from 'react-redux';
import { updateToken, updateUser } from './redux/userStore';
import { IUserInfo } from './models/user';
function App() {
  const defaultTheme = createTheme()
  const dispatch = useDispatch()
  const [article, setArticle] = useState<IArticle>()
  useEffect(() => {
    const fetchUserInfo = async () => {
      const token = localStorage.getItem('token');
      if (token) {
        dispatch(updateToken(token))
        const userInfoRes = await axios.get<IUserInfo>("http://localhost:5000/api/users", {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        });
        dispatch(updateUser(userInfoRes.data))
      }

    }
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
          </Routes>
        </Container>
      </ThemeProvider>
    </>
  )
}

export default App;
