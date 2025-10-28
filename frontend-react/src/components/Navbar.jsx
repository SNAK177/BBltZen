import { Link, useLocation } from 'react-router-dom'
import { useAuth } from '../context/AuthContext.jsx'
import { useCart } from '../context/CartContext.jsx'

export default function Navbar(){
  const { user, logout } = useAuth()
  const { items } = useCart()
  const loc = useLocation()
  const anchorHref = (id) => (loc.pathname === '/' ? `#${id}` : `/#${id}`)

  return (
    <header className="bbt-nav">
      <div className="bbt-logo" style={{ gap: 16 }}>
        <span className="bbt-wordmark">Bubble&nbsp;Freddy</span>
        <nav className="bbt-links" style={{ marginLeft: 12 }}>
          <Link to="/">Home</Link>
          <Link to="/carrello">Carrello ({items.length})</Link>
          {user ? (
            <>
              <Link to="/ordini">I miei ordini</Link>
              {user.ruolo === 'Admin' && (
                <>
                  <Link to="/admin">Dashboard</Link>
                  <Link to="/admin/ingredienti">Ingredienti</Link>
                  <Link to="/admin/ordini">Ordini</Link>
                </>
              )}
              <button onClick={logout} className="btn tiny" style={{ marginLeft: 8 }}>Logout</button>
            </>
          ) : (
            <Link to="/login">Login</Link>
          )}
        </nav>
      </div>

      <nav className="bbt-links">
        
        <a href={anchorHref('dove')}>Dove siamo</a>
        <Link className="bbt-cta" to="/menu">Ordina ora</Link>
      </nav>
    </header>
  )
}
