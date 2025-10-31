import {Routes, Route, Navigate} from 'react-router-dom'
import Navbar from './components/Navbar.jsx'
import Menu from './pages/Menu.jsx'
import CustomBuilder from './pages/CustomBuilder.jsx'
import CartCheckout from './pages/CartCheckout.jsx'
import MyOrders from './pages/MyOrders.jsx'
import AdminDashboard from './pages/admin/AdminDashboard.jsx'
import AdminIngredienti from './pages/admin/AdminIngredienti.jsx'
import AdminOrdini from './pages/admin/AdminOrdini.jsx'
import {useAuth} from './context/AuthContext.jsx'

import Home from './home.jsx'
import Login from './pages/Login.jsx'
import Footer from "./components/Footer.jsx";
import {MenuPage} from "./pages/MenuPage.jsx";
function PrivateRoute({children, role}) {
    const {user} = useAuth();
    if (!user) return <Navigate to="/login" replace/>
    if (role && user.ruolo !== role) return <Navigate to="/" replace/>
    return children
}

export default function App() {
    return (
        <div>
            <Navbar/>
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/login" element={<Login/>}/>
                <Route path="/menu" element={<MenuPage/>}/>
                <Route path="/custom" element={<CustomBuilder/>}/>
                <Route path="/carrello" element={<CartCheckout/>}/>
                <Route path="/ordini" element={<PrivateRoute><MyOrders/></PrivateRoute>}/>
                <Route path="/admin" element={<PrivateRoute role="Admin"><AdminDashboard/></PrivateRoute>}/>
                <Route path="/admin/ingredienti"
                       element={<PrivateRoute role="Admin"><AdminIngredienti/></PrivateRoute>}/>
                <Route path="/admin/ordini" element={<PrivateRoute role="Admin"><AdminOrdini/></PrivateRoute>}/>
                <Route path="*" element={<Navigate to="/"/>}/>
            </Routes>
            <Footer/>
        </div>
    )
}
