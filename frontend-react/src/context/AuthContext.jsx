import {createContext, useContext, useEffect, useState} from 'react'
import {apiPost, apiGet} from '../services/api.js'

const AuthCtx = createContext(null)
export const useAuth = () => useContext(AuthCtx)

export function AuthProvider({children}) {
    const [user, setUser] = useState(null)
    useEffect(() => {
        (async () => {
            try {
                const me = await apiGet('/auth/me');
                setUser(me)
            } catch {
            }
        })()
    }, [])

    async function login(email, password) {
        const {token} = await apiPost('/auth/login', {email, password});
        localStorage.setItem('token', token);
        const me = await apiGet('/auth/me');
        setUser(me)
    }

    function logout() {
        localStorage.removeItem('token');
        setUser(null)
    }

    return <AuthCtx.Provider value={{user, login, logout}}>{children}</AuthCtx.Provider>
}
