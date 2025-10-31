export const products = [
    {
        id: 1,
        name: 'Classic Milk Tea',
        description: 'Tè nero con latte e perle di tapioca',
        priceM: 4.50,
        priceL: 5.50,
        options: {
            base: 'Tè nero con latte',
            bubble: 'Tapioca classica',
            aroma: 'Vaniglia',
            sweetness: 50,
            ice: false,
            size: 'L'
        }
    },
    {
        id: 2,
        name: 'Matcha Latte',
        description: 'Tè matcha giapponese con latte',
        priceM: 5.00,
        priceL: 6.00,
        options: {
            base: 'Matcha con latte',
            bubble: 'Perle al cocco',
            aroma: 'Matcha',
            sweetness: 40,
            ice: true,
            size: 'L'
        }
    },
    {
        id: 3,
        name: 'Mango Paradise',
        description: 'Tè al mango fresco con perle',
        priceM: 4.80,
        priceL: 5.80,
        options: {
            base: 'Tè verde',
            bubble: 'Perle al mango',
            aroma: 'Mango',
            sweetness: 60,
            ice: true,
            size: 'L'
        }
    },
    {
        id: 4,
        name: 'Taro Dream',
        description: 'Cremoso tè al taro con perle',
        priceM: 5.20,
        priceL: 6.20,
        options: {
            base: 'Tè al taro',
            bubble: 'Tapioca classica',
            aroma: 'Taro',
            sweetness: 50,
            ice: true,
            size: 'L'
        }
    }
];