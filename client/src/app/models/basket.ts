import {v4 as uuidv4} from 'uuid';

export interface IBasket {
    id: string;
    items: BaskteItem[];
}

export interface BaskteItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureURL: string;
    brand: string;
    type: string;
}

export class Basket implements IBasket{
    id = uuidv4();
    items: BaskteItem[] = [];
}

export interface BasketTotals{
    shipping: number;
    subtotal: number;
    total: number;
}