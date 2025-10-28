import { createContext, useContext, useEffect, useState } from 'react'
const CartCtx = createContext(null)
export const useCart = () => useContext(CartCtx)
export function CartProvider({ children }){
  const [items, setItems] = useState(()=>JSON.parse(localStorage.getItem('cart')||'[]'))
  useEffect(()=>{ localStorage.setItem('cart', JSON.stringify(items)) },[items])
  const add = item => setItems(p=>[...p,item])
  const remove = index => setItems(p=>p.filter((_,i)=>i!==index))
  const clear = ()=> setItems([])
  return <CartCtx.Provider value={{ items, add, remove, clear }}>{children}</CartCtx.Provider>
}
