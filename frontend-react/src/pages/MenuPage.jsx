import React from 'react';
import {useCart} from '../hooks/useCart';
import {ProductCard} from '../components/ProductCard.jsx';
import {products} from '../data/products';
import '../styles/MenuPage.css';

export const MenuPage = () => {
    const {addToCart, currentTable} = useCart();

    const handleAddToCart = (product) => {
        addToCart({
            id: product.id,
            name: product.name,
            price: product.priceL,
            quantity: 1,
            options: product.options
        });
        alert(`${product.name} aggiunto al carrello!`);
    };

    return (
        <div className="menu-container">
            <div className="menu-header">
                <h1 className="menu-title">Menu Bubble Tea</h1>
                <div className="table-badge">
                    Tavolo #{currentTable}
                </div>
            </div>

            <div className="products-grid">
                {products.map(product => (
                    <ProductCard
                        key={product.id}
                        product={product}
                        onAddToCart={handleAddToCart}
                    />
                ))}
            </div>
        </div>
    );
};