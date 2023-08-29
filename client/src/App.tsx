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
function App() {
  const defaultTheme = createTheme();
  const [article, setArticle] = useState<IArticle>()
  useEffect(() => {
    const fetchArticle = async () => {
      try {
        const response = await axios.get<IArticle>('http://localhost:5000/api/Articles/1')
        setArticle(response.data)
      } catch (error) {
        console.error("Error fetching article:", error);
      }
    }
    fetchArticle()
  }, [])

  return (
    <>
      <ThemeProvider theme={defaultTheme}>
        <CssBaseline />
        <Navbar />
        <Container component="main" maxWidth="xs">
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
