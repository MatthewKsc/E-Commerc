import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, BasketTotals, BaskteItem, IBasket } from '../models/basket';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseURL = environment.apiUrl;
  private baseUrlWithId = this.baseURL+'basket?id=';
  private basketLocalStorage = 'basket_id';

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<BasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    return this.http.get(this.baseUrlWithId + id)
      .pipe(
        map((basket: IBasket) =>{
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseURL+'basket', basket).subscribe((response: IBasket)=>{
      this.basketSource.next(response);
      this.calculateTotals();
    }, error=>{
      console.log(error);
    });
  }

  deleteBasket(basket: IBasket) {
    this.http.delete(this.baseUrlWithId + basket.id).subscribe(()=>{
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem(this.basketLocalStorage)
    }, error =>{
      console.log(error);
    });
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item: Product, quantity = 1){
    const itemToAdd: BaskteItem = this.mapProductItemToBasketItem(item, quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBaske();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    
    this.setBasket(basket);
  }

  incrementItemQuantity(item: BaskteItem){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x=> x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: BaskteItem){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x=> x.id === item.id);
    if(basket.items[foundItemIndex].quantity> 1){
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    }else{
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: BaskteItem) {
    const basket = this.getCurrentBasketValue();
    if(basket.items.some(x=> x.id === item.id)){
      basket.items = basket.items.filter(s => s.id !== item.id);
      if(basket.items.length>0){
        this.setBasket(basket);
      }else{
        this.deleteBasket(basket);
      }
    }
  }

  private calculateTotals(){
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    const subtotal = basket.items.reduce((a,b)=> (b.price * b.quantity) + a, 0);
    const total = shipping + subtotal;
    this.basketTotalSource.next({shipping, total, subtotal});
  }

  private addOrUpdateItem(items: BaskteItem[], itemToAdd: BaskteItem, quantity: number): BaskteItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if(index === -1){
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }else{
      items[index].quantity += quantity;
    }

    return items;
  }

  private createBaske(): IBasket {
    const basket = new Basket();
    localStorage.setItem(this.basketLocalStorage, basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product, quantity: number): BaskteItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureURL: item.pictureURL,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }
}
