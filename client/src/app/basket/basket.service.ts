import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, BaskteItem, IBasket } from '../models/basket';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseURL = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    return this.http.get(this.baseURL +'basket?id='+id)
      .pipe(
        map((basket: IBasket) =>{
          this.basketSource.next(basket);
        })
      );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseURL+'basket', basket).subscribe((response: IBasket)=>{
      this.basketSource.next(response);
    }, error=>{
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
    localStorage.setItem('basket_id', basket.id);
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
