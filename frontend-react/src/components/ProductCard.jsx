import React from 'react';
import './ProductCard.css';

export const ProductCard = ({product, onAddToCart}) => {
    return (
        <div className="product-card">
            <div className="product-image">
                <span className="product-emoji">🧋</span>
            </div>
            <div className="product-content">
                <h3 className="product-title">{product.name}</h3>
                <p className="product-description">{product.description}</p>
                <div className="product-footer">
          <span className="product-price">
            €{product.priceL?.toFixed(2) || product.price?.toFixed(2)}
          </span>
                    <button
                        onClick={() => onAddToCart(product)}
                        className="add-button"
                    >
                        Aggiungi
                    </button>
                </div>
            </div>
        </div>
    );
};