import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useEffect, useState } from 'react';
import { Navbar } from './components/Navbar'
import { Container, CssBaseline, ThemeOptions } from '@mui/material';
import { useAppDispatch, RootState, AppDispatch } from "./store/store";
import { getMe, updateToken, updateUser } from './store/userStore';
import AppRouter from './routes/AppRouter';
import Footer from './components/Footer';
import { Toaster } from 'react-hot-toast';
import { useSelector } from 'react-redux';
import { updateColorTheme } from './store/localDataStore';
import { darkTheme, lightTheme } from './assets/colorThemes';

function App() {
  const dispatch: AppDispatch = useAppDispatch();
  const [isDarkTheme, setIsDarkTheme] = useState(false);
  const colorTheme = useSelector((state: RootState) => state.localData.colorTheme);
  const [theme, setTheme] = useState(() => {
    return localStorage.getItem("colorTheme") === 'dark' ? darkTheme : lightTheme;
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
    const newTheme = colorTheme === 'dark' ? darkTheme : lightTheme;
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
