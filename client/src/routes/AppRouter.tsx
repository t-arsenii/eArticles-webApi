import { Route, Routes } from "react-router-dom";
import { Home } from "../views/Home";
import { RegForm } from "../views/RegForm";
import CreateArticle from "../views/CreateArticle";
import FullArticle from "../views/FullArticle";
import EditArticle from "../views/EditArticle";
import { UserProfile } from "../views/UserProfile";
import { Login } from "../views/Login";
import { useSelector } from "react-redux";
import { RootState } from "../redux/store";

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
            {token && <Route path={`/profile/${userInfo.userName}`} element={<UserProfile />} />}
        </Routes>
    )
}
