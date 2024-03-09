import { Route, Routes } from "react-router-dom";
import { Home } from "../pages/Home";
import { RegForm } from "../pages/RegForm";
import CreateArticle from "../pages/CreateArticle";
import FullArticle from "../pages/FullArticle";
import EditArticle from "../pages/EditArticle";
import { UserProfile } from "../pages/UserProfile";
import { Login } from "../pages/Login";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import Latest from "../pages/Latest";

export default function AppRouter() {
    const token = useSelector((state: RootState) => state.user.token)
    const userInfo = useSelector((state: RootState) => state.user.userInfo)
    return (
        <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/signup' element={<RegForm />} />
            <Route path='/login' element={<Login />} />
            <Route path='/article/create' element={<CreateArticle />} />
            <Route path='/article/:id' element={<FullArticle />} />
            <Route path='/article/edit/:id' element={<EditArticle />} />
            <Route path='/latest' element={<Latest/>}/>
            {token && <Route path={`/profile/${userInfo.userName}`} element={<UserProfile />} />}
        </Routes>
    )
}
