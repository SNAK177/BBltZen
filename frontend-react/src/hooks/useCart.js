import { useContext } from "react";
import CartContext from "../context/CartContext.jsx";

export const useCart = () => {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error("useCart must be used within a CartProvider");
    }
    return {
        cart: context.cart,
        addToCart: context.addToCart,
        removeFromCart: context.removeFromCart,
        updateQuantity: context.updateQuantity,
        clearCart: context.clearCart,
        getTotalItems: context.getTotalItems,
        getTotalPrice: context.getTotalPrice,
        currentTable: context.currentTable,
        setCurrentTable: context.setCurrentTable,
    };
};